using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public abstract class GameObject
    {
        public GameObject Parent { get; set; }
        public List<GameObject> Components { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                component.Draw(spriteBatch);
            }
        }

        public virtual void Load(ContentManager content)
        {
            foreach (var component in Components)
            {
                component.Load(content);
            }
        }
    }
}
