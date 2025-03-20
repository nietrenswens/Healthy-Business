using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine.Managers
{
    public class GameManager
    {
        private static GameManager _gameManager;

        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _gameObjectsToBeAdded = new List<GameObject>();
        private List<GameObject> _gameObjectsToBeRemoved = new List<GameObject>();

        public ContentManager ContentManager { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

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
            _gameObjects = new();
        }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            // Initialize game objects here
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;

            AddGameObject(new Player(new Vector2(100, 100)));
        }

        public void Load(ContentManager content)
        {
            // Load content here
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Load(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            InputManager.GetInputManager().Update();
            foreach (var gameObject in _gameObjectsToBeAdded)
            {
                _gameObjects.Add(gameObject);
            }
            _gameObjectsToBeAdded.Clear();

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }

            foreach (var gameObject in _gameObjectsToBeRemoved)
            {
                _gameObjects.Remove(gameObject);
            }
            _gameObjectsToBeRemoved.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Draw game objects here
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.Load(ContentManager);
            _gameObjectsToBeAdded.Add(gameObject);
        }
    }
}
