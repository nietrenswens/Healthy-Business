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
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Levels;
using HealthyBusiness.Cameras;
using Microsoft.Xna.Framework.Input;


namespace HealthyBusiness.InGameGUIObjects
{
    public class PauseMenu : GameObject
    {
        public bool IsClosed { get; private set; } = false;

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

        private void ResumeButtonClicked(object? sender, EventArgs e)
        {
            IsClosed = true;
        }

        private void QuitButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeLevel(new MainMenu());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void Reset()
        {
            IsClosed = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
