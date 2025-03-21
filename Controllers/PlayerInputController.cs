using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace HealthyBusiness.Controllers
{
    public class PlayerInputController : GameObject
    {
        private InputManager _inputManager => InputManager.GetInputManager();
        private float _speed;

        public PlayerInputController(float speed)
        {
            _speed = speed;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            if (Parent == null)
            {
                throw new Exception("PlayerInputController must be attached to a GameObject");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var _velocity = Vector2.Zero;

            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _velocity.X = -1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _velocity.X = 1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _velocity.Y = -1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _velocity.Y = 1;
            }

            if (_velocity != Vector2.Zero)
            {
                _velocity.Normalize();
            }
            _velocity *= _speed * gameTime.ElapsedGameTime.Milliseconds;

            Parent.LocalPosition += _velocity;
        }
    }
}
