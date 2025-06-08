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
        private Text? titleText;
        private TextedButton? playButton;
        private TextedButton? quitButton;


        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());

            AddGameObject(new ImageBackground("backgrounds\\titlescreen"));
            titleText = new Text("fonts\\pixelated_elegance\\title", "Healthy Business", Color.White, new GUIStyling(marginTop: 200, horizontalFloat: HorizontalAlign.Center));
            AddGameObject(titleText);

            playButton = new TextedButton("Play", new GUIStyling(marginTop: 300, horizontalFloat: HorizontalAlign.Center));
            playButton.Clicked += PlayButtonClicked;
            quitButton = new TextedButton("Quit", new GUIStyling(marginTop: 400, horizontalFloat: HorizontalAlign.Center));
            quitButton.Clicked += QuitButtonClicked;
            var manualButton = new ImageButton(new GUIStyling(marginLeft: -10, marginTop: -10, horizontalFloat: HorizontalAlign.Right, verticalFloat: VerticalAlign.Bottom));
            manualButton.Clicked += ManualButtonClicked;

            AddGameObject(playButton);
            AddGameObject(quitButton);
            AddGameObject(manualButton);
        }

        private void PlayButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeScene(new GameScene(GameSceneType.PlayableLevel));
        }

        private void QuitButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().Exit();

        }

        private void ManualButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeScene(new ManualScene());
        }
    }
}
