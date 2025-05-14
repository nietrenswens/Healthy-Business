using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
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
            Texture = content.Load<Texture2D>("entities\\player");
            var width = (int)(Texture.Width * LocalScale);
            var height = (int)(Texture.Height * LocalScale);

            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width, height / 2)));
            collider.LocalPosition = new Vector2(0, height / 2);
            collider.CollisionGroup = CollisionGroup.Player;
            Add(collider);
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
    }
}
