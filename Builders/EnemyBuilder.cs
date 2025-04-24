using HealthyBusiness.Collision;
using HealthyBusiness.Controllers.PathFinding;
using HealthyBusiness.Controllers.StateMachines;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Builders
{
    public static class EnemyBuilder
    {
        public static Creature CreateTomatoEnemy(TileLocation tileLocation, Player player)
        {
            var enemy = new Creature(tileLocation.ToVector2(), 10, 10, "entities\\enemies\\tomato\\tomato");
            var collider = new RectangleCollider(new(tileLocation.ToPoint(), new Point(64, 64)));
            collider.CollisionGroup = CollisionGroup.None;
            var pathFinding = new PathfindingMovementController(2f);
            enemy.Add(pathFinding);
            enemy.Add(collider);
            enemy.Add(new TomatoStateMachine());
            return enemy;
        }
    }
}
