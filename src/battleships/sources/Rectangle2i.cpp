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

    int Rectangle2i::getWidth()
    {
        return (startPosition.x - endPosition.x + 1);
    }

    int Rectangle2i::getHeight()
    {
        return (startPosition.y - endPosition.y + 1);
    }

    std::set<Vector2i> Rectangle2i::getPositionsSet()
    {
        std::set<Vector2i> result;
        for (int i = 0; i < getWidth(); i++)
            for (int j = 0; j < getHeight(); j++)
                result.insert(startPosition.x + i, startPosition.y + j);
    }

    std::set<Vector2i> Rectangle2i::getOutlinePositionsSet()
    {
        std::set<Vector2i> result = getPositionsSet();
        std::set<Vector2i> toDelete;
        for (auto &item : result)
        {
            if (item.x == startPosition.x || item.x == endPosition.x ||
                item.y == startPosition.y || item.y == endPosition.y)
                toDelete.insert(item);
        }
        for (auto &item : toDelete)
            result.erase(item);
    }
} // namespace bs
