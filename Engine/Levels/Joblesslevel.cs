using HealthyBusiness.Data;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures.Employee;
using HealthyBusiness.Objects.Doors;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

namespace HealthyBusiness.Engine.Levels
{
    public class JoblessLevel : Level
    {

        public JoblessLevel(string pathToMap, string id) : base(pathToMap, id)
        {
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
                                throw new Exception("Navigation doors are not allowed in JoblessLevel.");
                            }

                            if (gid >= 4 && gid <= 7)
                            {
                                GameData gameData = GameManager.GetGameManager().GameData;
                                bool gameOver = false;

                                if(gameData.ShiftCount >= gameData.Quota.Deadline && gameData.Balance < gameData.Quota.amount)
                                {
                                    gameOver = true;
                                }

                                var direction = (DoorDirection)(gid - 4);

                                var door = new ExitDoor(new TileLocation(tile.X, tile.Y), direction, () => GameManager.GetGameManager().ChangeScene(
                                    (gameOver) ? new GameOverScene() : new LoadingScene()
                                ));

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

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
            SpawnTheDam();
        }

        private void SpawnTheDam()
        {
            Employer dam = new Employer(new TileLocation(10, 7));
            dam.Load(GameManager.GetGameManager().ContentManager);
            GameManager gm = GameManager.GetGameManager();

            GameScene currentScene = gm.CurrentScene as GameScene;

            List<GameObject> GameObjectsList = new List<GameObject>();
            
            if (currentScene != null)
            {
                GameObjectsList.Add(dam);
            }
            else
            {
                throw new Exception("Current scene is not a GameScene.");
            }

            SavedGameObjects = GameObjectsList.ToArray();
        }
    }
}
