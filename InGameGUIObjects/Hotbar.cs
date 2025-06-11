using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Objects.Items;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.InGameGUIObjects
{
    public class Hotbar : GameObject
    {
        private const int AMOUNT_OF_SLOTS = Globals.HOTBAR_SLOTS;

        public List<HotbarSlot> HotbarSlots = new List<HotbarSlot>();

        public Hotbar()
        {
            GameScene? currentLevel = ((GameScene?)GameManager.GetGameManager().CurrentlyLoadingScene);
            currentLevel?.GUIObjects.Add(new CurrentScore());
            IsPersistent = true;
        }

        public Hotbar(List<HotbarSlot> hotbarSlots) : this()
        {
            HotbarSlots = hotbarSlots;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (HotbarSlots.Count > 0)
            {
                // If the hotbar is already loaded, we don't need to initialize it again.
                return;
            }
            InitializeHotbarSlots(content);
        }

        public void InitializeHotbarSlots(ContentManager content)
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                HotbarSlot hotbarSlot = new HotbarSlot();
                hotbarSlot.Load(content);
                HotbarSlots.Add(hotbarSlot);
            }
        }


        public void SelectNextSlot(ScrollDirection scrollDirection)
        {
            GameScene currentLevel = (GameScene)GameManager.GetGameManager().CurrentScene;

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

            if (currentItem != null)
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

        public override void Unload()
        {
            GameManager.GetGameManager().GameData.HotbarSlots = [.. HotbarSlots];
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

            base.Draw(spriteBatch);
        }

        private HotbarSlot GetSelectedSlot()
        {
            return HotbarSlots.Where(slot => slot.isSelected).FirstOrDefault() ?? HotbarSlots[0];
        }

        public void Clear()
        {
            HotbarSlots.ForEach(slot => slot.Item = null);
        }
    }

    public enum ScrollDirection
    {
        Up,
        Down
    }
}
