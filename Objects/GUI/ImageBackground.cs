using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    public class ImageBackground : GameObject
    {
        private float _width;
        private float _height;
        private Texture2D texture;

        private readonly string texturePath;

        public ImageBackground(string texturePath)
        {
            _width = GameManager.GetGameManager().GraphicsDevice.Viewport.Width;
            _height = GameManager.GetGameManager().GraphicsDevice.Viewport.Height;
            this.texturePath = texturePath;
        }

        public override void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                new Rectangle(0, 0, (int)_width, (int)_height),
                Color.White
            );
        }
    }
}
