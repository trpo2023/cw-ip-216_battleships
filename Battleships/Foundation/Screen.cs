using System.Collections.Generic;

namespace Battleships.Foundation;

public abstract class Screen
{
    protected List<Sprite> _spriteList = new();
    protected List<ScreenEvent> _events = new();
    protected Navigator _navigator;

    public List<Sprite> SpriteList => _spriteList;
    public List<ScreenEvent> Events => _events;

    protected virtual void SpritesInit() { }

    protected virtual void EventInit() { }

    public Screen(Navigator navigator)
    {
        _navigator = navigator;
        SpritesInit();
        EventInit();
    }
}
