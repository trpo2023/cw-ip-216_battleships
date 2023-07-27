#pragma once

#include <list>
#include <Ship.hpp>
#include <Event.hpp>
#include <FieldsChanges.hpp>

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

        Battlefield(TileState **playerField, TileState **enemyField);
        Battlefield(std::list<Ship> playerShips, std::list<Ship> enemyShips);
    };
} // namespace battleships
