using HealthyBusiness.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Engine.Managers
{
    public class GameManager
    {
        private static GameManager _gameManager = null!;

        public ContentManager ContentManager { get; private set; } = null!;
        public GraphicsDevice GraphicsDevice { get; private set; } = null!;
        public Random RNG { get; private set; } = null!;
        public Level CurrentLevel { get; private set; } = null!;

        public static GameManager GetGameManager()
        {
            if (_gameManager == null)
            {
                _gameManager = new GameManager();
            }
            return _gameManager;
        }

        private GameManager()
        {
            RNG = new();
        }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            // Initialize game objects here
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            CurrentLevel = new MainMenu();
        }

        public void Load(ContentManager content)
        {
            // Load content here
            CurrentLevel.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            InputManager.GetInputManager().Update();
            CurrentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);
        }
    }
}
