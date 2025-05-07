using HealthyBusiness.Collision;
using HealthyBusiness.Controllers.PathFinding;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HealthyBusiness.Controllers.StateMachines
{
    public class TomatoStateMachine : GameObject
    {
        private bool pathfinding = false;
        private float _counter = 5f;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _counter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_counter <= 0 && !pathfinding)
            {
                pathfinding = true;
                Parent!.GetGameObject<PathfindingMovementController>()!.Target = GameManager.GetGameManager().
                    CurrentLevel.GameObjects.OfType<Player>()
                    .First()
                    .GetGameObject<Collider>();
            }
        }
    }
}
