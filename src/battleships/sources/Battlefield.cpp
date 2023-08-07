#include <Battlefield.hpp>

namespace bs
{

    void Battlefield::addShipToField(Ship ship)
    {
        for (int i = 0; i < ship.length; i++)
        {
            if (ship.isHorizontal)
                field[ship.startPosition.x + i][ship.startPosition.y] = TileState::ship;
            else
                field[ship.startPosition.x][ship.startPosition.y + i] = TileState::ship;
        }
    }

    bool Battlefield::checkShipArea(Rectangle2i area)
    {
        for (auto &ship : ships)
        {
            if (area.getCollision(ship.startPosition))
                return false;
            if (area.getCollision(ship.getEndPosition()))
                return false;
        }

        return true;
    }

    bool Battlefield::checkShipOutOfBorders(Ship ship)
    {
        if (ship.startPosition.x > 9 || ship.startPosition.y > 9)
            return false;
        if (ship.startPosition.x < 0 || ship.startPosition.y < 0)
            return false;

        if (ship.getEndPosition().x > 9 || ship.getEndPosition().y > 9)
            return false;
        if (ship.getEndPosition().x < 0 || ship.getEndPosition().y < 0)
            return false;

        return true;
    }

    bool Battlefield::checkShipPlace(Ship ship)
    {
        if (!checkShipOutOfBorders(ship))
            return false;
        if (!checkShipArea(ship.getAreaRectangle()))
            return false;
        return true;
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

    Battlefield::Battlefield()
    {
        for (int i = 4; i > 0; i--)
            for (int j = 5 - i; j < 0; j++)
                placeShip(i);
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
