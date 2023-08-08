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

        bool checkShipArea(Rectangle2i area);
        bool checkShipOutOfBorders(Ship ship);
        bool checkShipPlace(Ship ship);

        void addShipToField(Ship ship);

        bool tryPlaceShip(Ship ship);
        void placeShip(int size);

    public:
        Event<FieldChanges> onFieldChanged;

        TileState **getField();

        bool tryShoot(Vector2i position);
        void shootRandom();

        Battlefield();
        ~Battlefield();
    };
} // namespace battleships
