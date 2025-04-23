using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace HealthyBusiness.InGameGUIObjects
{
    public class PauseMenu : GameObject
    {
        private Texture2D? _backgroundTexture;
        private SpriteFont? _font;

        public override void Load(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>("objects\\wall");
            _font = content.Load<SpriteFont>("fonts\\pixelated_elegance\\title");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 1920, 1080), Color.Gray * 0.5f);
            spriteBatch.DrawString(_font, "Paused", new Vector2(960 - _font.Texture.Width / 6, 300), Color.White);
            spriteBatch.DrawString(_font, "Press 'Esc' to Resume", new Vector2(960 - _font.Texture.Width/2, 500), Color.White);

            spriteBatch.End();
        }
    }
}
