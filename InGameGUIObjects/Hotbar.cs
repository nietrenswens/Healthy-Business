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

        public bool AddItem(Item item)
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
                                                       //hotbarItems[i] // ready for when the player can add to the hotbar from the itempickup module       
                );
            }

            //System.Diagnostics.Debug.WriteLine(HotbarSlots.Count);

            // draw the hotbar container // TODO: niet nodig waarschijnlijk
            //Texture2D hotbarcontainer = new Texture2D(spriteBatch.GraphicsDevice, 100, 100);
            base.Draw(spriteBatch);
        }

    }
}
