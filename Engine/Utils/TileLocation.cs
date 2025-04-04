using Microsoft.Xna.Framework;

namespace HealthyBusiness.Engine.Utils
{
    public class TileLocation
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
    }
}
