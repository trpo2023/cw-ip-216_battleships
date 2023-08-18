using System;

namespace Battleships.Models.Primitive;

public struct Vector2i
{
    public int x = 0;
    public int y = 0;

    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2i()
    {
        x = 0;
        y = 0;
    }

    public void MakeOffset(int squareSize)
    {
        if (x != squareSize - 1)
        {
            x++;
        }
        else if (y != squareSize - 1)
        {
            x = 0;
            y++;
        }
        else
        {
            x = 0;
            y = 0;
        }
    }

    public static Vector2i GetRandomVector(int maxX, int maxY)
    {
        Random rnd = new();
        return new Vector2i(rnd.Next() % maxX, rnd.Next() % maxY);
    }

    public override readonly string ToString()
    {
        return $"{x}; {y}";
    }

    private static bool Compare(Vector2i left, Vector2i right)
    {
        return (left.x == right.x) && (left.y == right.y);
    }

    public static bool operator !=(Vector2i left, Vector2i right)
    {
        return !Compare(left, right);
    }

    public static bool operator ==(Vector2i left, Vector2i right)
    {
        return Compare(left, right);
    }
}
