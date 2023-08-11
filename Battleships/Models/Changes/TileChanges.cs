using System.Numerics;
using Battleships.Models.Primitive;

namespace Battleships.Models.Changes;

public struct TileChanges
{
    public TileState tileState;
    public Vector2i position;

    public TileChanges(TileState tileState, Vector2i position)
    {
        this.tileState = tileState;
        this.position = position;
    }
}
