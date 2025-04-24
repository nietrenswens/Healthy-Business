using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Collision;

public class RectangleCollider : Collider, IEquatable<RectangleCollider>
{
    public Rectangle Shape;

    public override float Width { get => Shape.Width; }
    public override float Height { get => Shape.Height; }

    public override Vector2 Center
    {
        get => new Vector2(Shape.X + Shape.Width / 2, Shape.Y + Shape.Height / 2);
        set
        {
            Shape.X = (int)(value.X - Shape.Width / 2);
            Shape.Y = (int)(value.Y - Shape.Height / 2);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Parent != null)
        {
            Shape.Location = Parent.WorldPosition.ToPoint();
        }
    }

    public RectangleCollider(Rectangle shape)
    {
        this.Shape = shape;
    }

    public override bool Contains(Vector2 loc)
    {
        return Shape.Contains(loc);
    }

    public bool Equals(RectangleCollider? other)
    {
        return other != null && Shape == other.Shape;
    }

    public override Rectangle GetBoundingBox()
    {
        return Shape;
    }

    public override bool Intersects(CircleCollider other)
    {
        return other.Intersects(this);
    }

    public override bool Intersects(RectangleCollider other)
    {
        return Shape.Intersects(other.Shape);
    }

    public override bool Intersects(LinePieceCollider other)
    {
        return other.Intersects(this);
    }
}