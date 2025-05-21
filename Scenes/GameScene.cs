using HealthyBusiness.Builders;
using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures.Enemies.Tomato;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Scenes
{
    public class GameScene : Scene
    {
        private List<GameObject> _collidableGameObjects { get; set; }
        private PauseMenu _pauseMenu = null!;
        
        public TileMapsManager TileMapsManager { get; set; }

        public GameScene()
        {
            _collidableGameObjects = new List<GameObject>();
            TileMapsManager = new TileMapsManager();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            var player = new Player(new TileLocation(4, 6));
            SetCamera(new GameObjectCenteredCamera(player, 1f));
            AddGameObject(LevelLoader.LoadMap("Maps/test/test.tmx", content));
            SpawnRandomItems(5);
            AddGameObject(player);
            AddGameObject(new TomatoEnemy(new(15, 5)));
            AddGameObject(new TomatoEnemy(new(16, 9)));
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _pauseMenu.Draw(spriteBatch);
        }

        public override void AddGameObject(GameObject gameObject)
        {
            base.AddGameObject(gameObject);
            if (!gameObject.GetGameObject<Collider>()?.CollisionGroup.HasFlag(CollisionGroup.None) ?? false)
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

        private void CheckCollision()
        {
            foreach (var gameObject in _collidableGameObjects)
            {
                foreach (var other in _collidableGameObjects)
                {
                    if (gameObject != other && gameObject.GetGameObject<Collider>() != null && other.GetGameObject<Collider>() != null)
                    {
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
