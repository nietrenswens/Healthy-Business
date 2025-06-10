using HealthyBusiness.Cameras;
using HealthyBusiness.Data;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.Scenes
{
    class GameOverScene : Scene
    {
        private TextedButton? _playAgainButton;
        private TextedButton? _quitButton;

        private const string TITLE_FONT = "fonts\\pixelated_elegance\\title";
        private const string LARGE_FONT = "fonts\\pixelated_elegance\\large";

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            AddGameObject(new ImageBackground("backgrounds\\gameover"));

            var gameData = GameManager.GetGameManager().GameData;

            AddGameObject(new Text(TITLE_FONT, "Stats", Color.White, new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Top,
            }));

            AddGameObject(new Text(LARGE_FONT, $"You survived the disaster for {gameData.ShiftCount} days!", Color.White, new()
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
    }
}
