using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Controllers
{
    public class CollidableMovementController : GameObject
    {
        private float _speed = 0.4f; // Adjusted speed for smoother movement
        private CollisionGroup _cantGoThrough;

        public CollidableMovementController(CollisionGroup cantGoThrough)
        {
            _cantGoThrough = cantGoThrough;
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            if (Parent == null || Parent.Collider == null)
                return;



            var velocity = direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var rectCollider = (RectangleCollider)Parent.Collider;

            var destination = Parent.WorldPosition + velocity;
            bool collided = false;
            var tempCollider = new RectangleCollider(new Rectangle(destination.ToPoint(), rectCollider.Shape.Size));

            foreach (var gameObject in GameManager.GetGameManager().CurrentLevel.GetGameObjects(_cantGoThrough))
            {
                if (gameObject.Collider == null)
                    continue;

                if (tempCollider.CheckIntersection(gameObject.Collider))
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
