using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Battleships.Foundation;

public struct ScreenEvent
{
    public Rectangle clickRectangle;
    public delegate void ClickEventHandler(Point touchPoint);
    public ClickEventHandler eventHandler;

    public ScreenEvent(Rectangle clickRectangle, ClickEventHandler eventHandler)
    {
        this.clickRectangle = clickRectangle;
        this.eventHandler = eventHandler;
    }
}
