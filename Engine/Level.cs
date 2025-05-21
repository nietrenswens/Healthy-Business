using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace HealthyBusiness.Engine
{
    public class Level
    {
        public string PathToMap { get; private set; }
        public List<Door> Doors { get; private set; }
        public string Id { get; private set; }
        public string? TopLevelId { get; private set; }
        public string? BottomlevelId { get; private set; }
        public string? LeftLevelId { get; private set; }
        public string? RightLevelId { get; private set; }

        public GameObject[] GameObjects { get; private set; } = null!;

        public Level(string pathToMap, string id, string? topLevelId = null, string? bottomLevelId = null, string? leftlevelId = null, string? rightLevelId = null)
        {
            PathToMap = pathToMap;
            Doors = new List<Door>();
            Id = id;
            TopLevelId = topLevelId;
            BottomlevelId = bottomLevelId;
            LeftLevelId = leftlevelId;
            RightLevelId = rightLevelId;
        }

        public void Load(ContentManager contentManager)
        {
            GameObjects = GetTiles(contentManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToMap">TMX file containing map info</param>
        /// <returns></returns>
        public GameObject[] GetTiles(ContentManager contentManager)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            GameManager gm = GameManager.GetGameManager();
            var map = new TmxMap(PathToMap);

            foreach (var tileset in map.Tilesets)
            {
                ((GameScene)gm.CurrentScene).TileMapsManager.LoadMap(tileset.Name, tileset.FirstGid - 1, contentManager);
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
                            var tileMap = ((GameScene)gm.CurrentScene).TileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Solid(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    case "floor":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = ((GameScene)gm.CurrentScene).TileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Floor(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    case "utilities":
                        var ts = map.Tilesets.First(ts => ts.Name == "Utilities");
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            gid = gid - ts.FirstGid + 1;

                            if (gid >= 0 && gid <= 3)
                            {
                                var doorType = (DoorType)gid;
                                string destinationLevelId = "";
                                switch (doorType)
                                {
                                    case DoorType.Left:
                                        destinationLevelId = LeftLevelId ?? throw new Exception("Left level ID is not set.");
                                        break;
                                    case DoorType.Right:
                                        destinationLevelId = RightLevelId ?? throw new Exception("Right level ID is not set.");
                                        break;
                                    case DoorType.Top:
                                        destinationLevelId = TopLevelId ?? throw new Exception("Top level ID is not set.");
                                        break;
                                    case DoorType.Bottom:
                                        destinationLevelId = BottomlevelId ?? throw new Exception("Bottom level ID is not set.");
                                        break;
                                }
                                var door = new Door(new TileLocation(tile.X, tile.Y), (DoorType)gid, destinationLevelId);
                                gameObjects.Add(door);
                                Doors.Add(door);
                            }
                        }
                        break;
                    case "decal":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = ((GameScene)gm.CurrentScene).TileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Decal(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    default:
                        break;
                }
            }

            return gameObjects.ToArray();
        }
    }
}
