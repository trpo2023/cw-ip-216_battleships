#pragma once

#include <list>
#include <Ship.hpp>

namespace bs
{
    class Battlefield
    {
    public:
        enum class TileState;

    private:
        TileState **field;

    public:
        enum class TileState
        {
            empty,
            ship,
            miss,
            destroy,
            hit
        };

        Event<FieldChanges> onFieldChanged;

        TileState **getField();

        void hit(Vector2i position);

        Battlefield(TileState **field);
        Battlefield(std::list<Ship> ships);
    };
} // namespace battleships
