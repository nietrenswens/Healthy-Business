using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Levels;
using HealthyBusiness.Objects.Creatures.Player;
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

            GameLevel currentGameLevel = (GameLevel)GameManager.GetGameManager().CurrentLevel;
            //Hotbar hotbar = currentGameLevel.GUIObjects.Attributes.OfType<Hotbar>()
            //    .First();

            // TODO: when the game is initlized, the hotbar is not yet created. We need to fix this but for now we skip it when it is null
            Hotbar hotbar = currentGameLevel.GUIObjects.Attributes.Where(x => x is Hotbar).FirstOrDefault() as Hotbar;

            //System.Diagnostics.Debug.WriteLine(currentGameLevel.GUIObjects.Attributes.ToString);



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
            if(_inputManager.IsMouseScrollingUp()) {
                if(hotbar != null) hotbar.SelectNextSlot(moveToNextSlot: false); // more readable than putting "false"

            }
            if (_inputManager.IsMouseScrollingDown())
            {
                if(hotbar != null) hotbar.SelectNextSlot(moveToNextSlot: true); // same goes for here. More readable than just "true"
            }

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            ;
            if (direction != Vector2.Zero)
            {
                player.GetGameObject<CollidableMovementController>()!.Move(direction, gameTime);
            }
        }

        private Player GetPlayer()
        {
            if (Parent == null && Parent is not Player)
                throw new Exception("Parent of PlayerInputController must be Player");
            return (Player)Parent;
        }
    }
}