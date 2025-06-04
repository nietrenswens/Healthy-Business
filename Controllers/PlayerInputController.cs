using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;

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
            Player player = GetPlayer();
            var direction = Vector2.Zero;

            GameScene currentGameLevel = (GameScene)GameManager.GetGameManager().CurrentScene;

            Hotbar? hotbar = currentGameLevel.GUIObjects.Attributes.Where(x => x is Hotbar).FirstOrDefault() as Hotbar;

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
            if (_inputManager.IsMouseScrollingUp())
            {
                if (hotbar != null) hotbar.SelectNextSlot(ScrollDirection.Up);

            }
            if (_inputManager.IsMouseScrollingDown())
            {
                if (hotbar != null) hotbar.SelectNextSlot(ScrollDirection.Down);
            }

            if (direction != Vector2.Zero)
            {
                player.GetGameObject<CollidableMovementController>()!.Move(direction, gameTime);
            }

            (Parent as Creature)?.OnDirectionChanged(direction);
        }

        private Player GetPlayer()
        {
            if (Parent == null && Parent is not Player)
                throw new Exception("Parent of PlayerInputController must be Player");
            return (Player)Parent;
        }
    }
}