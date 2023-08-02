#include <BattleshipsModel.hpp>
#include <Battlefield.hpp>

namespace bs
{
    BattleshipsModel::BattleshipsModel()
    {
        this->playerField = Battlefield();
        this->enemyField = Battlefield();

        this->playerField.onFieldChanged.addListener([&](FieldChanges fc)
                                                     {
            FieldsChanges fdc;
            fdc.isPlayerField = true;
            fdc.changes = fc;
            this->onFieldChanged.invoke(fdc); });
        this->enemyField.onFieldChanged.addListener([&](FieldChanges fc)
                                                    {
            FieldsChanges fdc;
            fdc.isPlayerField = false;
            fdc.changes = fc;
            this->onFieldChanged.invoke(fdc); });
    }

    TileState **BattleshipsModel::getTileField(FieldType fieldType)
    {
        return (fieldType == FieldType::Player)
                   ? this->playerField.getField()
                   : this->enemyField.getField();
    }

    bool BattleshipsModel::shoot(Vector2i position)
    {
        if (enemyField.shoot(position))
            return false;
        this->playerField.shoot();
        return true;
    }
} // namespace bs
