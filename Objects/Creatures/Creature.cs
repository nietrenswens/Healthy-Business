using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Objects.Creatures
{
    public class Creature : GameObject
    {
        private string? _textureName;

        public Texture2D Texture = null!;
        public Animation? Animation { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public float Width => GetGameObject<Collider>()!.Width;
        public float Height => GetGameObject<Collider>()!.Height;

        public Creature(Vector2 spawnPosition, int health, int maxHealth, string? textureName = null)
        {
            WorldPosition = spawnPosition;
            _textureName = textureName;
            Health = health;
            MaxHealth = maxHealth;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (_textureName != null && Animation == null)
            {
                Texture = content.Load<Texture2D>(_textureName);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Animation?.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Animation != null)
            {
                Animation.Draw(spriteBatch, WorldPosition, LocalScale / 2f);
            }
            else
            {
                var width = (int)(Texture.Width * LocalScale);
                var height = (int)(Texture.Height * LocalScale);
                spriteBatch.Draw(Texture, new Rectangle(WorldPosition.ToPoint(), new Point(width, height)), Color.White);
            }

            base.Draw(spriteBatch);
        }

        public virtual void OnDirectionChanged(Vector2 direction)
        {
            if (direction == Vector2.Zero)
            {
                Animation.Pause();
                return;
            }
            Animation.Resume();

            int newRow;

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                newRow = direction.X > 0 ? 1 : 2;
            }
            else
            {
                newRow = direction.Y > 0 ? 0 : 3;
            }

            if (Animation.CurrentRow != newRow)
            {
                Animation.SetRow(newRow);
            }
        }

    }
}
