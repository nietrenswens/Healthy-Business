using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Interfaces;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using HealthyBusiness.Animations;


namespace HealthyBusiness.Objects.Creatures.Enemies.Tomato
{
    public class TomatoEnemy : Creature, IAnimatedCreature
    {

        public static float AggroRange = 10f;
        public static float ExplosionRange = 2f;

        public const float SPEED = 2f;

        public TomatoEnemy(TileLocation tileLocation) : base(tileLocation.ToVector2(), 10, 10)
        {
            Animation = new TomatoAnimation("entities\\enemies\\tomato\\tomato");
            var collider = new RectangleCollider(new(tileLocation.ToPoint(), new Point(64, 64)));
            collider.CollisionGroup = CollisionGroup.None;
            Add(collider);
            var stateMachine = new TomatoStateMachine();
            Add(stateMachine);
        }
    }
}
