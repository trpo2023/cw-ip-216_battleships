#pragma once

#include <Vector2i.hpp>
#include <Rectangle2i.hpp>
#include <set>

namespace bs
{
    struct Ship
    {
        int length;
        bool isHorizontal;
        Vector2i startPosition;

        Vector2i getEndPosition();

        Rectangle2i getAreaRectangle();
        Rectangle2i getBodyRectangle();

        std::set<Vector2i> getBodyPositionsSet();
        std::set<Vector2i> getAreaOutlinePositionsSet();

        Ship(Vector2i startPosition, int length, bool isHorizontal)
            : startPosition(startPosition),
              length(length),
              isHorizontal(isHorizontal) {}
    };
} // namespace battleships