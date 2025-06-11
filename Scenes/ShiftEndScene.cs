using HealthyBusiness.Cameras;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Linq;

namespace HealthyBusiness.Scenes
{
    public class ShiftEndScene : Scene
    {
        private float _timeCounter = 0f; // ms
        private float _timeToWait = 4000f; // ms

        private const string TITLE_FONT = "fonts\\pixelated_elegance\\title";
        private const string LARGE_FONT = "fonts\\pixelated_elegance\\large";

        public ShiftEndScene()
        {
            GameManager.GetGameManager().GameData.ShiftCount += 1;
            SetCamera(new DefaultCamera());

            var gameData = GameManager.GetGameManager().GameData;

            AddGameObject(new ColoredBackground(new Color(40, 54, 78)));
            AddGameObject(new Text(TITLE_FONT, "You have finished your " + GetShiftName() + " shift", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
                marginBottom = 200f
            }));

            AddGameObject(new Text(TITLE_FONT, "Stats", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
            }));

            AddGameObject(new Text(LARGE_FONT, $"Shift day: {gameData.ShiftCount}", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
                marginTop = 100f
            }));

            int daysUntilDeadline = (gameData.Quota.Deadline - gameData.ShiftCount);

            string deadlineText = daysUntilDeadline > 0
                ? $"Deadline in {daysUntilDeadline} day(s)"
                : "Deadline today";

            AddGameObject(new Text(LARGE_FONT, deadlineText, Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
                marginTop = 220f
            }));

            AddGameObject(new Text(LARGE_FONT, $"Balance: {gameData.Balance}", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
                marginTop = 140f
            }));

            var hotbarValue = gameData.HotbarSlots.Sum(slot => slot.Item?.Price);
            AddGameObject(new Text(LARGE_FONT, $"Hotbar value: {hotbarValue}", Color.White, guiStyling: new()
            {
                horizontalFloat = HorizontalAlign.Center,
                verticalFloat = VerticalAlign.Center,
                marginTop = 180f
            }));
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _timeCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeCounter >= _timeToWait)
            {
                _timeCounter = 0f;
                GameManager.GetGameManager().ChangeScene(new GameScene(GameSceneType.Apartment));
            }
        }

        private string GetShiftName()
        {
            var shiftCount = GameManager.GetGameManager().GameData.ShiftCount;
            return $"{shiftCount}{GetOrdinalSuffix(shiftCount)}";
        }

        private string GetOrdinalSuffix(int number)
        {
            if (number <= 0) return "th";

            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
                return "th";

            switch (number % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
    }
}
