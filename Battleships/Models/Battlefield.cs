using System;
using System.Collections.Generic;
using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public class Battlefield
{
    private TileState[,] field;
    private List<Ship> _ships;
    private List<TileChanges> currentChanges;


    public delegate void FieldChangedHandler(List<TileChanges> fieldsChanges);
    public event FieldChangedHandler OnFieldChanged;

    private static bool CheckShipOutOfBorders(Ship ship)
    {
        Rectangle fieldRectangle = new(new Vector2i(0, 0), new Vector2i(9, 9));

        if (fieldRectangle.GetCollision(ship.startPosition))
            return false;
        if (fieldRectangle.GetCollision(ship.GetEndPosition()))
            return false;
        return true;
    }

    private bool CheckShipArea(Rectangle area)
    {
        foreach (var ship in _ships)
        {
            if (area.GetCollision(ship.startPosition))
                return false;
            if (area.GetCollision(ship.GetEndPosition()))
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
            if (ship.isHorizontal)
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

        Ship currentShip = new(startPlacePosition, shipsSize, rnd.Next() % 2 > 0);

        while (true)
        {
            if (TryPlaceShip(currentShip))
                break;
            currentShip.isHorizontal = !currentShip.isHorizontal;
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


    public TileState[,] GetField()
    {
        return field;
    }
}