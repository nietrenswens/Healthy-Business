using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine.Managers
{
    public class TileMapsManager
    {
        private List<TileMap> _tileMaps;

        public IReadOnlyList<TileMap> TileMaps => _tileMaps;

        public TileMapsManager()
        {
            _tileMaps = new();
        }

        public void LoadMap(string mapName, int firstGid, ContentManager contentManager)
        {
            Texture2D texture = contentManager.Load<Texture2D>("tilemaps/" + mapName);
            int width = texture.Width / Globals.TILESIZE;
            int height = texture.Height / Globals.TILESIZE;
            _tileMaps.Add(new TileMap(mapName, texture, width, height, firstGid));
        }
    }
}
