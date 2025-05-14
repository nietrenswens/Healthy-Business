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

        private HotbarSlot GetSelectedSlot()
        {
            return HotbarSlots.Where(slot => slot.isSelected).FirstOrDefault() ?? HotbarSlots[0];
        }

        public void SelectNextSlot(bool moveToNextSlot)
        {
            GameLevel currentLevel = (GameLevel)GameManager.GetGameManager().CurrentLevel;
            
            HotbarSlot selectedSlot = GetSelectedSlot();

            var currentItemMeta = currentLevel.GUIObjects.Attributes.OfType<ItemMetaData>().FirstOrDefault();

            if (currentItemMeta != null)
            {
                currentLevel.GUIObjects.Remove(currentItemMeta);
            }

            int selectedIndex = 0;

            if (moveToNextSlot)
            {
                selectedSlot.isSelected = false;
                selectedIndex = (HotbarSlots.IndexOf(selectedSlot) + 1) % AMOUNT_OF_SLOTS;
                HotbarSlots[selectedIndex].isSelected = true;
                ShowMetaData(HotbarSlots[selectedIndex].Item);
                //System.Diagnostics.Debug.WriteLine(HotbarSlots[nextIndex].Item);
            }
            else
            {
                selectedSlot.isSelected = false;
                selectedIndex = (HotbarSlots.IndexOf(selectedSlot) - 1 + AMOUNT_OF_SLOTS) % AMOUNT_OF_SLOTS;
                HotbarSlots[selectedIndex].isSelected = true;
            }

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
                    return true;
                }
            }

            return false; // no empty slot 
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
                    (i > 0)                             // check if te iteration is the first index
                        ? HotbarSlots[i - 1]     // if it is not the first iteration, pass the previous slot
                        : null                         // if it is the first iteration, pass null to prevent a index out of range exception     
                );
            }

            //System.Diagnostics.Debug.WriteLine(HotbarSlots.Count);

            // draw the hotbar container // TODO: niet nodig waarschijnlijk
            //Texture2D hotbarcontainer = new Texture2D(spriteBatch.GraphicsDevice, 100, 100);
            base.Draw(spriteBatch);
        }

        private void ShowMetaData(ValuedItem? item)
        {
            // TODO: black rectangle above the hotbar with the name of the item and price
            if (item == null) return;

            var font = GameManager.GetGameManager().ContentManager.Load<SpriteFont>("fonts\\pixelated_elegance\\small");

            var text = item.Name + " - " + item.price.ToString() + "DAM PIECES";

            //SpriteBatch spriteBatch = GameManager.GetGameManager().SpriteBatch;
        }
    }
}
