#include <Rectangle2i.hpp>

namespace bs
{
    bool Rectangle2i::getCollision(Vector2i position)
    {
        return (
            position.x >= startPosition.x &&
            position.x <= endPosition.x &&
            position.y >= startPosition.y &&
            position.y <= endPosition.y);
    }
} // namespace bs
