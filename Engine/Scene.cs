using HealthyBusiness.Cameras;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public abstract class Scene
    {
        private AttributeManager<GameObject> _gameObjectsManager;

        public Camera CurrentCamera { get; private set; } = null!;
        public IReadOnlyList<GameObject> GameObjects => _gameObjectsManager.Attributes;
        public IReadOnlyList<GameObject> GameObjectsToBeAdded => _gameObjectsManager.AttributesToBeAdded;

        public Scene()
        {
            _gameObjectsManager = new AttributeManager<GameObject>();
        }

        /// <summary>
        /// Used for updating the scene.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Update logic for the scene
            _gameObjectsManager.Update(gameTime);
        }

        /// <summary>
        /// Used for loading the scene, this includes adding gameObjects to the scene and loading textures.
        /// </summary>
        /// <param name="content"></param>
        public virtual void Load(ContentManager content)
        {
            // Load content for the scene
            _gameObjectsManager.Load(content);
        }

        /// <summary>
        /// Used for unloading the scene, this includes removing gameObjects from the scene and unloading textures.
        /// </summary>
        public virtual void Unload()
        {
            // Unload content for the scene
            _gameObjectsManager.Unload();
        }

        /// <summary>
        /// Used for drawing the scene.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw logic for the scene
            spriteBatch.Begin(transformMatrix: CurrentCamera.GetWorldTransformationMatrix(), samplerState: SamplerState.PointClamp);
            _gameObjectsManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        /// <summary>
        /// Adds a game object to the scene.
        /// </summary>
        /// <param name="gameObject"></param>
        public virtual void AddGameObject(GameObject gameObject)
        {
            _gameObjectsManager.Add(gameObject);
        }

        /// <summary>
        /// Adds gameobjects to the scene.
        /// </summary>
        /// <param name="gameObjects"></param>
        public virtual void AddGameObject(GameObject[] gameObjects)
        {
            _gameObjectsManager.Add(gameObjects);
        }

        /// <summary>
        /// Removes a game object from the scene.
        /// </summary>
        /// <param name="gameObject"></param>
        public virtual void RemoveGameObject(GameObject gameObject)
        {
            _gameObjectsManager.Remove(gameObject);
        }

        /// <summary>
        /// Sets the camera for the scene.
        /// </summary>
        /// <param name="camera"></param>
        public void SetCamera(Camera camera)
        {
            CurrentCamera = camera;
        }

        /// <summary>
        /// Returns all game objects in the scene that are in the specified collision group.
        /// </summary>
        /// <param name="collisionGroup"></param>
        /// <returns></returns>
        public IEnumerable<GameObject> GetGameObjects(CollisionGroup collisionGroup)
        {
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.CollisionGroup.HasFlag(collisionGroup))
                {
                    yield return gameObject;
                }
            }
        }
    }
}
