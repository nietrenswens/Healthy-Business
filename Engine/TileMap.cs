using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public class TileMap
    {
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int FirstGid { get; private set; }
        public Texture2D Texture { get; private set; }
        public Dictionary<int, Tile> Tiles { get; private set; } = new();

        public TileMap(string name, Texture2D texture, int width, int height, int firstGid)
        {
            Name = name;
            Width = width;
            Height = height;
            FirstGid = firstGid;
            Texture = texture;
            ImportRectangles();
        }

        private void ImportRectangles()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int gid = FirstGid + (i * Width) + j;
                    Tiles.Add(gid, new Tile(this, gid, new Rectangle(j * Globals.TILESIZE, i * Globals.TILESIZE, Globals.TILESIZE, Globals.TILESIZE)));
                }
            }
        }

    }
}
