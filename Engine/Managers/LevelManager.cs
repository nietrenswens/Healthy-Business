using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public LevelManager(Scene scene)
        {
            _scene = scene;
            Levels = new List<Level>();
            Levels.Add(new("Maps\\test\\order_room.tmx", "order_room", bottomLevelId: "restroom", topLevelId: "kitchen", rightLevelId: "party_room"));
            Levels.Add(new("Maps\\test\\restroom.tmx", "restroom", topLevelId: "order_room"));
            Levels.Add(new("Maps\\test\\kitchen.tmx", "kitchen", bottomLevelId: "order_room"));
            Levels.Add(new("Maps\\test\\party_room.tmx", "party_room", leftlevelId: "order_room"));
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
            _scene.AddGameObject(Currentlevel.GameObjects);
            _scene.AddGameObject(Currentlevel.SavedGameObjects);
        }
        private void ChangeLevel(Level level)
        {
            var player = _scene.GameObjects.OfType<Player>().First();
            SavePersistentGameObjects();
            _scene.Unload();
            Currentlevel = level;
            _scene.AddGameObject(Currentlevel.GameObjects);
            _scene.AddGameObject(Currentlevel.SavedGameObjects);
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
