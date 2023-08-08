#include <Ship.hpp>

namespace bs
{
    Rectangle2i Ship::getAreaRectangle()
    {
        Rectangle2i result;

        result.startPosition = startPosition;
        result.startPosition.x -= 1;
        result.startPosition.y -= 1;

        result.endPosition = getEndPosition();
        result.endPosition.x += 1;
        result.endPosition.y += 1;

        return result;
    }

    Vector2i Ship::getEndPosition()
    {
        Vector2i endPosition = startPosition;
        if (isHorizontal)
            endPosition.x += length - 1;
        else
            endPosition.y += length - 1;
        return endPosition;
    }

    Rectangle2i Ship::getBodyRectangle()
    {
        return Rectangle2i(startPosition, getEndPosition());
    }

    std::set<Vector2i> Ship::getBodyPositionsSet()
    {
        return Rectangle2i(startPosition, getEndPosition()).getPositionsSet();
    }
    std::set<Vector2i> Ship::getAreaOutlinePositionsSet()
    {
        return getAreaRectangle().getOutlinePositionsSet();
    }
} // namespace bs
