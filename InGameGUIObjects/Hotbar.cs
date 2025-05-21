using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Levels;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace HealthyBusiness.InGameGUIObjects
{
    public class Hotbar : GameObject
    {
        private const int AMOUNT_OF_SLOTS = Globals.HOTBAR_SLOTS;

        public List<HotbarSlot> HotbarSlots = new List<HotbarSlot>();

        public CurrentScore hotbarCurrentScore = new CurrentScore();

        public Hotbar()
        {
            InitializeHotbarSlots();
        }

        public void InitializeHotbarSlots()
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                HotbarSlot hotbarSlot = new HotbarSlot();
                hotbarSlot.Load(GameManager.GetGameManager().ContentManager);
                HotbarSlots.Add(hotbarSlot);
            }
        }


        public void SelectNextSlot(ScrollDirection scrollDirection)
        {
            GameLevel currentLevel = (GameLevel)GameManager.GetGameManager().CurrentLevel;
            
            HotbarSlot selectedSlot = GetSelectedSlot();

            var currentItemMeta = currentLevel.GUIObjects.Attributes.OfType<ItemMetaData>().FirstOrDefault();

            if (currentItemMeta != null)
            {
                currentLevel.GUIObjects.Remove(currentItemMeta);
            }

            int selectedIndex = 0;

            selectedSlot.isSelected = false;

            if (scrollDirection == ScrollDirection.Down)
            {
                selectedIndex = (HotbarSlots.IndexOf(selectedSlot) + 1) % AMOUNT_OF_SLOTS;
            }
            else
            {
                selectedIndex = (HotbarSlots.IndexOf(selectedSlot) - 1 + AMOUNT_OF_SLOTS) % AMOUNT_OF_SLOTS;
            }

            HotbarSlots[selectedIndex].isSelected = true;

            var currentItem = HotbarSlots[selectedIndex].Item;

            if(currentItem != null)
            {
                currentLevel.GUIObjects.Add(new ItemMetaData(
                    (ValuedItem)currentItem)
                );
            }
        }

        public bool AddItem(ValuedItem item)
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                var slot = HotbarSlots[i];
                if (slot.Item == null)
                {
                    slot.Item = item;

                    // update the score
                    hotbarCurrentScore.UpdateScore(this);

                    return true;
                }

            }

            return false; // no empty slot 
        }

        public override void Unload()
        {
            base.Unload();
            HotbarSlots.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw the hotbar slots
            for (int i = 0; i < HotbarSlots.Count; i++)
            {
                HotbarSlot slot = HotbarSlots[i];

                slot.Draw(
                    spriteBatch,
                    (i > 0)                             
                        ? HotbarSlots[i - 1]     
                        : null                             
                );
            }


            // draw the hotbar container // TODO: niet nodig waarschijnlijk
            //Texture2D hotbarcontainer = new Texture2D(spriteBatch.GraphicsDevice, 100, 100);
            base.Draw(spriteBatch);
        }

        private HotbarSlot GetSelectedSlot()
        {
            return HotbarSlots.Where(slot => slot.isSelected).FirstOrDefault() ?? HotbarSlots[0];
        }
    }

    public enum ScrollDirection
    {
        Up,
        Down
    }
}
