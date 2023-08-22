using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Battleships.Screens;
using Battleships.Foundation;
using Microsoft.Xna.Framework.Input;

namespace Battleships;

public class MainGame : Game, Navigator
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteManager _spriteManager = new();
    private Screen _currentScreen;

    private bool _clickTrigger = false;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 600,
            PreferredBackBufferWidth = 1200
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        StartEndScreen(false);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteManager.LoadTextures(Content);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && !_clickTrigger)
        {
            _clickTrigger = true;
            foreach (var ev in _currentScreen.Events)
            {
                if (ev.clickRectangle.Contains(Mouse.GetState().Position))
                    ev.eventHandler(Mouse.GetState().Position);
            }
        }

        if (Mouse.GetState().LeftButton == ButtonState.Released && _clickTrigger)
            _clickTrigger = false;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach (var sprite in _currentScreen.SpriteList)
            _spriteBatch.Draw(_spriteManager.getTexture(sprite.name), sprite.position, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void StartMainMenu()
    {
        _currentScreen = new MainMenuScreen(this);
        _currentScreen.Start();
    }

    public void StartGame()
    {
        _currentScreen = new GameScreen(this);
        _currentScreen.Start();
    }

    public void StartEndScreen(bool isPlayerWin)
    {
        _currentScreen = new EndMenuScreen(this, isPlayerWin);
        _currentScreen.Start();
    }
}
