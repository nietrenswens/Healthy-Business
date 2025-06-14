﻿using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Collision;

public class RectangleCollider : Collider, IEquatable<RectangleCollider>
{
    public Rectangle Shape;
    public override float Width { get => Shape.Width; }
    public override float Height { get => Shape.Height; }

    private Texture2D _texture;

    public RectangleCollider(Rectangle shape)
    {
        this.Shape = shape;
        _texture = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
        _texture.SetData(new[] { Color.White });
    }

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
            Shape.Location = WorldPosition.ToPoint();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Globals.DEBUG)
            spriteBatch.Draw(_texture, Shape, Color.White * 0.5f);
        base.Draw(spriteBatch);
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