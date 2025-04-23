using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HealthyBusiness;

public class HealthyBusiness : Game
{
    private GraphicsDeviceManager _graphics = null!;
    private SpriteBatch _spriteBatch = null!;
    private bool _isPaused = false;
    private KeyboardState _previousKeyboardState;


    public HealthyBusiness()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Globals.SCREENWIDTH;
        _graphics.PreferredBackBufferHeight = Globals.SCREENHEIGHT;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GameManager.GetGameManager().Initialize(Content, GraphicsDevice, this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        GameManager.GetGameManager().Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();

        if (currentKeyboardState.IsKeyDown(Keys.Escape) && !_previousKeyboardState.IsKeyDown(Keys.Escape))
        {
            _isPaused = !_isPaused;
        }

        if (_isPaused)
        {
            _previousKeyboardState = currentKeyboardState;
            return;
        }


        GameManager.GetGameManager().Update(gameTime);

        _previousKeyboardState = currentKeyboardState;


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        GameManager.GetGameManager().Draw(_spriteBatch);

        if (_isPaused)
        {
            GameManager.GetGameManager().PauseMenu.Draw(_spriteBatch);
        }

        base.Draw(gameTime);
    }
}
