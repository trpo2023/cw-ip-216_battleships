#pragma once

#include <Battlefield.hpp>
#include <Vector2i.hpp>
#include <TileState.hpp>

namespace bs
{
    struct TileChanges
    {
        TileState tileState = TileState::empty;
        Vector2i position;

        TileChanges(TileState tileState, Vector2i position) : tileState(tileState),
                                                              position(position) {}
    };
} // namespace battleships
