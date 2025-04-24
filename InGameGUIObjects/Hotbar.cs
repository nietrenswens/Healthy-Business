using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.InGameGUIObjects
{
    public class Hotbar : GameObject
    {
        private const int AMOUNT_OF_SLOTS = Globals.HOTBAR_SLOTS;

        private List<HotbarSlot> createdHotbarSlots = new List<HotbarSlot>();

        public Item[] hotbarItems;

        public Hotbar()
        {
            this.hotbarItems = new Item[AMOUNT_OF_SLOTS];
            InitializeHotbarSlots();
        }

        public void InitializeHotbarSlots()
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                HotbarSlot hotbarSlot = new HotbarSlot(hotbarItems[i] ?? null);
                hotbarSlot.Load(GameManager.GetGameManager().ContentManager);
                createdHotbarSlots.Add(hotbarSlot);
            }
        }

        public bool Add(Item item)
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                if (hotbarItems[i] == null)
                {
                    hotbarItems[i] = item;
                    return true;
                }
            }

            return false; // no empty slot 
        }

        public bool Remove(Item item)
        {
            for (int i = 0; i < AMOUNT_OF_SLOTS; i++)
            {
                if (hotbarItems[i] == item)
                {
                    hotbarItems[i] = null;
                    return true;
                }
            }
            return false; // item not found
        }

        public void Clear()
        {
            //_hotbarItems.Clear();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw the hotbar slots
            for (int i = 0; i < createdHotbarSlots.Count; i++)
            {
                HotbarSlot slot = createdHotbarSlots[i];

                slot.Draw(
                    spriteBatch,
                    (i > 0)                             // check if te iteration is the first index
                        ? createdHotbarSlots[i - 1]     // if it is not the first iteration, pass the previous slot
                        : null                         // if it is the first iteration, pass null to prevent a index out of range exception
                                                       //hotbarItems[i] // ready for when the player can add to the hotbar from the itempickup module       
                );
            }

            //System.Diagnostics.Debug.WriteLine(createdHotbarSlots.Count);

            // draw the hotbar container // TODO: niet nodig waarschijnlijk
            //Texture2D hotbarcontainer = new Texture2D(spriteBatch.GraphicsDevice, 100, 100);
            base.Draw(spriteBatch);
        }

    }
}
