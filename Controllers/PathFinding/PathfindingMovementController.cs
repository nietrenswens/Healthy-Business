using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Controllers.PathFinding
{
    public class PathfindingMovementController : GameObject
    {
        private TileLocation? _lastTargetTileLocation;
        private TileLocation? _currentStep;

        public Stack<TileLocation> CurrentPath { get; private set; }
        public float Speed { get; private set; } = 1f;

        public GameObject? Target { get; set; }


        public PathfindingMovementController(float speed)
        {
            Speed = speed;
            CurrentPath = new Stack<TileLocation>();
        }

        private void SetNextStep()
        {
            if (_currentStep != null || CurrentPath == null || CurrentPath.Count == 0)
            {
                return;
            }
            TileLocation nextTile = CurrentPath.Pop();
            _currentStep = nextTile;

        }

        private void Move()
        {
            if (_currentStep == null)
            {
                return;
            }
            Vector2 targetPosition = _currentStep.ToVector2();
            Vector2 direction = targetPosition - Parent!.WorldPosition;
            if (direction.Length() < Speed)
            {
                Parent!.WorldPosition = targetPosition;
                _currentStep = null;
            } else
            {
                direction.Normalize();
                Parent!.WorldPosition += direction * Speed;
            }
        }

        private void CalculatePath()
        {
            if (Target == null)
                return;


            if (_lastTargetTileLocation != Target.TileLocation || CurrentPath.Count == 0)
            {
                _lastTargetTileLocation = Target.TileLocation;
                var path = Pathfinding.PathFinding(Parent!.TileLocation, Target.TileLocation).Skip(1).Take(3).ToList();
                path.Reverse();
                CurrentPath = new Stack<TileLocation>(path);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CalculatePath();
            SetNextStep();
            Move();
        }
    }
}
