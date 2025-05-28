
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Engine
{
    public class Tile
    {
        public TileMap TileMap { get; private set; }

        public int Gid { get; private set; }
        public Rectangle SourceRectangle { get; private set; }

        public Tile(TileMap tileMap, int gid, Rectangle sourceRectangle)
        {
            TileMap = tileMap;
            Gid = gid;
            SourceRectangle = sourceRectangle;
        }
    }
}
