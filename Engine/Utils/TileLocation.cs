using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HealthyBusiness.Engine.Utils
{
    public class TileLocation : GameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public TileLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public TileLocation(Vector2 realposition) : this((int)realposition.X, (int)realposition.Y)
        {
        }

        public TileLocation(Point realposition) : this(realposition.ToVector2())
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (Parent != null)
                Parent.WorldPosition = new Vector2(X * Globals.TILESIZE, Y * Globals.TILESIZE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Parent != null)
            {
                // Update the TileLocation based on the parent's position
                var parentPosition = Parent.WorldPosition;
                X = (int)(parentPosition.X / Globals.TILESIZE);
                Y = (int)(parentPosition.Y / Globals.TILESIZE);
            }
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
