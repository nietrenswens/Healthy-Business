using HealthyBusiness.Builders;
using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Engine.Managers
{
    public class GameManager
    {
        private static GameManager _gameManager = null!;

        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _collidableGameObjects = new List<GameObject>();
        private List<GameObject> _gameObjectsToBeAdded = new List<GameObject>();
        private List<GameObject> _gameObjectsToBeRemoved = new List<GameObject>();

        public ContentManager ContentManager { get; private set; } = null!;
        public GraphicsDevice GraphicsDevice { get; private set; } = null!;
        public Camera CurrentCamera { get; private set; } = null!;
        public Random RNG { get; private set; }

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
            RNG = new();
        }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            // Initialize game objects here
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            var player = new Player(new TileLocation(4, 4));
            CurrentCamera = new GameObjectCenteredCamera(player, 1f);

            AddGameObjects(LevelBuilder.CreateRectangularLevel(Globals.MAPWIDTH, Globals.MAPHEIGHT));
            SpawnRandomItems(3);
            AddGameObject(player);
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
                if (_collidableGameObjects.Contains(gameObject))
                {
                    _collidableGameObjects.Remove(gameObject);
                }
            }
            _gameObjectsToBeRemoved.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: CurrentCamera.GetWorldTransformationMatrix(), samplerState: SamplerState.PointClamp);
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

        public void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                AddGameObject(gameObject);
                if (!gameObject.CollisionGroup.HasFlag(CollisionGroup.None))
                {
                    _collidableGameObjects.Add(gameObject);
                }
            }
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjectsToBeRemoved.Add(gameObject);
        }

        public IEnumerable<GameObject> GetGameObjects(CollisionGroup collisionGroups)
        {
            var objects = _gameObjects.Where(_object => _object.CollisionGroup == collisionGroups);
            return objects;
        }

        private void SpawnRandomItems(int number)
        {
            for (int i = 0; i < number; i++)
            {
                var currentAndPendingGameObjects = _gameObjects.Concat(_gameObjectsToBeAdded).ToList();
                var floorTiles = currentAndPendingGameObjects.Where(go => go.CollisionGroup.HasFlag(CollisionGroup.Floor)).ToList();
                if (floorTiles.Count == 0)
                {
                    break;
                }
                var randomTileIndex = RNG.Next(0, floorTiles.Count);
                var randomTileLocation = floorTiles[randomTileIndex].TileLocation;

                var item = ItemBuilder.CreateRandomItem(randomTileLocation);
                AddGameObject(item);
            }
        }

        private void CheckCollision()
        {
            foreach (var gameObject in _collidableGameObjects)
            {
                foreach (var other in _collidableGameObjects)
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
