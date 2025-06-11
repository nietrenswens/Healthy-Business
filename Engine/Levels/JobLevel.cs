using HealthyBusiness.Builders;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures.Enemies.Tomato;
using HealthyBusiness.Objects.Doors;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace HealthyBusiness.Engine.Levels
{
    public class JobLevel : Level
    {
        public string? TopLevelId { get; private set; }
        public string? BottomlevelId { get; private set; }
        public string? LeftLevelId { get; private set; }
        public string? RightLevelId { get; private set; }

        public JobLevel(string pathToMap, string id, string? topLevelId = null, string? bottomLevelId = null, string? leftlevelId = null, string? rightLevelId = null) : base(pathToMap, id)
        {
            TopLevelId = topLevelId;
            BottomlevelId = bottomLevelId;
            LeftLevelId = leftlevelId;
            RightLevelId = rightLevelId;
        }

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
            GenerateGameObjects(contentManager);
        }

        public void GenerateGameObjects(ContentManager contentManager)
        {
            var rng = GameManager.GetGameManager().RNG;

            var floorTilesCount = GameObjects.Where(go => go is Floor).Count();
            int maxItemCount = floorTilesCount switch
            {
                < 20 => 1,
                < 40 => 4,
                < 100 => 5,
                _ => 7,
            };

            SpawnRandomItems(rng.Next(0, maxItemCount));

            int maxEnemyCount = floorTilesCount switch
            {
                < 30 => 0,
                < 60 => 1,
                < 100 => 2,
                _ => 3,
            };
            int enemyCount = rng.Next(0, maxEnemyCount + 1);
            SpawnEnemies(enemyCount);
        }

        public override GameObject[] GetTiles(ContentManager contentManager)
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
                            if (gid >= 0 && gid <= 3)
                            {
                                var doorType = (DoorDirection)gid;
                                string destinationLevelId = "";
                                switch (doorType)
                                {
                                    case DoorDirection.Left:
                                        destinationLevelId = LeftLevelId ?? throw new Exception("Left level ID is not set.");
                                        break;
                                    case DoorDirection.Right:
                                        destinationLevelId = RightLevelId ?? throw new Exception("Right level ID is not set.");
                                        break;
                                    case DoorDirection.Top:
                                        destinationLevelId = TopLevelId ?? throw new Exception("Top level ID is not set.");
                                        break;
                                    case DoorDirection.Bottom:
                                        destinationLevelId = BottomlevelId ?? throw new Exception("Bottom level ID is not set.");
                                        break;
                                }
                                var door = new NavigationalDoor(new TileLocation(tile.X, tile.Y), (DoorDirection)gid, destinationLevelId);
                                gameObjects.Add(door);
                                Doors.Add(door);
                            }

                            if (gid >= 4 && gid <= 7)
                            {
                                var direction = (DoorDirection)(gid - 4);
                                var door = new ExitDoor(new TileLocation(tile.X, tile.Y), direction, () => GameManager.GetGameManager().ChangeScene(new ShiftEndScene()));
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

        private void SpawnEnemies(int number)
        {
            List<GameObject> gameObjects = SavedGameObjects.ToList();
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

                var enemy = new TomatoEnemy(randomTileLocation);
                gameObjects.Add(enemy);
                SavedGameObjects = gameObjects.ToArray();
            }
        }

    }
}
