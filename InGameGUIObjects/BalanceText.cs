using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.InGameGUIObjects
{
    public class BalanceText : GameObject
    {
        public int lastBalance { get; private set; } = -1;

        public BalanceText()
        {
            var balance = GameManager.GetGameManager().GameData.Balance;
            AddText(balance);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var balance = GameManager.GetGameManager().GameData.Balance;
            if (balance != lastBalance)
            {
                lastBalance = balance;
                AddText(balance);
            }
        }

        private void AddText(int balance)
        {
            var textObj = GetGameObject<Text>();
            if (textObj == null)
            {
                textObj = new Text("fonts\\pixelated_elegance\\large", $"Balance: {balance}", Color.White, guiStyling: new()
                {
                    verticalFloat = VerticalAlign.Bottom,
                    horizontalFloat = HorizontalAlign.Right,
                    marginRight = 50f,
                });
                Add(textObj);
            }
            else
            {
                textObj.TextString = $"Balance: {balance}";
            }
        }
    }
}
