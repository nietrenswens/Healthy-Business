using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.Doors;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace HealthyBusiness.Engine.Levels
{
    public abstract class Level
    {
        protected TileMapsManager _tileMapsManager;
        public string PathToMap { get; protected set; }
        public List<Door> Doors { get; protected set; }
        public string Id { get; protected set; }

        public GameObject[] GameObjects { get; protected set; }
        public GameObject[] SavedGameObjects { get; protected set; }

        public Level(string pathToMap, string id)
        {
            _tileMapsManager = new TileMapsManager();
            PathToMap = pathToMap;
            Doors = new List<Door>();
            Id = id;

            GameObjects = [];
            SavedGameObjects = [];
        }

        public virtual void Load(ContentManager contentManager)
        {
            GameObjects = GetTiles(contentManager);
        }

        public void SaveGameObjects(GameObject[] gameObjects)
        {
            SavedGameObjects = gameObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToMap">TMX file containing map info</param>
        /// <returns></returns>
        public abstract GameObject[] GetTiles(ContentManager contentManager);

    }
}
