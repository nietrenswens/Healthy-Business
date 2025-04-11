using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Engine
{
    public abstract class Level
    {
        public AttributeManager<GameObject> GameObjects { get; private set; }

        public Level()
        {
            GameObjects = new AttributeManager<GameObject>();
        }
        /// <summary>
        /// Used for updating the level.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Update logic for the level
            GameObjects.Update(gameTime);
        }

        /// <summary>
        /// Used for loading the level, this includes adding gameObjects to the level and loading textures.
        /// </summary>
        /// <param name="content"></param>
        public virtual void Load(ContentManager content)
        {
            // Load content for the level
            GameObjects.Load(content);
        }

        /// <summary>
        /// Used for unloading the level, this includes removing gameObjects from the level and unloading textures.
        /// </summary>
        public virtual void Unload()
        {
            // Unload content for the level
            GameObjects.Unload();
        }

        /// <summary>
        /// Used for drawing the level.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw logic for the level
            GameObjects.Draw(spriteBatch);
        }

    }
}
