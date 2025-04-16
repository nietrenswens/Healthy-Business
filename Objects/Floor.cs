using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Floor : GameObject
    {
        private Texture2D _texture = null!;

        public Floor(TileLocation tileLocation)
        {
            WorldPosition = tileLocation.ToVector2();
            CollisionGroup = Collision.CollisionGroup.None | Collision.CollisionGroup.Floor;
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
