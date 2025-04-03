using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Collision;

public class RectangleCollider : Collider, IEquatable<RectangleCollider>
{
    public Rectangle Shape;

    public RectangleCollider(Rectangle shape)
    {
        this.Shape = shape;
    }

    public override bool Contains(Vector2 loc)
    {
        return Shape.Contains(loc);
    }

    public bool Equals(RectangleCollider other)
    {
        return Shape == other.Shape;
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