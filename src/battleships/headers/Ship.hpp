#pragma once

#include <Vector2i.hpp>

namespace bs
{
    class Ship
    {
    private:
        int _length;
        bool _isHorizontal;

        Vector2i _startPosition;
        Vector2i _endPosition;

    public:
        Ship(Vector2i startPosition, int length, bool isHorizontal)
            : _startPosition(startPosition),
              _length(length),
              _isHorizontal(isHorizontal) {}

        Vector2i getPosition();
        Vector2i getEndPosition();

        int isHorizontal();
        int getLength();
    };
} // namespace battleships