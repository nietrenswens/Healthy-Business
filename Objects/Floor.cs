using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Floor : GameObject
    {
        private Texture2D _texture;
        public Floor(Point position)
        {
            WorldPosition = position.ToVector2();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("objects\\floor");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(_texture.Width, _texture.Height)), Color.White);
        }
    }
}
