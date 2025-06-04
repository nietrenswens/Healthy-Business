using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
using System;
using System.Linq;

namespace HealthyBusiness.Objects.Doors
{
    public class NavigationalDoor : Door
    {
        public string DestinationlevelId { get; private set; }

        public NavigationalDoor(TileLocation tileLocation, DoorDirection doorDirection, string destinationLevelId) : base(tileLocation, doorDirection)
        {
            DestinationlevelId = destinationLevelId;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is not Player)
                return;

            var player = (Player)other;
            var gameScene = (GameScene)GameManager.GetGameManager().CurrentScene;
            var nextLevel = gameScene.LevelManager.Levels.FirstOrDefault(l => l.Id == DestinationlevelId);

            if (nextLevel == null)
                throw new Exception($"Level with ID {DestinationlevelId} not found.");

            var destinationDoor = nextLevel.Doors.FirstOrDefault(d => d.DoorDirection == DoorDirection.Opposite());
            if (destinationDoor == null)
                throw new Exception($"Destination door not found for level {DestinationlevelId}.");

            if (!gameScene.LevelManager.HasNextLevel)
                gameScene.LevelManager.ScheduleLevelChange(nextLevel, destinationDoor.EntitySpawnLocation());
        }
    }
}
