using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class Text : GameObject
    {
        private SpriteFont _font = null!;
        private Color _textColor;
        private GUIStyling _guiStyling;
        private Texture2D? _backgroundPixel;

        public string FontName { get; set; }
        public string TextString { get; set; }
        public Color TextColor { get; set; }
        public Color? BackgroundColor { get; set; }

        public Text(string fontPath, string text, Color? color, Color? backgroundColor = null, GUIStyling? guiStyling = null)
        {
            FontName = fontPath;
            TextString = text;

            _guiStyling = guiStyling ?? new GUIStyling();

            _textColor = color ?? Color.White;

            BackgroundColor = backgroundColor;
            TextColor = _textColor;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _font = content.Load<SpriteFont>(FontName);
            var width = _font.MeasureString(TextString).X;
            var height = _font.MeasureString(TextString).Y;
            _guiStyling = _guiStyling with { width = width, height = height };

            if (BackgroundColor != null)
            {
                _backgroundPixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
                _backgroundPixel.SetData(new[] { BackgroundColor.Value });
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (BackgroundColor != null)
            {
                DrawBackground(spriteBatch);
            }
            spriteBatch.DrawString(_font, TextString, _guiStyling.GetPosition(), _textColor);
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            var padding = 20;
            Point location = new Point((int)(_guiStyling.GetPosition().X - padding), (int)(_guiStyling.GetPosition().Y - padding));

            var textSize = _font.MeasureString(TextString);
            Point size = new Point((int)(textSize.X + padding * 2), (int)(textSize.Y + padding * 2));
            spriteBatch.Draw(_backgroundPixel, new Rectangle(location, size), BackgroundColor!.Value);
        }
    }
}
