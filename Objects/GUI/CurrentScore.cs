﻿using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Items;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Linq;

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
            // loop through the items of the hotbar and count the scores
            int totalScore = 0;

            if (hotbar == null) return;

            foreach (HotbarSlot hotbarSlot in hotbar.HotbarSlots)
            {
                //totalScore += item.price
                if (hotbarSlot.Item != null)
                {
                    ValuedItem item = hotbarSlot.Item;
                    totalScore += item.Price;
                }
            }

            // update the total score of the class
            Score = totalScore;

            AddText();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var hotbar = ((GameScene)GameManager.GetGameManager().CurrentScene).GUIObjects.Attributes.OfType<Hotbar>().FirstOrDefault();

            if (hotbar != null)
            {
                UpdateScore(hotbar);
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            AddText();
        }

        public void AddText()
        {
            if (Components.OfType<Text>().Any())
            {
                Remove(Components.OfType<Text>().First());
            }

            Add(
                new Text("fonts\\pixelated_elegance\\title", Score.ToString(), Color.White, guiStyling: new GUIStyling(marginTop: 0, horizontalFloat: HorizontalAlign.Right))
            );
        }
    }
}
