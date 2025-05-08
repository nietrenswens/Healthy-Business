using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Controllers
{
    public class IdleMovementController : MovementController
    {
        private float _pauseInterval;
        private float _pauseTimer;

        public IdleMovementController(float speed, float pauseInterval) : base(speed)
        {
            _pauseInterval = pauseInterval;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_targetLocation != null)
            {
                return;
            }

            if (_pauseTimer > 0)
            {
                _pauseTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }
            _pauseTimer = _pauseInterval + GameManager.GetGameManager().RNG.Next(0, (int)(_pauseInterval / 2));

            var currentLocation = Parent!.TileLocation;
            TileLocation? target = null;
            while (target == null)
            {
                var randomX = GameManager.GetGameManager().RNG.Next(-2, 3);
                var randomY = GameManager.GetGameManager().RNG.Next(-2, 3);
                var newLocation = new TileLocation(currentLocation.X + randomX, currentLocation.Y + randomY);
                if (TileLocation.IsTileWalkable(newLocation))
                {
                    target = newLocation;
                }
            }
            _targetLocation = target;
        }
    }
}
