using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Animations
{
    public class TomatoAnimation : Animation
    {
        public TomatoAnimation(string spriteSheetName)
            : base(128, 128, 100, 0, 1, true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }
}
