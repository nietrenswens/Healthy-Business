using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

namespace HealthyBusiness.Engine.Managers
{
    public class GameManager
    {
        private static GameManager _gameManager = null!;
        private Level? _nextLevel;
        private Game _game = null!;

        public ContentManager ContentManager { get; private set; } = null!;
        public GraphicsDevice GraphicsDevice { get; private set; } = null!;
        public Random RNG { get; private set; } = null!;
        public Level CurrentLevel { get; private set; } = null!;
        public PauseMenu PauseMenu { get; private set; }

        private GameManager()
        {
            RNG = new();
        }
        


        public static GameManager GetGameManager()
        {
            if (_gameManager == null)
            {
                _gameManager = new GameManager();
            }
            return _gameManager;
        }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice, Game game)
        {
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            CurrentLevel = new MainMenu();
            PauseMenu = new PauseMenu();
            PauseMenu.Load(contentManager);
            _game = game;
        }

        public void Load(ContentManager content)
        {
            CurrentLevel.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            InputManager.GetInputManager().Update();
            if (_nextLevel != null)
            {
                CurrentLevel.Unload();
                CurrentLevel = _nextLevel;
                _nextLevel = null;
                CurrentLevel.Load(ContentManager);
            }
            CurrentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);
        }

        public void ChangeLevel(Level newLevel)
        {
            _nextLevel = newLevel;
        }

        public void Exit()
        {
            _game.Exit();
        }
    }
}
