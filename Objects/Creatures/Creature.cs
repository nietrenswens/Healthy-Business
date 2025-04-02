using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Objects.Creatures
{
    public class Creature : GameObject
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public Creature(Vector2 spawnPosition)
        {
            WorldPosition = spawnPosition;
        }
    }
}
