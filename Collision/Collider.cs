using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Collision
{
    public abstract class Collider : GameObject
    {
        public CollisionGroup CollisionGroup { get; set; } = CollisionGroup.None;

        public abstract float Width { get; }
        public abstract float Height { get; }
        public abstract Vector2 Center { get; set; }

        /// <summary>
        /// Get the enclosing Rectangle that surrounds the Circle.
        /// </summary>
        /// <returns></returns>
        public abstract Rectangle GetBoundingBox();


        /// <summary>
        /// Checks if the Primitive intersects another primitive.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CheckIntersection(Collider other)
        {
            switch (other)
            {
                case RectangleCollider collider:
                    {
                        return Intersects(collider);
                    }
                case CircleCollider collider:
                    {
                        return Intersects(collider);
                    }
                case LinePieceCollider collider:
                    {
                        return Intersects(collider);
                    }
                case null:
                    {
                        return false;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Ensures that the base update won't be executed. Colliders shall never have Components
        }

        /// <summary>
        /// Gets whether or not the shape intersects a Circle.
        /// </summary>
        /// <param name="other">The Circle to check for intersection.</param>
        /// <returns>true there is any overlap between the two Circles.</returns>
        public abstract bool Intersects(CircleCollider other);

        /// <summary>
        /// Gets whether or not the shape intersects the Rectangle.
        /// </summary>
        /// <param name="other">The Rectangle to check for intersection.</param>
        /// <returns>true there is any overlap between the Circle and the Rectangle.</returns>
        public abstract bool Intersects(RectangleCollider other);

        /// <summary>
        /// Gets whether or not the shape intersects the Line
        /// </summary>
        /// <param name="other">The Line to check for intersection</param>
        /// <returns>true there is any overlap between the Circle and the Line.</returns>
        public abstract bool Intersects(LinePieceCollider other);

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this shape.
        /// </summary>
        /// <param name="loc">The coordinates to check.</param>
        /// <returns>true if the coordinates are within the circle.</returns>
        public abstract bool Contains(Vector2 loc);
    }
}
