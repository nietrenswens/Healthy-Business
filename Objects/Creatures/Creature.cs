using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.Creatures
{
    public class Creature : GameObject
    {
        private string? _textureName;

        public Texture2D Texture = null!;
        public float Width => GetGameObject<Collider>()!.Width;
        public float Height => GetGameObject<Collider>()!.Height;
        public int Health { get; set; }
        public int MaxHealth { get; set; }

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
            if (_textureName != null)
            {
                Texture = content.Load<Texture2D>(_textureName);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var width = (int)(Texture.Width * LocalScale);
            var height = (int)(Texture.Height * LocalScale);
            spriteBatch.Draw(Texture, new Rectangle(WorldPosition.ToPoint(), new Point(width, height)), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
