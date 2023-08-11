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
        this.x = 0;
        this.y = 0;
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

}