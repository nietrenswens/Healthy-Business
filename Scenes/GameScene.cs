using HealthyBusiness.Engine;
using HealthyBusiness.Cameras;
using Microsoft.Xna.Framework;
using HealthyBusiness.Builders;
using HealthyBusiness.Collision;
using System.Collections.Generic;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures.PlayerCreature;

namespace HealthyBusiness.Scenes
{
    public class GameScene : Scene
    {
        private GameSceneType _gameSceneType;
        private List<GameObject> _collidableGameObjects { get; set; }
        public AttributeManager<GameObject> GUIObjects { get; private set; } = null!;
        private PauseMenu _pauseMenu = null!;

        public LevelManager LevelManager { get; private set; } = null!;
       
        public GameScene(GameSceneType gameSceneType)
        {
            _collidableGameObjects = new List<GameObject>();
            GUIObjects = new();
            LevelManager = new(this);
            _gameSceneType = gameSceneType;
            switch (gameSceneType)
            {
                case GameSceneType.PlayableLevel:
                    LevelManager.AddDefaultLevel();
                    break;
                case GameSceneType.Apartment:
                    LevelManager.AddApartment();
                    GUIObjects.Add(new QuotaText());
                    break;
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            LevelManager.Load(content);
            var player = new Player(LevelManager.SpawnLocation);
            player.SetFeetPosition(LevelManager.SpawnLocation);
            SetCamera(new GameObjectCenteredCamera(player, 1f));
            AddGameObject(player);
            var hotbarslots = GameManager.GetGameManager().GameData.HotbarSlots;
            GUIObjects.Add(new Hotbar(hotbarslots));
            _pauseMenu = new PauseMenu();
            _pauseMenu.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            CheckCollision();
            GUIObjects.Update(gameTime);
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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GUIObjects.Draw(spriteBatch);
            spriteBatch.End();
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

        public void ClearGameObjects()
        {
            base.Unload();
            _collidableGameObjects.Clear();
        }

        public override void Unload()
        {
            base.Unload();
            GUIObjects.Unload();
            _collidableGameObjects.Clear();
        }

        private void CheckCollision()
        {
            HashSet<GameObject> checkedGameObjects = new HashSet<GameObject>();
            foreach (var gameObject in _collidableGameObjects)
            {
                foreach (var other in _collidableGameObjects)
                {
                    var collider = gameObject.GetGameObject<Collider>();
                    if (gameObject != other && collider != null && other.GetGameObject<Collider>() != null)
                    {
                        if (checkedGameObjects.Contains(gameObject) || checkedGameObjects.Contains(other))
                        {
                            continue; // Skip if already checked
                        }
                        if (gameObject.GetGameObject<Collider>()!.CheckIntersection(other.GetGameObject<Collider>()!))
                        {
                            gameObject.OnCollision(other);
                            other.OnCollision(gameObject);

                            checkedGameObjects.Add(gameObject);
                            checkedGameObjects.Add(other);
                        }
                    }
                }
            }
        }
    }

    public enum GameSceneType
    {
        PlayableLevel,
        Apartment
    }
}
