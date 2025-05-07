using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects;
using HealthyBusiness.Objects.Creatures;
using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Engine.Utils
{
    public class TileLocation : IEquatable<TileLocation>
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public TileLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public TileLocation(Vector2 realposition) : this((int)realposition.X / Globals.TILESIZE, (int)realposition.Y / Globals.TILESIZE)
        {
        }

        public TileLocation(Point realposition) : this(realposition.ToVector2())
        {
        }

        /// <summary>
        /// Converts the TileLocation to a Vector2 in real coordinates.
        /// </summary>
        /// <returns></returns>
        public Vector2 ToVector2()
        {
            return new Vector2(X * Globals.TILESIZE, Y * Globals.TILESIZE);
        }

        /// <summary>
        /// Converts the TileLocation to a Point in real coordinates.
        /// </summary>
        /// <returns></returns>
        public Point ToPoint()
        {
            return new Point(X * Globals.TILESIZE, Y * Globals.TILESIZE);
        }

        public override string ToString()
        {
            return $"TileLocation({X}, {Y})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public bool Equals(TileLocation? other)
        {
            return other is not null && other.X == X && other.Y == Y;
        }

        public static TileLocation operator +(TileLocation a, TileLocation b)
        {
            return new TileLocation(a.X + b.X, a.Y + b.Y);
        }

        public static TileLocation operator -(TileLocation a, TileLocation b)
        {
            return new TileLocation(a.X - b.X, a.Y - b.Y);
        }

        public static TileLocation operator *(TileLocation a, int b)
        {
            return new TileLocation(a.X * b, a.Y * b);
        }

        public static TileLocation operator /(TileLocation a, int b)
        {
            return new TileLocation(a.X / b, a.Y / b);
        }

        public static bool operator ==(TileLocation? a, TileLocation? b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(TileLocation? a, TileLocation? b)
        {
            if ((a is null && b is not null) || (a is not null && b is null))
                return true;
            if (a is null || b is null)
                return false;

            return !a.Equals(b);
        }

        public static bool IsTileWalkable(TileLocation newLocation)
        {
            var gameManager = GameManager.GetGameManager();
            var gameObjects = gameManager.CurrentLevel.GameObjects;

            var isValidFloor = false;
            foreach (GameObject go in gameObjects)
            {
                if (go is Floor && go.TileLocation == newLocation)
                {
                    isValidFloor = true;
                    continue;
                }
                if (go is Creature && go.TileLocation == newLocation)
                {
                    return false;
                }
            }
            return isValidFloor;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
