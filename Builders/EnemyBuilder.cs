using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Builders
{
    public static class EnemyBuilder
    {
        public static Creature CreateTomatoEnemy(TileLocation tileLocation)
        {
            var enemy = new Creature(tileLocation.ToVector2(), 10, 10, "entities\\enemies\\tomato\\tomato");
            var collider = new RectangleCollider(new(tileLocation.ToPoint(), new Point(64, 64)));
            collider.CollisionGroup = CollisionGroup.Solid;
            enemy.Add(collider);
            return enemy;
        }
    }
}
