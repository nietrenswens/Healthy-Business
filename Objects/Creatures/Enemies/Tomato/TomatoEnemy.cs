using HealthyBusiness.Animations;
using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;


namespace HealthyBusiness.Objects.Creatures.Enemies.Tomato
{
    public class TomatoEnemy : Creature
    {
        public static int Damage = 15;
        public static float AggroRange = 5f;
        public static float ExplosionRange = 1.5f;

        public const float SPEED = 2f;

        public TomatoEnemy(TileLocation tileLocation) : base(tileLocation.ToVector2(), 10, 10)
        {
            Animation = new TomatoAnimation("entities\\enemies\\tomato\\tomato3");
            var collider = new RectangleCollider(new(tileLocation.ToPoint(), new Point(64, 64)));
            collider.CollisionGroup = CollisionGroup.None;
            Add(collider);
            var stateMachine = new TomatoStateMachine();
            Add(stateMachine);
            IsPersistent = true;
        }
    }
}
