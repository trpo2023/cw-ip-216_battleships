#pragma once

#include <stdlib.h>

namespace bs
{
    struct Vector2i
    {
        int x = 0;
        int y = 0;

        void makeOffset(int value, int squareSize);
        static Vector2i getRandomVector(int maxX, int maxY);
        Vector2i() {}
        Vector2i(int x, int y) : x(x), y(y) {}
    };
}