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
    }

    TileState **Battlefield::getField()
    {
    }

    void Battlefield::shoot()
    {
    }

    bool Battlefield::shoot(Vector2i position)
    {
    }

} // namespace bs
