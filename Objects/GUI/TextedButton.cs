using HealthyBusiness.Collision;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class TextedButton : Button
    {
        // Padding for the text
        private const float paddingTop = 5f;
        private SpriteFont _font = null!;

        public string Text { get; private set; }

        public TextedButton(string text, GUIStyling? guiStyling = null) : base("gui\\base_button", guiStyling)
        {
            Text = text;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _font = content.Load<SpriteFont>("fonts\\pixelated_elegance\\large");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var textSize = _font.MeasureString(Text);
            var collider = GetGameObject<Collider>()!;
            var position = collider.GetBoundingBox().Location.ToVector2();
            var textPosition = new Vector2(
                position.X + (collider.Width - textSize.X) / 2,
                position.Y + (collider.Height - textSize.Y) / 2 + paddingTop
            );
            spriteBatch.DrawString(_font, Text, textPosition, Color.White);
        }
    }
}
