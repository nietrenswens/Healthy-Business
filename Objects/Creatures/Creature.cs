using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Objects.Creatures
{
    public class Creature : GameObject
    {
        private string? _textureName;
        private Texture2D _grayPixel = null!;
        private Texture2D _greenPixel = null!;

        private const int HealthBarHeight = 10;
        private const int HealthBarWidth = 50;

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

            _grayPixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _grayPixel.SetData(new[] { Color.DarkGray });
            _greenPixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _greenPixel.SetData(new[] { Color.Green });

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

            DrawHealthBar(spriteBatch);

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

        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            if (Health >= MaxHealth)
                return;
            var center = GetGameObject<Collider>()!.Center;
            var x = (int)(center.X) - (HealthBarWidth / 2);
            var y = (int)(WorldPosition.Y - 20);
            spriteBatch.Draw(_grayPixel, new Rectangle(new Point(x, y), new Point(HealthBarWidth, HealthBarHeight)), Color.White);
            var healthPercentage = (float)Health / MaxHealth;
            spriteBatch.Draw(_greenPixel, new Rectangle(new Point(x, y), new Point((int)(HealthBarWidth * healthPercentage), HealthBarHeight)), Color.White);
        }

    }
}
