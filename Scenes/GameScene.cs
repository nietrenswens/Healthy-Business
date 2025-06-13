using HealthyBusiness.Builders;
using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HealthyBusiness.Scenes
{
    public class GameScene : Scene
    {
        private GameSceneType _gameSceneType;
        private List<GameObject> _collidableGameObjects { get; set; }
        private PauseMenu _pauseMenu = null!;
        private SoundEffectInstance? _backgroundSound;

        public AttributeManager<GameObject> GUIObjects { get; private set; } = null!;
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

            var backgroundSound = content.Load<SoundEffect>("audio\\whiteNoise");
            GameManager.GetGameManager().PlayLoopingMusic(backgroundSound, 0.03f);

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
            var inputManager = InputManager.GetInputManager();

            if (inputManager.IsKeyPressed(Keys.Escape))
            {
                _pauseMenu.Toggle();
            }

            if (!_pauseMenu.IsPaused)
            {
                CheckCollision();
                base.Update(gameTime);
                LevelManager.Update(gameTime);
            }

            GUIObjects.Update(gameTime);

            if (_pauseMenu.IsPaused)
            {
                _pauseMenu.Update(gameTime);
            }

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
