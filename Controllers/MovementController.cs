using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using HealthyBusiness.Engine.Interfaces;
using HealthyBusiness.Objects.Creatures;

namespace HealthyBusiness.Controllers
{
    public abstract class MovementController : GameObject
    {
        public float Speed { get; set; }

        protected TileLocation? _targetLocation;
        
        private Vector2 _lastMoveDirection = Vector2.Zero;


        private Vector2 _lastMoveDirection = Vector2.Zero;

        public MovementController(float speed)
        {
            Speed = speed;
        }

        public void Move(Vector2 direction)
        {
            if (direction.Length() > 0)
            {
                direction.Normalize();
                Parent!.WorldPosition += direction * Speed;
            }

            if (direction != _lastMoveDirection)
            {
                _lastMoveDirection = direction;
                (Parent as Creature)?.OnDirectionChanged(direction);
            }
        }

        public void Move(TileLocation location)
        {
            _targetLocation = location;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_targetLocation == null)
                return;
            var targetPosition = _targetLocation.ToVector2();
            Vector2 direction = targetPosition - Parent!.WorldPosition;
            if (direction.Length() < Speed)
            {
                Parent.WorldPosition = targetPosition;
                _targetLocation = null;
            }
            else
            {
                direction.Normalize();
                Parent.WorldPosition += direction * Speed;

                if (direction != _lastMoveDirection)
                {
                    _lastMoveDirection = direction;
                    (Parent as Creature)?.OnDirectionChanged(direction);
                }
            }
        }
    }
}
