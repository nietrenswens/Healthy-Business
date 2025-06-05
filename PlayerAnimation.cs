using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness
{
    public class PlayerAnimation : Animation
    {
        public PlayerAnimation(string spriteSheetName)
            : base(32, 64, 100, 0, 4, true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }

}
