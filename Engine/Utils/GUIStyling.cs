using HealthyBusiness.Engine.GUI;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Engine.Utils
{
    public record GUIStyling(float width = 0, float height = 0, float marginLeft = 0, float marginRight = 0, float marginTop = 0, float marginBottom = 0, HorizontalAlign horizontalFloat = HorizontalAlign.Left, VerticalAlign verticalFloat = VerticalAlign.Top)
    {
        public Vector2 GetPosition()
        {
            Vector2 basePos = new Vector2(0, 0);
            switch (horizontalFloat)
            {
                case HorizontalAlign.Left:
                    basePos.X = 0;
                    break;
                case HorizontalAlign.Center:
                    basePos.X = (Globals.SCREENWIDTH - width) / 2;
                    break;
                case HorizontalAlign.Right:
                    basePos.X = Globals.SCREENWIDTH - width;
                    break;
            }

            switch (verticalFloat)
            {
                case VerticalAlign.Top:
                    basePos.Y = 0;
                    break;
                case VerticalAlign.Center:
                    basePos.Y = (Globals.SCREENHEIGHT - height) / 2;
                    break;
                case VerticalAlign.Bottom:
                    basePos.Y = Globals.SCREENHEIGHT - height;
                    break;
            }

            return new Vector2(basePos.X + marginLeft - marginRight, basePos.Y + marginTop - marginBottom);
        }
    }
}
