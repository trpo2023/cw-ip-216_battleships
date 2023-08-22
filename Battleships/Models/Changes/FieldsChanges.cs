using System.Collections.Generic;

namespace Battleships.Models.Changes;

public class FieldsChanges
{
    public List<TileChanges> playerChanges = new();
    public List<TileChanges> enemyChanges = new();

    public void Clear()
    {
        playerChanges.Clear();
        enemyChanges.Clear();
    }
}
