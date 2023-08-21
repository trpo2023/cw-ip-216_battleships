using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Battleships.Foundation;

public class SpriteManager
{
    private Dictionary<SpriteName, Texture2D> _textures = new();

    private static readonly Dictionary<SpriteName, string> _paths =
        new()
        {
            { SpriteName.EndBackground, "Background/end_background" },
            { SpriteName.GameBackground, "Background/game_background" },
            { SpriteName.MenuBackground, "Background/menu_background" },
            { SpriteName.ExitButton, "Buttons/exit_button" },
            { SpriteName.GoHomeButton, "Buttons/go_home_button" },
            { SpriteName.PlayAgainButton, "Buttons/play_again_button" },
            { SpriteName.PlayButton, "Buttons/play_button" },
            { SpriteName.EnemyField, "Fields/enemy_field" },
            { SpriteName.PlayerField, "Fields/player_field" },
            { SpriteName.LoseTitle, "Labels/lose_title" },
            { SpriteName.MenuTitle, "Labels/menu_title" },
            { SpriteName.WinTitle, "Labels/win_title" },
            { SpriteName.Ship, "Tiles/ship" },
            { SpriteName.ShootDestroy, "Tiles/shoot_destroy" },
            { SpriteName.ShootMiss, "Tiles/shoot_miss" },
            { SpriteName.ShootShip, "Tiles/shoot_ship" },
            { SpriteName.Empty, "Tiles/empty" },
        };

    public void LoadTextures(ContentManager content)
    {
        foreach (SpriteName name in (SpriteName[])Enum.GetValues(typeof(SpriteName)))
        {
            _textures.Add(name, content.Load<Texture2D>(_paths[name]));
        }
    }

    public Texture2D getTexture(SpriteName name)
    {
        return _textures[name];
    }
}
