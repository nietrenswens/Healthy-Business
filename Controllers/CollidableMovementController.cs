using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HealthyBusiness.Controllers
{
    public class CollidableMovementController : MovementController
    {
        private CollisionGroup _cantGoThrough;

        public CollidableMovementController(CollisionGroup cantGoThrough) : base(0.4f)
        {
            _cantGoThrough = cantGoThrough;
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            var parentCollider = Parent?.GetGameObject<Collider>();
            if (Parent == null || parentCollider == null)
                return;

            var velocity = direction * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            var destination = Parent.GetGameObject<Collider>()!.WorldPosition + velocity;
            bool collided = false;
            var tempCollider = new RectangleCollider(new Rectangle(destination.ToPoint(), parentCollider.GetBoundingBox().Size));
            var solidGameObjects = GameManager.GetGameManager().CurrentLevel.GetGameObjects(_cantGoThrough).ToList();

            foreach (var gameObject in solidGameObjects)
            {
                var collider = gameObject.GetGameObject<Collider>();
                if (collider == null)
                    continue;

                if (tempCollider.CheckIntersection(collider))
                {
                    collided = true;
                    break;
                }
            }

            if (!collided)
                Parent.WorldPosition += velocity;
        }
    }
}
