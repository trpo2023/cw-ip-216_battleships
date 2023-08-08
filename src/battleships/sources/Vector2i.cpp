#include <Vector2i.hpp>

namespace bs
{
    void Vector2i::makeOffset(int value, int squareSize)
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

    Vector2i Vector2i::getRandomVector(int maxX, int maxY)
    {
        return Vector2i(rand() % maxX, rand() % maxY);
    }
} // namespace bs
