using HealthyBusiness.Cameras;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace HealthyBusiness.Scenes
{
    class GameOverScene : Scene
    {
        private SoundEffectInstance? _gameOverMusic;
        private TextedButton? _playAgainButton;
        private TextedButton? _quitButton;

        private const string TITLE_FONT = "fonts\\pixelated_elegance\\title";
        private const string LARGE_FONT = "fonts\\pixelated_elegance\\large";

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            AddGameObject(new ImageBackground("backgrounds\\gameover"));

            var gameOverMusic = content.Load<SoundEffect>("audio\\gameOver");
            _gameOverMusic = gameOverMusic.CreateInstance();
            _gameOverMusic.IsLooped = true;
            _gameOverMusic.Volume = 0.5f;
            _gameOverMusic.Play();

            var gameData = GameManager.GetGameManager().GameData;

            AddGameObject(new Text(TITLE_FONT, "Game Over", Color.Firebrick, guiStyling: new GUIStyling(marginTop: 100, horizontalFloat: HorizontalAlign.Center)));

            AddGameObject(new Text(LARGE_FONT, $"You survived the disaster for {gameData.ShiftCount} days!", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Top,
                marginTop = 200f
            }));

            _playAgainButton = new TextedButton("Play Again", new GUIStyling(marginTop: 500, horizontalFloat: HorizontalAlign.Center));
            _playAgainButton.Clicked += PlayAgainButtonClicked;
            _quitButton = new TextedButton("Quit", new GUIStyling(marginTop: 600, horizontalFloat: HorizontalAlign.Center));
            _quitButton.Clicked += QuitButtonClicked;

            AddGameObject(_playAgainButton);
            AddGameObject(_quitButton);
        }

        private void PlayAgainButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().RestartGame();
        }

        private void QuitButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().Exit();
        }

        public override void Unload()
        {
            _gameOverMusic?.Stop();
            _gameOverMusic?.Dispose();
            base.Unload();
        }
    }
}
