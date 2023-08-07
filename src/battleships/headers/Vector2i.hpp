#pragma once

namespace bs
{
    struct Vector2i
    {
        int x = 0;
        int y = 0;

        void makeOffset(int value, int squareSize);
        Vector2i() {}
        Vector2i(int x, int y) : x(x), y(y) {}
    };
}