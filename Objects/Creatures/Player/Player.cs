using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
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
            LocalScale = 4;
            Add(new CollidableMovementController(CollisionGroup.Solid));
            Add(new PlayerInputController());
        }

        public Player(TileLocation tileLocation) : this(tileLocation.ToVector2())
        { }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("entities\\player");
            var width = (int)(_texture.Width * LocalScale);
            var height = (int)(_texture.Height * LocalScale);
            SetCollider(new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width, height))), CollisionGroup.Player);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Collider is RectangleCollider rectangleCollider)
            {
                rectangleCollider.Shape.Location = WorldPosition.ToPoint();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var width = (int)(_texture.Width * LocalScale);
            var height = (int)(_texture.Height * LocalScale);
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(width, height)), Color.White);
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
    }
}
