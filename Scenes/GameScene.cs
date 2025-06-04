using HealthyBusiness.Builders;
using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HealthyBusiness.Scenes
{
    public class GameScene : Scene
    {
        private List<GameObject> _collidableGameObjects { get; set; }
        public AttributeManager<GameObject> GUIObjects { get; private set; } = null!;
        private PauseMenu _pauseMenu = null!;

        public LevelManager LevelManager { get; private set; } = null!;

        public GameScene(GameSceneType gameSceneType)
        {
            _collidableGameObjects = new List<GameObject>();
            GUIObjects = new();
            LevelManager = new(this);
            switch (gameSceneType)
            {
                case GameSceneType.PlayableLevel:
                    LevelManager.AddDefaultLevel();
                    GameManager.GetGameManager().GameData.ShiftCount += 1;
                    break;
                case GameSceneType.Apartment:
                    LevelManager.AddApartment();
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

        public override void Unload()
        {
            base.Unload();
            GUIObjects.Unload();
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
    }

    public enum GameSceneType
    {
        PlayableLevel,
        Apartment
    }
}
