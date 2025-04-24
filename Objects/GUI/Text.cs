using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class Text : GameObject
    {
        public string FontName { get; set; }
        public string TextString { get; set; }

        private SpriteFont _font = null!;

        private Color _textColor;
        private GUIStyling _guiStyling;

        public Text(string fontPath, string text, Color? color, GUIStyling? guiStyling = null)
        {
            FontName = fontPath;
            TextString = text;

            _guiStyling = guiStyling ?? new GUIStyling();

            _textColor = color ?? Color.White;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _font = content.Load<SpriteFont>(FontName);
            var width = _font.MeasureString(TextString).X;
            var height = _font.MeasureString(TextString).Y;
            _guiStyling = _guiStyling with { width = width, height = height };

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, TextString, _guiStyling.GetPosition(), _textColor);
        }
    }
}
