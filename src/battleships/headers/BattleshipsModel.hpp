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
    public:
        Event<bool> onGameOver;
        Event<FieldsChanges> onFieldChanged;

        Battlefield getField(FieldType fieldType);
        void shoot(Vector2i position);

        BattleshipsModel();
    };
}
