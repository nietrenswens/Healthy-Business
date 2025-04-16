using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
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

        private float _marginLeft;
        private float _marginRight;
        private float _marginTop;
        private float _marginBottom;

        private HorizontalFloat _horizontalFloat;
        private VerticalFloat _verticalFloat;

        private Color _textColor;

        public Text(string fontPath, string text, Color? color, float marginLeft = 0, float marginRight = 0, float marginTop = 0, float marginBottom = 0, HorizontalFloat horizontalFloat = HorizontalFloat.Left, VerticalFloat verticalFloat = VerticalFloat.Top)
        {
            FontName = fontPath;
            TextString = text;

            _marginLeft = marginLeft;
            _marginRight = marginRight;
            _marginTop = marginTop;
            _marginBottom = marginBottom;

            _horizontalFloat = horizontalFloat;
            _verticalFloat = verticalFloat;

            _textColor = color ?? Color.White;
        }

        public Text(string fontName, string text, float marginLeft = 0, float marginRight = 0, float marginTop = 0, float marginBottom = 0, HorizontalFloat horizontalFloat = HorizontalFloat.Left, VerticalFloat verticalFloat = VerticalFloat.Top)
            : this(fontName, text, Color.White, marginLeft, marginRight, marginTop, marginBottom, horizontalFloat, verticalFloat)
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _font = content.Load<SpriteFont>(FontName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var textSize = _font.MeasureString(TextString);
            Vector2 basePos = new Vector2(0, 0);
            switch (_horizontalFloat)
            {
                case HorizontalFloat.Left:
                    basePos.X = 0;
                    break;
                case HorizontalFloat.Center:
                    basePos.X = (Globals.SCREENWIDTH - textSize.X) / 2;
                    break;
                case HorizontalFloat.Right:
                    basePos.X = Globals.SCREENWIDTH - textSize.X;
                    break;
            }

            switch (_verticalFloat)
            {
                case VerticalFloat.Top:
                    basePos.Y = 0;
                    break;
                case VerticalFloat.Center:
                    basePos.Y = (Globals.SCREENHEIGHT - textSize.Y) / 2;
                    break;
                case VerticalFloat.Bottom:
                    basePos.Y = Globals.SCREENHEIGHT - textSize.Y;
                    break;
            }

            Vector2 pos = new Vector2(basePos.X + _marginLeft - _marginRight, basePos.Y + _marginTop - _marginBottom);

            spriteBatch.DrawString(_font, TextString, pos, _textColor);
        }
    }
}
