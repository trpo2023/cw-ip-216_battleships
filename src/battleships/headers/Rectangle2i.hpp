#pragma once

#include <Vector2i.hpp>
#include <set>

namespace bs
{
    struct Rectangle2i
    {
        Vector2i startPosition;
        Vector2i endPosition;

        bool getCollision(Vector2i position);

        int getWidth();
        int getHeight();

        std::set<Vector2i> getOutlinePositionsSet();
        std::set<Vector2i> getPositionsSet();

        Rectangle2i(
            Vector2i startPosition,
            Vector2i endPosition) : startPosition(startPosition),
                                    endPosition(endPosition) {}
        Rectangle2i() {}
    };

} // namespace bs
