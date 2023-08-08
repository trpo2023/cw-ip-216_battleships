#include <BattleshipsModel.hpp>
#include <Battlefield.hpp>

namespace bs
{
    BattleshipsModel::BattleshipsModel()
    {
        playerField = new Battlefield;
        enemyField = new Battlefield;

        playerField->onFieldChanged.addListener([&](FieldChanges fc)
                                                { this->currentChanges.player = fc; });
        enemyField->onFieldChanged.addListener([&](FieldChanges fc)
                                               { this->currentChanges.enemy = fc; });
    }

    TileState **BattleshipsModel::getTileField(FieldType fieldType)
    {
        return (fieldType == FieldType::Player)
                   ? playerField->getField()
                   : enemyField->getField();
    }

    bool BattleshipsModel::tryShoot(Vector2i position)
    {
        if (enemyField->tryShoot(position))
            return false;
        playerField->shootRandom();
        onFieldChanged.invoke(currentChanges);
        return true;
    }

    BattleshipsModel::~BattleshipsModel()
    {
        delete playerField;
        delete enemyField;
    }
} // namespace bs
