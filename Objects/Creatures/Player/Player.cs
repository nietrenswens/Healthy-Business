using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.Player.Modules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.Creatures.Player
{
    public class Player : Creature
    {
        public Player(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            Add(new CollidableMovementController(CollisionGroup.Solid));
            Add(new PlayerInputController());
            Add(new ItemPickupModule());
        }

        public Player(TileLocation tileLocation) : this(tileLocation.ToVector2())
        { }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("entities\\player");
            var width = (int)(_texture.Width * LocalScale);
            var height = (int)(_texture.Height * LocalScale);

            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width, height)));
            collider.CollisionGroup = CollisionGroup.Player;
            Add(collider);
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var collider = GetGameObject<Collider>();
            if (collider is RectangleCollider rectangleCollider)
            {
                rectangleCollider.Shape.Location = WorldPosition.ToPoint();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var width = (int)(_texture.Width * LocalScale);
            var height = (int)(_texture.Height * LocalScale);
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(width, height)), Color.White);
            base.Draw(spriteBatch);
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
    }
}
