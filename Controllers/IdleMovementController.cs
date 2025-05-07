using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Levels;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Controllers
{
    public class IdleMovementController : GameObject
    {
        private float _speed;
        private TileLocation? _targetLocation;
        private float _pauseInterval;
        private float _pauseTimer;

        public IdleMovementController(float speed, float pauseInterval)
        {
            _speed = speed;
            _pauseInterval = pauseInterval;
        }

        public void Move()
        {
            var targetPosition = _targetLocation!.ToVector2();
            Vector2 direction = targetPosition - Parent!.WorldPosition;
            if (direction.Length() < _speed)
            {
                Parent.WorldPosition = targetPosition;
                _targetLocation = null;
            }
            else
            {
                direction.Normalize();
                Parent.WorldPosition += direction * _speed;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_targetLocation != null)
            {
                Move();
                return;
            }

            if (_pauseTimer > 0)
            {
                _pauseTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }
            _pauseTimer = _pauseInterval;

            var currentLocation = Parent!.TileLocation;
            TileLocation? target = null;
            while (target == null)
            {
                var randomX = GameManager.GetGameManager().RNG.Next(-2, 3);
                var randomY = GameManager.GetGameManager().RNG.Next(-2, 3);
                var newLocation = new TileLocation(currentLocation.X + randomX, currentLocation.Y + randomY);
                if (((GameLevel)GameManager.GetGameManager().CurrentLevel).IsTileWalkable(newLocation))
                {
                    target = newLocation;
                }
            }
            _targetLocation = target;
        }
    }
}
