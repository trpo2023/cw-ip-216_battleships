#pragma once

#include <Vector2i.hpp>

namespace bs
{
    struct Ship
    {
        int length;
        bool isHorizontal;
        Vector2i startPosition;

        Ship(Vector2i startPosition, int length, bool isHorizontal)
            : startPosition(startPosition),
              length(length),
              isHorizontal(isHorizontal) {}
    };
} // namespace battleships