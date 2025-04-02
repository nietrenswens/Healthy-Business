using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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
        public Camera CurrentCamera { get; private set; }

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
            var player = new Player(new Vector2(2, 2));
            CurrentCamera = new GameObjectCenteredCamera(player, 1f);

            AddGameObject(player);
            AddGameObject(new Wall(new Point(200, 200)));
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
            CheckCollision();
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
            spriteBatch.Begin(transformMatrix: CurrentCamera.GetWorldTransformationMatrix());
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

        public IEnumerable<GameObject> GetGameObjects(CollisionGroup collisionGroups)
        {
            var objects = _gameObjects.Where(_object => _object.CollisionGroup == collisionGroups);
            return objects;
        }

        private void CheckCollision()
        {
            foreach (var gameObject in _gameObjects)
            {
                foreach (var other in _gameObjects)
                {
                    if (gameObject != other && gameObject.Collider != null && other.Collider != null)
                    {
                        if (gameObject.Collider.CheckIntersection(other.Collider))
                        {
                            gameObject.OnCollision(other);
                            other.OnCollision(gameObject);
                        }
                    }
                }
            }
        }
    }
}
