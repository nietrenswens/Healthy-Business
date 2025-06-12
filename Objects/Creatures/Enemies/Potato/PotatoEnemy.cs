using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Objects.Creatures.Enemies.Potato
{
    public class PotatoEnemy : Creature
    {
        public const float SPEED = 0.8f;
        public const float AGGRO_RANGE = 7f;
        public const int DAMAGE = 40;

        public const string POTATO_NORMAL_TEXTURE_PATH = "entities\\enemies\\potato\\potato_normal";
        public const string POTATO_ATTACK_TEXTURE_PATH = "entities\\enemies\\potato\\potato_shocked";

        public PotatoEnemy(TileLocation tileLocation) : base(tileLocation.ToVector2(), 15, 15, POTATO_NORMAL_TEXTURE_PATH)
        {
            Add(new PotatoEnemyStateMachine());
            var collider = new RectangleCollider(new(tileLocation.ToPoint(), new Point(64, 64)));
            collider.CollisionGroup = CollisionGroup.None;
            Add(collider);
            IsPersistent = true;
        }
    }
}
