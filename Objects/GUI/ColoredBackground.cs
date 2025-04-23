using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class ColoredBackground : GameObject
    {
        private float _width;
        private float _height;
        private Texture2D _pixel;

        public override float Width => _width;
        public override float Height => _height;

        public ColoredBackground(Color color)
        {
            _width = GameManager.GetGameManager().GraphicsDevice.Viewport.Width;
            _height = GameManager.GetGameManager().GraphicsDevice.Viewport.Height;
            _pixel = new Texture2D(
                GameManager.GetGameManager().GraphicsDevice,
                1,
                1
            );
            _pixel.SetData(new[] { color });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_pixel,
                new Rectangle(0, 0, (int)_width, (int)_height),
                Color.White
            );
        }
    }
}
