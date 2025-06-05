using HealthyBusiness.Engine.Levels;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Objects.Doors;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Engine.Managers
{
    public class LevelManager
    {
        private Level? _nextLevel;
        private TileLocation? _playerSpawnLocation;
        private Scene _scene;

        public List<Level> Levels { get; private set; }
        public Level Currentlevel { get; private set; } = null!;
        public bool HasNextLevel => _nextLevel != null;

        public TileLocation SpawnLocation
        {
            get
            {
                if (_playerSpawnLocation != null)
                    return _playerSpawnLocation;

                var entrance = Currentlevel.Doors.OfType<ExitDoor>().FirstOrDefault();
                if (entrance != null)
                    return entrance.EntitySpawnLocation();
                throw new InvalidOperationException("No player spawn location set and no exit door found in the current level.");
            }
        }

        public LevelManager(Scene scene)
        {
            _scene = scene;
            Levels = new List<Level>();
        }

        public void ScheduleLevelChange(Level level, TileLocation playerSpawnLocation)
        {
            _nextLevel = level;
            _playerSpawnLocation = playerSpawnLocation;
        }

        public void Update(GameTime gameTime)
        {
            if (_nextLevel != null)
            {
                ChangeLevel(_nextLevel);
                _nextLevel = null;
            }
        }

        public void Load(ContentManager contentManager)
        {
            Levels.ForEach(level => level.Load(contentManager));
            Currentlevel = Levels[0];
            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            GameScene currentScene = (GameScene)_scene;
            currentScene.ClearGameObjects();
            _scene.AddGameObject(Currentlevel.GameObjects);
            _scene.AddGameObject(Currentlevel.SavedGameObjects);
        }

        private void ChangeLevel(Level level)
        {
            var player = _scene.GameObjects.OfType<Player>().First();
            SavePersistentGameObjects();
            Currentlevel = level;
            LoadCurrentLevel();
            player.SetFeetPosition(_playerSpawnLocation!);
            _scene.AddGameObject(player);
        }

        private void SavePersistentGameObjects()
        {
            var level = Currentlevel;
            var persistentGameObjects = _scene.GameObjects
                .Where(go => go.IsPersistent)
                .ToList();
            level.SaveGameObjects(persistentGameObjects.ToArray());
        }

    }
}
