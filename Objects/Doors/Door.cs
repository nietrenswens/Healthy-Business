using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Objects.Doors
{
    public abstract class Door : GameObject
    {
        public DoorDirection DoorDirection { get; private set; }

        public Door(TileLocation tileLocation, DoorDirection doorDirection)
        {
            DoorDirection = doorDirection;
            WorldPosition = tileLocation.ToVector2();
            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)));
            CollisionGroup = CollisionGroup.Utility;
            Add(collider);
        }

        public TileLocation EntitySpawnLocation()
        {
            TileLocation dest;
            switch (DoorDirection)
            {
                case DoorDirection.Left:
                    dest = new TileLocation(TileLocation.X + 1, TileLocation.Y);
                    break;
                case DoorDirection.Right:
                    dest = new TileLocation(TileLocation.X - 1, TileLocation.Y);
                    break;
                case DoorDirection.Top:
                    dest = new TileLocation(TileLocation.X, TileLocation.Y + 1);
                    break;
                case DoorDirection.Bottom:
                    dest = new TileLocation(TileLocation.X, TileLocation.Y - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(DoorDirection), DoorDirection, null);
            }
            return dest;
        }
    }

    public enum DoorDirection
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
    }

    public static class DoorDirectionExtensions
    {
        public static DoorDirection Opposite(this DoorDirection doorType)
        {
            return doorType switch
            {
                DoorDirection.Left => DoorDirection.Right,
                DoorDirection.Right => DoorDirection.Left,
                DoorDirection.Top => DoorDirection.Bottom,
                DoorDirection.Bottom => DoorDirection.Top,
                _ => throw new ArgumentOutOfRangeException(nameof(doorType), doorType, null)
            };
        }
    }
}
