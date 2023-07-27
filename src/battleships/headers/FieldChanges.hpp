#pragma once

#include <TileChanges.hpp>
#include <list>

namespace bs
{
    struct FieldChanges
    {
        std::list<TileChanges> tileChanges;
    };
} // namespace battleships
