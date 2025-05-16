using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Levels;
using HealthyBusiness.Objects;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace HealthyBusiness.Engine
{
    public static class LevelLoader
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToMap">TMX file containing map info</param>
        /// <returns></returns>
        public static GameObject[] LoadMap(string pathToMap, ContentManager contentManager)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            GameManager gm = GameManager.GetGameManager();
            var map = new TmxMap(pathToMap);

            foreach (var tileset in map.Tilesets)
            {
                ((GameLevel)gm.CurrentLevel).TileMapsManager.LoadMap(tileset.Name, tileset.FirstGid - 1, contentManager);
            }

            foreach (var layer in map.Layers)
            {
                var type = layer.Properties["type"];
                switch (type)
                {
                    case "solid":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = ((GameLevel)gm.CurrentLevel).TileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Solid(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    case "floor":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = ((GameLevel)gm.CurrentLevel).TileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Floor(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    default:
                        continue;
                }
            }

            return gameObjects.ToArray();
        }
    }
}
