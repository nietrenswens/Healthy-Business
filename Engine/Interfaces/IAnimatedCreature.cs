using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace HealthyBusiness.Engine.Interfaces
{
    public interface IAnimatedCreature
    {
        void OnDirectionChanged(Vector2 direction);
    }
}
