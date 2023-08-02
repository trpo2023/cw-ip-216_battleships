#pragma once

#include <list>
#include <Ship.hpp>
#include <Event.hpp>
#include <FieldsChanges.hpp>
#include <TileState.hpp>
namespace bs
{
    class Battlefield
    {
    private:
        TileState **field;

    public:
        struct FieldChanges;
        Event<FieldChanges> onFieldChanged;

        TileState **getField();

        bool shoot(Vector2i position);

        Battlefield();
    };
} // namespace battleships
