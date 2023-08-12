using System.Collections.Generic;
using Battleships.Models.Primitive;

namespace Battleships.Models;

internal struct Ship
{
    public int lenght;
    public Vector2i startPosition;
    public bool isHorizontal;

    public Ship(Vector2i startPosition, int lenght, bool isHorizontal)
    {
        this.lenght = lenght;
        this.startPosition = startPosition;
        this.isHorizontal = isHorizontal;
    }

    public Vector2i GetEndPosition()
    {
        var result = startPosition;
        if (isHorizontal)
        {
            result.x += lenght - 1;
        }
        else
        {
            result.y += lenght - 1;
        }

        return result;
    }

    public Rectangle GetAreaRectangle()
    {
        Rectangle result;

        result.startPosition = startPosition;
        result.startPosition.x -= 1;
        result.startPosition.y -= 1;

        result.endPosition = GetEndPosition();
        result.endPosition.x += 1;
        result.endPosition.y += 1;

        return result;
    }
    public Rectangle GetBodyRectangle()
    {
        return new Rectangle(startPosition, GetEndPosition());
    }

    public HashSet<Vector2i> GetBodyPositionsSet()
    {
        return new Rectangle(startPosition, GetEndPosition()).GetPositionsSet();
    }
    public HashSet<Vector2i> GetAreaOutlinePositionsSet()
    {
        return GetAreaRectangle().GetOutlinePositionsSet();
    }

}