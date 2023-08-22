using System;
using System.Collections.Generic;
using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public class Battlefield
{
    private TileState[,] field;
    private List<Ship> _ships = new();

    public delegate void FieldChangedHandler(List<TileChanges> fieldsChanges);
    public event FieldChangedHandler OnFieldChanged;

    public delegate void OnGameOverHandler();
    public event OnGameOverHandler OnGameOver;

    private void TriggerChanges(TileChanges changes)
    {
        OnFieldChanged.Invoke(new List<TileChanges> { changes });
    }

    private static bool CheckShipOutOfBorders(Ship ship)
    {
        IntRectangle fieldRectangle = new(new Vector2i(0, 0), new Vector2i(9, 9));

        if (!fieldRectangle.GetCollision(ship.startPosition))
            return false;
        if (!fieldRectangle.GetCollision(ship.EndPosition))
            return false;
        return true;
    }

    private bool CheckShipArea(IntRectangle area)
    {
        foreach (var ship in _ships)
        {
            if (area.GetCollision(ship.startPosition))
                return false;
            if (area.GetCollision(ship.EndPosition))
                return false;
        }

        return true;
    }

    private bool CheckShipPlace(Ship ship)
    {
        if (!CheckShipOutOfBorders(ship))
            return false;
        if (!CheckShipArea(ship.GetAreaRectangle()))
            return false;
        return true;
    }

    private void AddShipToField(Ship ship)
    {
        for (int i = 0; i < ship.lenght; i++)
        {
            if (ship.orientation == Ship.Orientation.Horizontal)
                field[ship.startPosition.x + i, ship.startPosition.y] = TileState.Ship;
            else
                field[ship.startPosition.x, ship.startPosition.y + i] = TileState.Ship;
        }
    }

    private bool TryPlaceShip(Ship ship)
    {
        if (!CheckShipPlace(ship))
            return false;
        else
        {
            _ships.Add(ship);
            AddShipToField(ship);
            return true;
        }
    }

    private void PlaceShip(int lenght)
    {
        int shipsSize = lenght;
        Random rnd = new();
        Vector2i startPlacePosition = new(rnd.Next() % 10, rnd.Next() % 10);

        var direction =
            (rnd.Next() % 2 == 0) ? Ship.Orientation.Horizontal : Ship.Orientation.Vertical;

        Ship currentShip = new(startPlacePosition, shipsSize, direction);

        while (true)
        {
            if (TryPlaceShip(currentShip))
                break;

            currentShip.orientation =
                currentShip.orientation == Ship.Orientation.Horizontal
                    ? Ship.Orientation.Vertical
                    : Ship.Orientation.Horizontal;

            if (TryPlaceShip(currentShip))
                break;
            currentShip.startPosition.MakeOffset(10);
        }
    }

    public Battlefield()
    {
        field = new TileState[10, 10];
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                field[i, j] = TileState.Empty;

        for (int i = 4; i > 0; i--)
            for (int j = 0; j < 5 - i; j++)
                PlaceShip(i);
    }

    private bool CheckShootPosition(Vector2i position)
    {
        return field[position.x, position.y] == TileState.Empty
            || field[position.x, position.y] == TileState.Ship;
    }

    private Ship GetShipByPosition(Vector2i position)
    {
        foreach (var ship in _ships)
            if (ship.GetBodyRectangle().GetCollision(position))
                return ship;
        return new Ship();
    }

    private void DestroyShip(Ship ship)
    {
        HashSet<Vector2i> outlineSet = ship.GetAreaOutlinePositionsSet();
        HashSet<Vector2i> bodySet = ship.GetBodyPositionsSet();

        foreach (var position in bodySet)
        {
            field[position.x, position.y] = TileState.Destroy;
            TriggerChanges(new TileChanges(TileState.Destroy, position));
        }

        foreach (var position in outlineSet)
        {
            field[position.x, position.y] = TileState.Miss;
            TriggerChanges(new TileChanges(TileState.Miss, position));
        }
    }

    private bool CheckShipDestroyed(Ship ship)
    {
        foreach (var pos in ship.GetBodyPositionsSet())
            if (field[pos.x, pos.y] == TileState.Ship)
                return false;
        return true;
    }

    private void HitShip(Vector2i position)
    {
        field[position.x, position.y] = TileState.Hit;
        Ship hittedShip = GetShipByPosition(position);
        if (CheckShipDestroyed(hittedShip))
            DestroyShip(hittedShip);
        else
            TriggerChanges(new TileChanges(TileState.Hit, position));
    }

    private bool CheckFieldDestroyed()
    {
        foreach (var ship in _ships)
            if (!CheckShipDestroyed(ship))
                return false;

        return true;
    }

    private ShootResult Shoot(Vector2i position)
    {
        Console.WriteLine($"shoot {position}");
        ShootResult result;
        if (field[position.x, position.y] == TileState.Empty)
        {
            result = ShootResult.Miss;
            field[position.x, position.y] = TileState.Miss;
            TriggerChanges(new TileChanges(TileState.Miss, position));
        }
        else
        {
            result = ShootResult.Hit;
            HitShip(position);
        }
        if (CheckFieldDestroyed())
            OnGameOver.Invoke();
        return result;
    }

    public ShootResult TryShoot(Vector2i position)
    {
        if (!CheckShootPosition(position))
            return ShootResult.None;

        return Shoot(position);
    }

    public ShootResult ShootRandom()
    {
        Vector2i startShootPosition = Vector2i.GetRandomVector(10, 10);
        Vector2i currentShootPosition = startShootPosition;
        currentShootPosition.MakeOffset(10);
        while (startShootPosition != currentShootPosition)
        {
            ShootResult result = TryShoot(currentShootPosition);
            if (result != ShootResult.None)
                return result;
            currentShootPosition.MakeOffset(10);
        }
        return ShootResult.None;
    }

    public TileState[,] GetField()
    {
        return field;
    }
}
