using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace HealthyBusiness.Controllers
{
    public class PlayerInputController : GameObject
    {
        private InputManager _inputManager => InputManager.GetInputManager();

        public override void Load(ContentManager content)
        {
            base.Load(content);

            if (Parent is not Player)
            {
                throw new Exception("PlayerInputController must be attached to a GameObject");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Player player = (Player)Parent;
            var direction = Vector2.Zero;

            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                direction.X = -1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                direction.X = 1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                direction.Y = -1;
            }
            if (_inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                direction.Y = 1;
            }

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Player p = (Player)Parent;
            if (direction != Vector2.Zero)
            {
                p.GetGameObject<CollidableMovementController>().Move(direction, gameTime);
            }
        }
    }
}