using HealthyBusiness.Data;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.InGameGUIObjects
{
    public class QuotaText : GameObject
    {
        public int lastQuota = -1;
        public int lastBalance = -1;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var gameData = GameManager.GetGameManager().GameData;
            var quotaAmount = gameData.Quota.Amount;
            var balance = gameData.Balance;

            if (quotaAmount != lastQuota || balance != lastBalance)
            {
                lastQuota = quotaAmount;
                lastBalance = balance;
                AddText();
            }
        }

        private void AddText()
        {
            var textObj = GetGameObject<Text>();
            if (textObj == null)
            {
                textObj = new Text("fonts\\pixelated_elegance\\large", $"{GameManager.GetGameManager().GameData.Balance}/{GameManager.GetGameManager().GameData.Quota.Amount}", Color.White, new()
                {
                    verticalFloat = VerticalAlign.Bottom,
                    horizontalFloat = HorizontalAlign.Right,
                    marginRight = 50f,
                });
                Add(textObj);
            }
            else
            {
                textObj.TextString = $"{lastBalance}/{lastQuota}";
            }
        }
    }
}
