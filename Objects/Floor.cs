using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Floor : TiledGameObject
    {
        private Texture2D _texture;

        public Floor(TileLocation tileLocation) : base(tileLocation)
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("objects\\floor");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
