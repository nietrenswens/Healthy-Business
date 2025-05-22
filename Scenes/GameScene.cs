using HealthyBusiness.Builders;
using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Scenes
{
    public class GameScene : Scene
    {
        private Level? _nextLevel;
        private Vector2? _playerSpawnLocation;
        private List<GameObject> _collidableGameObjects { get; set; }
        private PauseMenu _pauseMenu = null!;
        
        public TileMapsManager TileMapsManager { get; set; }
        public List<Level> Levels { get; private set; }
        public Level Currentlevel = null!;
        public bool HasNextLevel => _nextLevel != null;
        public LevelManager LevelManager { get; private set; }

        public GameScene()
        {
            _collidableGameObjects = new List<GameObject>();
            LevelManager = new(this);
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            LevelManager.Load(content);
            var player = new Player(new TileLocation(1, 4));
            SetCamera(new GameObjectCenteredCamera(player, 1f));
            SpawnRandomItems(5);
            AddGameObject(player);
            _pauseMenu = new PauseMenu();
            _pauseMenu.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            CheckCollision();
            _pauseMenu.Update(gameTime);

            if (!_pauseMenu.IsPaused)
            {
                base.Update(gameTime);
            }

            LevelManager.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _pauseMenu.Draw(spriteBatch);
        }

        public override void AddGameObject(GameObject gameObject)
        {
            base.AddGameObject(gameObject);
            var hasFlag = gameObject.CollisionGroup == CollisionGroup.None;
            if (!hasFlag)
            {
                _collidableGameObjects.Add(gameObject);
            }
        }

        public override void AddGameObject(GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.AddGameObject(gameObject);
            }
        }

        public override void RemoveGameObject(GameObject gameObject)
        {
            base.RemoveGameObject(gameObject);
            if (_collidableGameObjects.Contains(gameObject))
            {
                _collidableGameObjects.Remove(gameObject);
            }
        }

        public override void Unload()
        {
            base.Unload();
            _collidableGameObjects.Clear();
        }

        private void CheckCollision()
        {
            foreach (var gameObject in _collidableGameObjects)
            {
                foreach (var other in _collidableGameObjects)
                {
                    var collider = gameObject.GetGameObject<Collider>();
                    if (gameObject != other && collider != null && other.GetGameObject<Collider>() != null)
                    {
                        if (collider.CollisionGroup == CollisionGroup.Utility)
                            Console.WriteLine();
                        if (gameObject.GetGameObject<Collider>()!.CheckIntersection(other.GetGameObject<Collider>()!))
                        {
                            gameObject.OnCollision(other);
                            other.OnCollision(gameObject);
                        }
                    }
                }
            }
        }

        private void SpawnRandomItems(int number)
        {
            for (int i = 0; i < number; i++)
            {
                var floorTiles = GameObjects.ToList().Concat(GameObjectsToBeAdded)
                    .Where(go => go is Floor).ToList();
                if (floorTiles.Count == 0)
                {
                    break;
                }
                var randomTileIndex = GameManager.GetGameManager().RNG.Next(0, floorTiles.Count);
                var randomTileLocation = floorTiles[randomTileIndex].TileLocation;

                var item = ItemBuilder.CreateRandomItem(randomTileLocation);
                AddGameObject(item);
            }
        }
    }
}
