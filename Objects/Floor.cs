using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects
{
    public class Floor : GameObject
    {
        private Tile _tile;

        public Floor(TileLocation tileLocation, Tile tile)
        {
            WorldPosition = tileLocation.ToVector2();
            _tile = tile;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_tile.TileMap.Texture, new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), _tile.SourceRectangle, Color.White);
        }
    }
}
