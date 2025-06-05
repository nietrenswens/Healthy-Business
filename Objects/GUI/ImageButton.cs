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
        private SpriteFont _font = null!;

        public string ?Text { get; private set; }

        public ImageButton(GUIStyling? guiStyling = null) : base("gui\\manual", guiStyling)
        {

        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _font = content.Load<SpriteFont>("fonts\\pixelated_elegance\\large");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var collider = GetGameObject<Collider>()!;
            var position = collider.GetBoundingBox().Location.ToVector2();
        }
    }
}
