using HealthyBusiness.Cameras;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Audio;

namespace HealthyBusiness.Scenes
{
    public class MainMenu : Scene
    {
        private SoundEffectInstance? _menuMusic;
        private SoundEffectInstance? _startSound;
        private Text? _titleText;
        private TextedButton? _playButton;
        private TextedButton? _quitButton;

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());

            var menuMusic = content.Load<SoundEffect>("audio\\mainMenu");
            _menuMusic = menuMusic.CreateInstance();
            _menuMusic.IsLooped = true;
            GameManager.GetGameManager().PlayLoopingMusic(menuMusic, 0.5f, "main_menu_music");

            var startSound = content.Load<SoundEffect>("audio\\startLevel");
            _startSound = startSound.CreateInstance();
            _startSound.IsLooped = false;
            _startSound.Volume = 0.5f;
            _startSound.Pitch = -0.5f;

            AddGameObject(new ImageBackground("backgrounds\\titlescreen"));
            _titleText = new Text("fonts\\pixelated_elegance\\title", "Healthy Business", Color.White, guiStyling: new GUIStyling(marginTop: 200, horizontalFloat: HorizontalAlign.Center));
            AddGameObject(_titleText);

            _playButton = new TextedButton("Play", new GUIStyling(marginTop: 300, horizontalFloat: HorizontalAlign.Center));
            _playButton.Clicked += PlayButtonClicked;
            _quitButton = new TextedButton("Quit", new GUIStyling(marginTop: 400, horizontalFloat: HorizontalAlign.Center));
            _quitButton.Clicked += QuitButtonClicked;
            var manualButton = new ImageButton(new GUIStyling(marginLeft: -10, marginTop: -10, horizontalFloat: HorizontalAlign.Right, verticalFloat: VerticalAlign.Bottom));
            manualButton.Clicked += ManualButtonClicked;

            AddGameObject(_playButton);
            AddGameObject(_quitButton);
            AddGameObject(manualButton);
        }

        private void PlayButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeScene(new LoadingScene());
            _startSound?.Play();
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
