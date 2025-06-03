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
    public class ExplosionAnimation : Animation
    {
        public ExplosionAnimation(string spriteSheetName)
            : base(128, 128, 100, 0, 1, false)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }
}
