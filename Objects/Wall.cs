using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Wall : GameObject
    {
        private Texture2D _texture;

        public Wall(TileLocation tileLocation)
        {
            CollisionGroup = CollisionGroup.Solid;
            WorldPosition = tileLocation.ToVector2();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("objects\\wall");
            SetCollider(new RectangleCollider(new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE))));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
        }
    }
}
