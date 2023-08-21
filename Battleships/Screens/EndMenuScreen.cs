using Battleships.Foundation;
using Microsoft.Xna.Framework;

namespace Battleships.Screens;

public class EndMenuScreen : Screen
{
    private bool _isPlayerWin = true;

    public EndMenuScreen(Navigator navigator, bool isPlayerWin)
        : base(navigator)
    {
        _isPlayerWin = isPlayerWin;
    }

    protected override void SpritesInit()
    {
        base.SpritesInit();
        if (_isPlayerWin)
            _spriteList.Add(new Sprite(SpriteName.WinTitle, new Vector2(450, 50)));
        else
            _spriteList.Add(new Sprite(SpriteName.LoseTitle, new Vector2(450, 50)));

        _spriteList.Add(new Sprite(SpriteName.EndBackground, new Vector2(0, 0)));
        _spriteList.Add(new Sprite(SpriteName.PlayAgainButton, new Vector2(200, 200)));
        _spriteList.Add(new Sprite(SpriteName.GoHomeButton, new Vector2(750, 200)));
    }

    protected override void EventInit()
    {
        base.EventInit();
        _events.Add(
            new ScreenEvent(
                new Rectangle(200, 200, 250, 250),
                (_) =>
                {
                    _navigator.StartGame();
                }
            )
        );
        _events.Add(
            new ScreenEvent(
                new Rectangle(750, 200, 250, 250),
                (_) =>
                {
                    _navigator.StartMainMenu();
                }
            )
        );
    }
}
