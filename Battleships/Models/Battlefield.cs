using System;
using System.Collections.Generic;
using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public class Battlefield
{
    private TileState[,] field;
    private List<Ship> _ships = new();
    private List<TileChanges> currentChanges = new();

    public delegate void FieldChangedHandler(List<TileChanges> fieldsChanges);
    public event FieldChangedHandler OnFieldChanged;

    private static bool CheckShipOutOfBorders(Ship ship)
    {
        Rectangle fieldRectangle = new(new Vector2i(0, 0), new Vector2i(9, 9));

        if (!fieldRectangle.GetCollision(ship.startPosition))
            return false;
        if (!fieldRectangle.GetCollision(ship.endPosition))
            return false;
        return true;
    }

    private bool CheckShipArea(Rectangle area)
    {
        foreach (var ship in _ships)
        {
            if (area.GetCollision(ship.startPosition))
                return false;
            if (area.GetCollision(ship.endPosition))
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
            if (ship.CurrentOrientation == Ship.Orientation.Horizontal)
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

            currentShip.CurrentOrientation =
                currentShip.CurrentOrientation == Ship.Orientation.Horizontal
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
            currentChanges.Add(new TileChanges(TileState.Destroy, position));
        }

        foreach (var position in outlineSet)
        {
            field[position.x, position.y] = TileState.Miss;
            currentChanges.Add(new TileChanges(TileState.Miss, position));
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
            currentChanges.Add(new TileChanges(TileState.Hit, position));
    }

    private void Shoot(Vector2i position)
    {
        if (field[position.x, position.y] == TileState.Empty)
        {
            field[position.x, position.y] = TileState.Miss;
            currentChanges.Add(new TileChanges(TileState.Miss, position));
        }
        else
            HitShip(position);
        OnFieldChanged.Invoke(currentChanges);
        currentChanges.Clear();
    }

    public bool TryShoot(Vector2i position)
    {
        if (!CheckShootPosition(position))
            return false;

        Shoot(position);
        return true;
    }

    public void ShootRandom()
    {
        while (true)
            if (TryShoot(Vector2i.GetRandomVector(10, 10)))
                break;
    }

    public TileState[,] GetField()
    {
        return field;
    }
}
