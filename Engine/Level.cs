using HealthyBusiness.Builders;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace HealthyBusiness.Engine
{
    public class Level
    {
        private TileMapsManager _tileMapsManager;
        public string PathToMap { get; private set; }
        public List<Door> Doors { get; private set; }
        public string Id { get; private set; }
        public string? TopLevelId { get; private set; }
        public string? BottomlevelId { get; private set; }
        public string? LeftLevelId { get; private set; }
        public string? RightLevelId { get; private set; }

        public GameObject[] GameObjects { get; private set; }
        public GameObject[] SavedGameObjects { get; private set; }

        public Level(string pathToMap, string id, string? topLevelId = null, string? bottomLevelId = null, string? leftlevelId = null, string? rightLevelId = null)
        {
            _tileMapsManager = new TileMapsManager();
            PathToMap = pathToMap;
            Doors = new List<Door>();
            Id = id;
            TopLevelId = topLevelId;
            BottomlevelId = bottomLevelId;
            LeftLevelId = leftlevelId;
            RightLevelId = rightLevelId;

            GameObjects = [];
            SavedGameObjects = [];
        }

        public void Load(ContentManager contentManager)
        {
            GenerateGameObjects(contentManager);
        }

        public void SaveGameObjects(GameObject[] gameObjects)
        {
            SavedGameObjects = gameObjects;
        }

        public void GenerateGameObjects(ContentManager contentManager)
        {
            GameObjects = GetTiles(contentManager);
            var rng = GameManager.GetGameManager().RNG;

            var floorTilesCount = GameObjects.Where(go => go is Floor).Count();
            SpawnRandomItems(rng.Next(floorTilesCount / 16, floorTilesCount / 8));
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

            // Load map file
            var map = new TmxMap(PathToMap);

            // Load tilesets and textures.
            foreach (var tileset in map.Tilesets)
            {
                _tileMapsManager.LoadMap(tileset.Name, tileset.FirstGid - 1, contentManager);
            }

            foreach (var layer in map.Layers)
            {
                // Look through layers, find type and set object type accordingly.
                var type = layer.Properties["type"];
                switch (type)
                {
                    case "solid":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = _tileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Solid(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    case "floor":
                        foreach (var tile in layer.Tiles)
                        {
                            var gid = tile.Gid - 1;
                            if (gid == -1)
                                continue;
                            var tileMap = _tileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
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

                            // For utilities, there is more advanced logic as these wont be displayed but will be used for game objects.
                            if (gid >= 0 && gid <= 4)
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
                                    case DoorType.Exit:
                                        destinationLevelId = "exit";
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
                            var tileMap = _tileMapsManager.TileMaps.First(ts => ts.FirstGid <= gid && ts.FirstGid + ts.Tiles.Count > gid);
                            gameObjects.Add(new Decal(new TileLocation(tile.X, tile.Y), tileMap.Tiles[gid]));
                        }
                        break;
                    default:
                        break;
                }
            }

            return gameObjects.ToArray();
        }

        private void SpawnRandomItems(int number)
        {
            List<GameObject> gameObjects = new();
            var floorTiles = GameObjects.Where(go => go is Floor).ToList();
            HashSet<TileLocation> usedLocations = new HashSet<TileLocation>();

            if (floorTiles.Count == 0)
            {
                return;
            }

            if (number >= floorTiles.Count / 2)
                number = floorTiles.Count / 2;

            for (int i = 0; i < number; i++)
            {
                int retries = 0;
                if (retries > 4)
                    break;

                var randomTileIndex = GameManager.GetGameManager().RNG.Next(0, floorTiles.Count);
                var randomTileLocation = floorTiles[randomTileIndex].TileLocation;

                while (usedLocations.Contains(randomTileLocation))
                {
                    randomTileIndex = GameManager.GetGameManager().RNG.Next(0, floorTiles.Count);
                    randomTileLocation = floorTiles[randomTileIndex].TileLocation;
                    retries++;
                }

                var item = ItemBuilder.CreateRandomItem(randomTileLocation);
                gameObjects.Add(item);
                SavedGameObjects = gameObjects.ToArray();
            }
        }

    }
}
