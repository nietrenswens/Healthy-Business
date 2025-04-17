using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class TextedButton : Button
    {
        public string Text { get; private set; }

        private SpriteFont _font = null!;

        public TextedButton(string text, GUIStyling? guiStyling = null) : base("gui\\base_button", guiStyling)
        {
            Text = text;
        }

        public override void Load(ContentManager content)
        {

            _font = content.Load<SpriteFont>("fonts\\pixelated_elegance\\large");
            base.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var textSize = _font.MeasureString(Text);
            var position = Collider!.GetBoundingBox().Location.ToVector2();
            var textPosition = new Vector2(
                position.X + (Width - textSize.X) / 2,
                position.Y + (Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(_font, Text, textPosition, Color.White);
        }
    }
}
