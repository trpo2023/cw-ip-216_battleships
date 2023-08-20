using Microsoft.Xna.Framework;
using static Battleships.Foundation.SpriteManager;

namespace Battleships.Foundation;

public class Sprite
{
    public SpriteName name;
    public Vector2 position;

    public Sprite(SpriteName name, Vector2 position)
    {
        this.name = name;
        this.position = position;
    }
}
