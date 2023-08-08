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
        FieldChanges currentChanges;

        bool checkShipArea(Rectangle2i area);
        bool checkShipOutOfBorders(Ship ship);
        bool checkShipPlace(Ship ship);
        void addShipToField(Ship ship);
        bool tryPlaceShip(Ship ship);
        void placeShip(int size);

        bool checkShootPosition(Vector2i position);
        void shoot(Vector2i position);
        Ship getShipByPosition(Vector2i position);
        void hitShip(Vector2i position);
        bool checkShipDestroyed(Ship ship);
        void destroyShip(Ship ship);

    public:
        Event<FieldChanges> onFieldChanged;

        TileState **getField();

        bool tryShoot(Vector2i position);
        void shootRandom();

        Battlefield();
        ~Battlefield();
    };
} // namespace battleships
