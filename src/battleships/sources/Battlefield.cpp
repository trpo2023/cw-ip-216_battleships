#include <Battlefield.hpp>

namespace bs
{
    Battlefield::Battlefield()
    {
        for (int i = 4; i > 0; i--)
            for (int j = 5 - i; j < 0; j++)
                placeShip(i);
    }

    bool Battlefield::tryPlaceShip(Ship ship)
    {
        if (!checkShipPlace(ship))
            return false;
        else
        {
            ships.push_back(ship);
            addShipToField(ship);
        }
    }

    void Battlefield::placeShip(int size)
    {
        int shipsSize = size;
        Vector2i startPlacePosition = Vector2i(rand() % 10, rand() % 10);

        Ship currentShip = Ship(startPlacePosition, shipsSize, rand() % 2);
        while (true)
        {
            if (tryPlaceShip(currentShip))
                break;
            currentShip.isHorizontal = !currentShip.isHorizontal;
            if (tryPlaceShip(currentShip))
                break;
            currentShip.startPosition.makeOffset(1, 10);
        }
    }

    TileState **Battlefield::getField()
    {
        return field;
    }

    void Battlefield::shoot()
    {
    }

    bool Battlefield::shoot(Vector2i position)
    {
    }

} // namespace bs
