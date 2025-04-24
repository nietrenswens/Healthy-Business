using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures;

namespace HealthyBusiness.Builders
{
    public static class EnemyBuilder
    {
        public static Creature CreateTomatoEnemy(TileLocation tileLocation)
        {
            var enemy = new Creature(tileLocation.ToVector2(), 10, 10, "entities\\tomato");
            return enemy;
        }
    }
}
