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
        public int lastQuota = 0;

        public QuotaText()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Quota quota = GameManager.GetGameManager().GameData.Quota;

            if (quota.amount != lastQuota)
            {
                lastQuota = quota.amount;
            }
            AddText();
        }

        private void AddText()
        {
            var textObj = GetGameObject<Text>();
            if (textObj == null)
            {
                textObj = new Text("fonts\\pixelated_elegance\\large", $"{GameManager.GetGameManager().GameData.Balance}/{GameManager.GetGameManager().GameData.Quota.amount}", Color.White, new()
                {
                    verticalFloat = VerticalAlign.Bottom,
                    horizontalFloat = HorizontalAlign.Right,
                    marginRight = 50f,
                });
                Add(textObj);
            }
            else
            {
                textObj.TextString = $"{GameManager.GetGameManager().GameData.Balance}/{GameManager.GetGameManager().GameData.Quota.amount}";
            }
        }
    }
}
