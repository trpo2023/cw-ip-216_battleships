#pragma once

#include <Vector2i.hpp>

namespace bs
{
    struct Rectangle2i
    {
        Vector2i startPosition;
        Vector2i endPosition;

        bool getCollision(Vector2i position);

        Rectangle2i(
            Vector2i startPosition,
            Vector2i endPosition) : startPosition(startPosition),
                                    endPosition(endPosition) {}
        Rectangle2i() {}
    };

} // namespace bs
