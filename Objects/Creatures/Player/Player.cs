
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.Creatures.Player
{
    public class Player : Creature
    {
        private Texture2D _texture;
        public Player(Vector2 spawnPosition) : base(spawnPosition)
        {
            Health = 100;
            MaxHealth = 100;

            Add(new PlayerInputController(0.5f));
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(100, 100)), Color.White);
        }
    }
}
