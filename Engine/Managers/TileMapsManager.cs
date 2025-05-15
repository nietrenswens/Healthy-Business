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

        public void Load(ContentManager contentManager)
        {
            LoadTileMaps(contentManager);
        }

        private void LoadTileMaps(ContentManager contentManager)
        {
            LoadMap("Room_Builder_64x64", 0, contentManager);
            LoadMap("Interiors_64x64_1", 8588, contentManager);
            LoadMap("Interiors_64x64_2", 9644, contentManager);
            LoadMap("Interiors_64x64_3", 10700, contentManager);
            LoadMap("Interiors_64x64_4", 11756, contentManager);
            LoadMap("Interiors_64x64_6", 12812, contentManager);
            LoadMap("Interiors_64x64_7", 13866, contentManager);
            LoadMap("Interiors_64x64_8", 14924, contentManager);
            LoadMap("Interiors_64x64_9", 15980, contentManager);
            LoadMap("Interiors_64x64_10", 17036, contentManager);
            LoadMap("Interiors_64x64_5", 18092, contentManager);
        }

        private void LoadMap(string mapName, int firstGid, ContentManager contentManager)
        {
            Texture2D texture = contentManager.Load<Texture2D>("tilemaps/" + mapName);
            int width = texture.Width / Globals.TILESIZE;
            int height = texture.Height / Globals.TILESIZE;
            _tileMaps.Add(new TileMap(mapName, texture, width, height, firstGid));
        }
    }
}
