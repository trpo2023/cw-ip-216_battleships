using System.Collections.Generic;

namespace Battleships.Models.Primitive;

public struct Rectangle
{
    public Vector2i startPosition;
    public Vector2i endPosition;

    public bool GetCollision(Vector2i position)
    {
        return (
            position.x >= startPosition.x &&
            position.x <= endPosition.x &&
            position.y >= startPosition.y &&
            position.y <= endPosition.y
        );
    }

    public readonly int GetWidth()
    {
        return endPosition.x - startPosition.x + 1;
    }
    public readonly int GetHeight()
    {
        return endPosition.y - startPosition.y + 1;
    }

    public readonly HashSet<Vector2i> GetPositionsSet()
    {
        HashSet<Vector2i> result = new();
        for (int i = 0; i < GetWidth(); i++)
            for (int j = 0; j < GetHeight(); j++)
                result.Add(new Vector2i(startPosition.x + i, startPosition.y + j));
        return result;
    }

    public readonly HashSet<Vector2i> GetOutlinePositionsSet()
    {
        HashSet<Vector2i> result = GetPositionsSet();
        HashSet<Vector2i> toDelete = new();
        foreach (var item in result)
        {
            if (item.x == startPosition.x || item.x == endPosition.x ||
                item.y == startPosition.y || item.y == endPosition.y)
                toDelete.Add(item);
        }
        foreach (var item in toDelete)
            result.Remove(item);
        return result;
    }

    public Rectangle(Vector2i startPosition, Vector2i endPosition)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
    }
    public Rectangle()
    {
        startPosition = new Vector2i(0, 0);
        endPosition = new Vector2i(0, 0);
    }
}