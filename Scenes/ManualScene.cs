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
    public class ManualScene : Scene
    {
        private Text? manualTitle;
        private Text? manualText;
        private Text? manualGoal;
        private Text? manualControls;
        private ImageButton? backButton;

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            AddGameObject(new ColoredBackground(new Color(40, 54, 78)));

            manualTitle = new Text("fonts\\pixelated_elegance\\title", "Manual", Color.MediumTurquoise,
                guiStyling: new GUIStyling(marginTop: 100, horizontalFloat: HorizontalAlign.Center));
            manualText = new Text("fonts\\pixelated_elegance\\small",
                "Well well well. So you're lucky enough to have survived this nuclear disaster together with me!\n" +
                "I would call this fate, to be the last one surviving together with my lifelong rival... #EnemiesToLovers\n" +
                "You see, this now post-apocalyptic country has been ravaged by this nuclear disaster.\n" +
                "AND WORST OF ALL! Mutated vegetables have wiped out my beloved fast food chains... :,(\n" +
                "Before you got here, I went to work and put my personal brand \"HEALTHY BUSINESS\" on all fast food chains.\n" +
                "You know, just in case everything goes back to normal one day.\n" +
                "So, to make you useful for once, I want YOU to search for the legendary lost treasures of those fast food chains.\n" +
                "He he.\n\n\n\n\n" +
                "Gather fast food items in the Healthy Business fast food chain.\n" +
                "Then, sell them to me (The Dam) and reach the quota I give to you at the start of each workweek.\n" +
                "Each workweek consists of 3 shifts, meaning you have 3 entire days to sell me the goods. Else... you'll see.\n" +
                "And like I said before, mutated vegetables have taken over these fast food chains. So, be warned.\n\n\n\n\n" +
                "W - Walk Up\n" +
                "A - Walk Left\n" +
                "S - Walk Down\n" +
                "D - Walk Right\n" +
                "E - Pick Up/Sell Items", Color.LightYellow,
                guiStyling: new GUIStyling(marginTop: 180, horizontalFloat: HorizontalAlign.Center));
            manualGoal = new Text("fonts\\pixelated_elegance\\large", "Goal:", Color.MediumTurquoise,
                guiStyling: new GUIStyling(marginTop: 390, horizontalFloat: HorizontalAlign.Left));
            manualControls = new Text("fonts\\pixelated_elegance\\large", "Controls:", Color.MediumTurquoise,
                guiStyling: new GUIStyling(marginTop: 550, horizontalFloat: HorizontalAlign.Left));
            AddGameObject(manualTitle);
            AddGameObject(manualText);
            AddGameObject(manualGoal);
            AddGameObject(manualControls);

            backButton = new ImageButton(new GUIStyling(marginLeft: -10, marginTop: -10, horizontalFloat: HorizontalAlign.Right, verticalFloat: VerticalAlign.Bottom));
            backButton.Clicked += BackButtonClicked;
            AddGameObject(backButton);
        }

        private void BackButtonClicked(object? sender, EventArgs e)
        {
            GameManager.GetGameManager().ChangeScene(new MainMenu());
        }
    }
}
