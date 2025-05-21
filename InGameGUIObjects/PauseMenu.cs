using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Levels;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace HealthyBusiness.InGameGUIObjects
{
    public class PauseMenu : GameObject
    {
        public bool IsPaused { get; private set; }

        public PauseMenu()
        {
            IsPaused = false;
        }

        public override void Load(ContentManager content)
        {
            Add(new ColoredBackground(Color.Black * 0.5f));
            Add(new Text("fonts\\pixelated_elegance\\title", "Paused", Color.White, new GUIStyling(marginTop: 200, horizontalFloat: HorizontalAlign.Center)));

            var resumeButton = new TextedButton("Resume", new GUIStyling(marginTop: 300, horizontalFloat: HorizontalAlign.Center));
            resumeButton.Clicked += ResumeButtonClicked;
            var mainMenuButton = new TextedButton("Main Menu", new GUIStyling(marginTop: 400, horizontalFloat: HorizontalAlign.Center));
            mainMenuButton.Clicked += QuitButtonClicked;

            Add(resumeButton);
            Add(mainMenuButton);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var inputManager = InputManager.GetInputManager();
            if (inputManager.IsKeyPressed(Keys.Escape))
            {
                IsPaused = !IsPaused;
            }
        }

        private void ResumeButtonClicked(object? sender, EventArgs e)
        {
            IsPaused = false;
        }

        private void QuitButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeLevel(new MainMenu());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsPaused)
                return;
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
