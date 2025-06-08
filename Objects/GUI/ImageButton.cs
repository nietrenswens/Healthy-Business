using HealthyBusiness.Collision;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class ImageButton : Button
    {
        public ImageButton(GUIStyling? guiStyling = null) : base("gui\\manual", guiStyling)
        {

        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var collider = GetGameObject<Collider>()!;
            var position = collider.GetBoundingBox().Location.ToVector2();
        }
    }
}
