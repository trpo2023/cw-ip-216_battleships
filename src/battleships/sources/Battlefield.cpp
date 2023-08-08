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
        Rectangle2i fieldRectangle(Vector2i(0, 0), Vector2i(9, 9));

        if (fieldRectangle.getCollision(ship.startPosition))
            return false;
        if (fieldRectangle.getCollision(ship.getEndPosition()))
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
            return true;
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
        field = new TileState *[10];
        for (int i = 0; i < 10; i++)
        {
            field[i] = new TileState[10];
            for (int j = 0; j < 10; j++)
            {
                field[i][j] = TileState::empty;
            }
        }
        for (int i = 4; i > 0; i--)
            for (int j = 0; j < 5 - i; j++)
                placeShip(i);
    }

    TileState **Battlefield::getField()
    {
        return field;
    }

    bool Battlefield::checkShootPosition(Vector2i position)
    {
        return field[position.x][position.y] == TileState::empty ||
               field[position.x][position.y] == TileState::ship;
    }

    Ship Battlefield::getShipByPosition(Vector2i position)
    {
        for (auto &ship : ships)
            if (ship.getBodyRectangle().getCollision(position))
                return ship;
    }

    void Battlefield::destroyShip(Ship ship)
    {
        std::set<Vector2i> bodySet = ship.getAreaRectangle().getOutlinePositionsSet();
        std::set<Vector2i> outlineSet = ship.getAreaRectangle().getOutlinePositionsSet();

        for (auto &position : bodySet)
        {
            field[position.x][position.y] = TileState::destroy;
            currentChanges.tileChanges.push_back(TileChanges(TileState::destroy, position));
        }

        for (auto &position : bodySet)
        {
            field[position.x][position.y] = TileState::miss;
            currentChanges.tileChanges.push_back(TileChanges(TileState::miss, position));
        }
        onFieldChanged.invoke(currentChanges);
        currentChanges.tileChanges.clear();
    }

    void Battlefield::hitShip(Vector2i position)
    {
        field[position.x][position.y] = TileState::hit;
        Ship hittedShip = getShipByPosition(position);
        if (checkShipDestroyed(hittedShip))
            destroyShip(hittedShip);
        else
            currentChanges.tileChanges.push_back(TileChanges(TileState::hit, position));
    }

    void Battlefield::shoot(Vector2i position)
    {
        if (field[position.x][position.y] == TileState::empty)
        {
            field[position.x][position.y] = TileState::miss;
            currentChanges.tileChanges.push_back(TileChanges(TileState::miss, position));
        }
        else
            hitShip(position);
        onFieldChanged.invoke(currentChanges);
        currentChanges.tileChanges.clear();
    }

    bool Battlefield::tryShoot(Vector2i position)
    {
        if (!checkShootPosition(position))
            return false;

        shoot(position);
        return true;
    }

    void Battlefield::shootRandom()
    {
        while (true)
            if (tryShoot(Vector2i::getRandomVector(10, 10)))
                break;
    }

    Battlefield::~Battlefield()
    {
        for (int i = 0; i < 10; i++)
            delete[] field[i];
        delete[] field;
    }

} // namespace bs
