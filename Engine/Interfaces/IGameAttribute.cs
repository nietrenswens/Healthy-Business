using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Engine.Interfaces
{
    public interface IGameAttribute
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Load(ContentManager content);
        public void Update(GameTime gameTime);
        public void Unload();
    }
}
