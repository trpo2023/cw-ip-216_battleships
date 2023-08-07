#pragma once

#include <list>
#include <Ship.hpp>
#include <Event.hpp>
#include <FieldsChanges.hpp>
#include <TileState.hpp>
namespace bs
{
    struct FieldChanges;

    class Battlefield
    {
    private:
        TileState **field;
        std::list<Ship> ships;

        void addShipToField(Ship ship);
        bool tryPlaceShip(Ship ship);
        void placeShip(int size);

    public:
        Event<FieldChanges> onFieldChanged;

        TileState **getField();

        bool shoot(Vector2i position);
        void shoot();

        Battlefield();
    };
} // namespace battleships
