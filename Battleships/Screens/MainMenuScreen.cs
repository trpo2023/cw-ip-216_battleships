using System;
using Battleships.Foundation;
using Microsoft.Xna.Framework;

namespace Battleships.Screens;

public class MainMenuScreen : Screen
{
    public MainMenuScreen(Navigator navigator)
        : base(navigator) { }

    protected override void SpritesInit()
    {
        base.SpritesInit();
        _spriteList.Add(new Sprite(SpriteName.MenuBackground, new Vector2(0, 0)));
        _spriteList.Add(new Sprite(SpriteName.MenuTitle, new Vector2(350, 30)));
        _spriteList.Add(new Sprite(SpriteName.PlayButton, new Vector2(275, 175)));
        _spriteList.Add(new Sprite(SpriteName.ExitButton, new Vector2(275, 375)));
    }

    protected override void EventInit()
    {
        base.EventInit();
        _events.Add(
            new ScreenEvent(
                new Rectangle(275, 175, 650, 150),
                (Point touchPoint) =>
                {
                    _navigator.StartGame();
                }
            )
        );
        _events.Add(
            new ScreenEvent(
                new Rectangle(275, 375, 650, 150),
                (Point touchPoint) =>
                {
                    _navigator.Exit();
                }
            )
        );
    }
}
