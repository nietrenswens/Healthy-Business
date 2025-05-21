using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HealthyBusiness
{
    public class PlayerAnimation : Animation
    {
        public PlayerAnimation(string spriteSheetName, int rowPlaying)
            : base(32, 64, 100, rowPlaying, true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }

}
