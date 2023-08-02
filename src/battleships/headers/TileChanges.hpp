#pragma once

#include <Battlefield.hpp>
#include <Vector2i.hpp>

namespace bs
{
    struct TileChanges
    {
        TileState tileState;
        Vector2i position;
    };
} // namespace battleships
