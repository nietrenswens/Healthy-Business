using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Levels;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.Objects.GUI
{
    // <summary>
    // This class handles getting the total score of the hotbar and displaying it in the GUI.
    // </summary>
    public class CurrentScore : GameObject
    {
        public int Score { get; set; }
        
        public CurrentScore()
        {
            Score = 0;
        }

        public void UpdateScore(Hotbar hotbar)
        {
            // get the hotbar 
            //GameLevel currentLevel = (GameLevel)GameManager.GetGameManager().CurrentLevel;

            //Hotbar? hotbar = currentLevel.GUIObjects.Attributes.OfType<Hotbar>().FirstOrDefault() ?? null;
            // loop through the items of the hotbar and count the scores
            int totalScore = 0;

            if (hotbar == null) return;

            foreach (HotbarSlot hotbarSlot in hotbar.HotbarSlots)
            {
                //totalScore += item.price
                if(hotbarSlot.Item != null)
                {
                    ValuedItem item = hotbarSlot.Item;
                    totalScore += item.Price;
                }
            }

            // update the total score of the class
            Score = totalScore;

            System.Diagnostics.Debug.WriteLine(Score);

        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            GameLevel currentLevel = (GameLevel)GameManager.GetGameManager().CurrentLevel;

            currentLevel.GUIObjects.Add(
                new Text("fonts\\pixelated_elegance\\med", Score.ToString(), Color.White, new GUIStyling(marginTop: 0, horizontalFloat: HorizontalAlign.Center))
            );
        }
    }
}
