using System.Collections.Generic;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public struct Ship
{
    public int lenght;
    public Vector2i startPosition;
    public Vector2i endPosition;

    private Orientation _orientation = Orientation.Horizontal;
    public Orientation CurrentOrientation
    {
        readonly get => _orientation;
        set
        {
            _orientation = value;

            endPosition = startPosition;
            switch (value)
            {
                case Orientation.Horizontal:
                    endPosition.x += lenght - 1;
                    break;
                case Orientation.Vertical:
                    endPosition.y += lenght - 1;
                    break;
            }
        }
    }

    public enum Orientation
    {
        Vertical,
        Horizontal
    }

    public Ship(Vector2i startPosition, int lenght, Orientation direction)
    {
        this.lenght = lenght;
        this.startPosition = this.endPosition = startPosition;
        this.CurrentOrientation = direction;
    }

    public Rectangle GetAreaRectangle()
    {
        Rectangle result;

        result.startPosition = startPosition;
        result.startPosition.x -= 1;
        result.startPosition.y -= 1;

        result.endPosition = endPosition;
        result.endPosition.x += 1;
        result.endPosition.y += 1;

        return result;
    }

    public Rectangle GetBodyRectangle()
    {
        return new Rectangle(startPosition, endPosition);
    }

    public HashSet<Vector2i> GetBodyPositionsSet()
    {
        return new Rectangle(startPosition, endPosition).GetPositionsSet();
    }

    public HashSet<Vector2i> GetAreaOutlinePositionsSet()
    {
        var result = GetAreaRectangle().GetOutlinePositionsSet();
        HashSet<Vector2i> toDelete = new();
        foreach (var item in result)
        {
            if (item.x < 0 || item.x > 9 || item.y < 0 || item.y > 9)
                toDelete.Add(item);
        }
        foreach (var item in toDelete)
        {
            result.Remove(item);
        }
        return result;
    }
}
