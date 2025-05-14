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
        private Texture2D _texture = null!;

        public Wall(TileLocation tileLocation)
        {
            WorldPosition = tileLocation.ToVector2();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("objects\\wall");
            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)));
            collider.CollisionGroup = CollisionGroup.Solid;
            Add(collider);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
        }
    }
}
