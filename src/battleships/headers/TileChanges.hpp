#pragma once

#include <Battlefield.hpp>
#include <Vector2i.hpp>

namespace bs
{
    struct TileChanges
    {
        Battlefield::TileState tileState;
        Vector2i position;
    };
} // namespace battleships
