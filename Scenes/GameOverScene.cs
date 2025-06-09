using HealthyBusiness.Cameras;
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
        private TextedButton? playAgainButton;
        private TextedButton? quitButton;
        private const string TITLE_FONT = "fonts\\pixelated_elegance\\title";

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            AddGameObject(new ColoredBackground(Color.Black));

            AddGameObject(new Text(TITLE_FONT, "Stats", Color.White, new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Top,
            }));

            playAgainButton = new TextedButton("Play Again", new GUIStyling(marginTop: 300, horizontalFloat: HorizontalAlign.Center));
            playAgainButton.Clicked += PlayAgainButtonClicked;
            quitButton = new TextedButton("Quit", new GUIStyling(marginTop: 400, horizontalFloat: HorizontalAlign.Center));
            quitButton.Clicked += QuitButtonClicked;

            AddGameObject(playAgainButton);
            AddGameObject(quitButton);
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
