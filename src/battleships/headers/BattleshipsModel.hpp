#pragma once

#include <Vector2i.hpp>
#include <Event.hpp>
#include <FieldsChanges.hpp>
#include <FieldType.hpp>
#include <Battlefield.hpp>

namespace bs
{
    class BattleshipsModel
    {
    private:
        Battlefield *playerField;
        Battlefield *enemyField;

    public:
        Event<bool> onGameOver;
        Event<FieldsChanges> onFieldChanged;

        TileState **getTileField(FieldType fieldType);
        bool tryShoot(Vector2i position);

        BattleshipsModel();
        ~BattleshipsModel();
    };
}
