using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Engine.GUI
{
    public abstract class Button : GameObject
    {
        private string _texturePath;
        private GUIStyling _guiStyling;
        private Texture2D _texture = null!;

        public event EventHandler? Clicked;

        public Button(string texturePath, GUIStyling? guiStyling = null)
        {
            _texturePath = texturePath;
            _guiStyling = guiStyling ?? new GUIStyling();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>(_texturePath);
            _guiStyling = _guiStyling with { width = _texture.Width, height = _texture.Height };
            Add(new RectangleCollider(new Rectangle(_guiStyling.GetPosition().ToPoint(), new Point(_texture.Width, _texture.Height))));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            WorldPosition = _guiStyling.GetPosition();
            var inputManager = InputManager.GetInputManager();
            if (inputManager.LeftMousePressed() && GetGameObject<Collider>()!.Contains(inputManager.GetMousePosition()))
            {
                Clicked?.Invoke(this, new EventArgs());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, _guiStyling.GetPosition(), Color.White);
        }
    }
}
