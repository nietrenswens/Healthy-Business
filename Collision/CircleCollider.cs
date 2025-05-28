using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Collision
{
    public class CircleCollider : Collider, IEquatable<CircleCollider>
    {
        public float X;
        public float Y;
        public override float Width => GetBoundingBox().Width;
        public override float Height => GetBoundingBox().Height;
        public override Vector2 Center
        {
            get
            {
                return new Vector2(X, Y);
            }

            set
            {
                X = value.X; Y = value.Y;
            }
        }
        public float Radius;

        private Texture2D _texture;

        /// <summary>
        /// Creates a new Circle object.
        /// </summary>
        /// <param name="x">The X coordinate of the circle's center</param>
        /// <param name="y">The Y coordinate of the circle's center</param>
        /// <param name="radius">The radius of the circle</param>
        public CircleCollider(float x, float y, float radius)
        {
            this.X = x;
            this.Y = y;
            this.Radius = radius;
            _texture = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.Pink });
        }

        /// <summary>
        /// Creates a new Circle object.
        /// </summary>
        /// <param name="center">The coordinates of the circle's center</param>
        /// <param name="radius">The radius of the circle</param>
        public CircleCollider(Vector2 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
            _texture = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.Pink });
        }


        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this Circle.
        /// </summary>
        /// <param name="coordinates">The coordinates to check.</param>
        /// <returns>true if the coordinates are within the circle.</returns>
        public override bool Contains(Vector2 coordinates)
        {
            return (Center - coordinates).Length() < Radius;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Parent != null)
            {
                Center = Parent.WorldPosition;
            }
        }

        /// <summary>
        /// Gets whether or not the Circle intersects another Circle.
        /// </summary>
        /// <param name="other">The Circle to check for intersection.</param>
        /// <returns>true there is any overlap between the two Circles.</returns>
        public override bool Intersects(CircleCollider other)
        {
            var distance = (Center - other.Center).Length();
            return distance < Radius + other.Radius;
        }

        /// <summary>
        /// Gets whether or not the Circle intersects the Rectangle.
        /// </summary>
        /// <param name="other">The Rectangle to check for intersection.</param>
        /// <returns>true there is any overlap between the Circle and the Rectangle.</returns>
        public override bool Intersects(RectangleCollider other)
        {
            var rectCenterCompensation = other.Shape.Center.ToVector2();

            // Calculate how much we should move to get the rectangle center to (0,0)
            var compensatedBottomRight = new Vector2(other.Shape.Right, other.Shape.Bottom) - rectCenterCompensation;
            // Calculate how much we should move to get the circle center to its correct position when the rectangle center is at (0,0)
            var compensatedCircleCenter = Center - rectCenterCompensation;

            // We make the value absolute so we only need to check the bottom right quadrant
            var absCompensatedCircleCenter = new Vector2(Math.Abs(compensatedCircleCenter.X), Math.Abs(compensatedCircleCenter.Y));

            if (absCompensatedCircleCenter.X < compensatedBottomRight.X)
            {
                return absCompensatedCircleCenter.Y - compensatedBottomRight.Y < Radius;
            }
            else if (absCompensatedCircleCenter.Y < compensatedBottomRight.Y)
            {
                return absCompensatedCircleCenter.X - compensatedBottomRight.X < Radius;
            }
            else
            {
                return (compensatedBottomRight - compensatedCircleCenter).Length() < Radius;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Globals.DEBUG)
                spriteBatch.Draw(_texture, GetBoundingBox(), Color.Pink * 0.5f);
        }

        /// <summary>
        /// Gets whether or not the Circle intersects the Line
        /// </summary>
        /// <param name="other">The Line to check for intersection</param>
        /// <returns>true there is any overlap between the Circle and the Line.</returns>
        public override bool Intersects(LinePieceCollider other)
        {
            // Implemented in the line code.
            return other.Intersects(this);
        }

        /// <summary>
        /// Get the enclosing Rectangle that surrounds the Circle.
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetBoundingBox()
        {
            return new Rectangle((int)(X - Radius), (int)(Y - Radius), (int)(2 * Radius), (int)(2 * Radius));
        }

        public bool Equals(CircleCollider? other)
        {
            return other != null && other.X == X && other.Y == Y && other.Radius == Radius;
        }
    }
}
