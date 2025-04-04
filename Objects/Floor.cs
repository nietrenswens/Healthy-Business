using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Floor : GameObject
    {
        private Texture2D _texture;

        public Floor(TileLocation tileLocation)
        {
            WorldPosition = tileLocation.ToVector2();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("objects\\floor");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
