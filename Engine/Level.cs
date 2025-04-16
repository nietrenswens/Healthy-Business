using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public abstract class Level
    {
        public Camera CurrentCamera { get; private set; } = null!;
        public IReadOnlyList<GameObject> GameObjects => _gameObjectsManager.Attributes;
        public IReadOnlyList<GameObject> GameObjectsToBeAdded => _gameObjectsManager.AttributesToBeAdded;
        private AttributeManager<GameObject> _gameObjectsManager { get; set; }

        public Level()
        {
            _gameObjectsManager = new AttributeManager<GameObject>();
        }
        /// <summary>
        /// Used for updating the level.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Update logic for the level
            _gameObjectsManager.Update(gameTime);
        }

        /// <summary>
        /// Used for loading the level, this includes adding gameObjects to the level and loading textures.
        /// </summary>
        /// <param name="content"></param>
        public virtual void Load(ContentManager content)
        {
            // Load content for the level
            _gameObjectsManager.Load(content);
        }

        /// <summary>
        /// Used for unloading the level, this includes removing gameObjects from the level and unloading textures.
        /// </summary>
        public virtual void Unload()
        {
            // Unload content for the level
            _gameObjectsManager.Unload();
        }

        /// <summary>
        /// Used for drawing the level.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw logic for the level
            spriteBatch.Begin(transformMatrix: CurrentCamera.GetWorldTransformationMatrix(), samplerState: SamplerState.PointClamp);
            _gameObjectsManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public virtual void AddGameObject(GameObject gameObject)
        {
            _gameObjectsManager.Add(gameObject);
        }

        public virtual void AddGameObject(GameObject[] gameObjects)
        {
            _gameObjectsManager.Add(gameObjects);
        }

        public virtual void RemoveGameObject(GameObject gameObject)
        {
            _gameObjectsManager.Remove(gameObject);
        }

        public void SetCamera(Camera camera)
        {
            CurrentCamera = camera;
        }

        public IEnumerable<GameObject> GetGameObjects(CollisionGroup cg)
        {
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.CollisionGroup.HasFlag(cg))
                {
                    yield return gameObject;
                }
            }
        }

    }
}
