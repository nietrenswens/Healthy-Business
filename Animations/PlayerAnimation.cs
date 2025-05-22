using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Animations
{
    public class PlayerAnimation : Animation
    {
        public PlayerAnimation(string spriteSheetName)
            : base(32, 64, 200, 0, 4, true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }

}
