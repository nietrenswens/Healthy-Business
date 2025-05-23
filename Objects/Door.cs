using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.Player;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace HealthyBusiness.Objects
{
    public class Door : GameObject
    {
        public DoorType DoorType { get; private set; }
        public string DestinationlevelId { get; private set; }

        public Door(TileLocation tileLocation, DoorType doorType, string destinationLevelId)
        {
            DoorType = doorType;
            WorldPosition = tileLocation.ToVector2();
            DestinationlevelId = destinationLevelId;
            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)));
            CollisionGroup = CollisionGroup.Utility;
            Add(collider);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is not Player)
                return;

            if (DoorType == DoorType.Exit)
            {
                GameManager.GetGameManager().ChangeScene(new MainMenu());
                return;
            }

            var player = (Player)other;
            var gameScene = (GameScene)GameManager.GetGameManager().CurrentScene;
            var nextLevel = gameScene.LevelManager.Levels.FirstOrDefault(l => l.Id == DestinationlevelId);

            if (nextLevel == null)
                throw new Exception($"Level with ID {DestinationlevelId} not found.");

            var destinationDoor = nextLevel.Doors.FirstOrDefault(d => d.DoorType == DoorType.Opposite());
            if (destinationDoor == null)
                throw new Exception($"Destination door not found for level {DestinationlevelId}.");

            if (!gameScene.LevelManager.HasNextLevel)
                gameScene.LevelManager.ScheduleLevelChange(nextLevel, destinationDoor.EntitySpawnLocation());
        }

        public TileLocation EntitySpawnLocation()
        {
            TileLocation dest;
            switch (DoorType)
            {
                case DoorType.Left:
                    dest = new TileLocation(TileLocation.X + 1, TileLocation.Y);
                    break;
                case DoorType.Right:
                    dest = new TileLocation(TileLocation.X - 1, TileLocation.Y);
                    break;
                case DoorType.Top:
                    dest = new TileLocation(TileLocation.X, TileLocation.Y + 1);
                    break;
                case DoorType.Bottom:
                    dest = new TileLocation(TileLocation.X, TileLocation.Y - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(DoorType), DoorType, null);
            }
            return dest;
        }
    }

    public enum DoorType
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
        Exit = 4,
    }

    public static class DoorTypeExtensions
    {
        public static DoorType Opposite(this DoorType doorType)
        {
            return doorType switch
            {
                DoorType.Left => DoorType.Right,
                DoorType.Right => DoorType.Left,
                DoorType.Top => DoorType.Bottom,
                DoorType.Bottom => DoorType.Top,
                _ => throw new ArgumentOutOfRangeException(nameof(doorType), doorType, null)
            };
        }
    }
}
