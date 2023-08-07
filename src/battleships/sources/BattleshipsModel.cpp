#include <BattleshipsModel.hpp>
#include <Battlefield.hpp>

namespace bs
{
    BattleshipsModel::BattleshipsModel()
    {
        playerField = new Battlefield;
        enemyField = new Battlefield;

        playerField->onFieldChanged.addListener([&](FieldChanges fc)
                                                {
            FieldsChanges fdc;
            fdc.isPlayerField = true;
            fdc.changes = fc;
            onFieldChanged.invoke(fdc); });
        enemyField->onFieldChanged.addListener([&](FieldChanges fc)
                                               {
            FieldsChanges fdc;
            fdc.isPlayerField = false;
            fdc.changes = fc;
            onFieldChanged.invoke(fdc); });
    }

    TileState **BattleshipsModel::getTileField(FieldType fieldType)
    {
        return (fieldType == FieldType::Player)
                   ? playerField->getField()
                   : enemyField->getField();
    }

    bool BattleshipsModel::shoot(Vector2i position)
    {
        if (enemyField->shoot(position))
            return false;
        playerField->shoot();
        return true;
    }

    BattleshipsModel::~BattleshipsModel()
    {
        delete playerField;
        delete enemyField;
    }
} // namespace bs
