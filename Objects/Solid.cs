using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Solid : GameObject
    {
        private Tile _tile;

        public Solid(TileLocation tileLocation, Tile tile)
        {
            WorldPosition = tileLocation.ToVector2();
            _tile = tile;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
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
            spriteBatch.Draw(_tile.TileMap.Texture, new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), _tile.SourceRectangle, Color.White);
        }
    }
}
