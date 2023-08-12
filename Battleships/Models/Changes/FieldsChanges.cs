using System.Collections.Generic;

namespace Battleships.Models.Changes;

public struct FieldsChanges
{
    public List<TileChanges> playerChanges;
    public List<TileChanges> enemyChanges;
}
