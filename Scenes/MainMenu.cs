using HealthyBusiness.Cameras;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace HealthyBusiness.Scenes
{
    public class MainMenu : Scene
    {
        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            //AddGameObject(new ColoredBackground(Color.LightCoral));
            AddGameObject(new ImageBackground("backgrounds\\titlescreen"));
            AddGameObject(new Text("fonts\\pixelated_elegance\\title", "Healthy Business", Color.White, new GUIStyling(marginTop: 200, horizontalFloat: HorizontalAlign.Center)));

            var playButton = new TextedButton("Play", new GUIStyling(marginTop: 300, horizontalFloat: HorizontalAlign.Center));
            playButton.Clicked += PlayButtonClicked;
            var quitButton = new TextedButton("Quit", new GUIStyling(marginTop: 400, horizontalFloat: HorizontalAlign.Center));
            quitButton.Clicked += QuitButtonClicked;

            AddGameObject(playButton);
            AddGameObject(quitButton);
        }

        private void PlayButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeScene(new GameScene());

        }

        private void QuitButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().Exit();

        }
    }
}
