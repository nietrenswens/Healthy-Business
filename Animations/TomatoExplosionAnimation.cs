using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Animations
{
    public class TomatoExplosionAnimation : Animation
    {
        public TomatoExplosionAnimation()
            : base(128, 128, 100, 0, 1, false)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>("entities\\enemies\\tomato\\TomatoBoom");
        }
    }
}
