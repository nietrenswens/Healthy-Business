using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.GUI
{
    /// <summary>
    /// foreach item in the hotbar, a slot needs to be drawn.
    /// </summary>
    public class HotbarSlot : GameObject
    {
        public Item? Item; // is nullable because not every slot has an item
        public Texture2D? rectangle;

        public Vector2 position;

        public HotbarSlot(Item? item = null)
        {
            Item = item;
            LocalPosition = new Vector2(0, Globals.SCREENHEIGHT - Globals.HOTBAR_SLOT_SIZE - Globals.HOTBAR_SLOT_MARGIN);
        }

        public override void Load(ContentManager content)
        {
            rectangle = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            rectangle.SetData(new[] { Color.White });
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, HotbarSlot? previousSlot)
        {
            base.Draw(spriteBatch);

            // TODO: kijken hoe we de X en Y dynamisch kunnen maken zodat het beneden komt te staan
            // TODO: item texture moet gecentreerd gedrawd worden in de slot

            // in order to make the spacing even, we need to calculate the position of the previous slot
            if (previousSlot != null)
            {
                float x = previousSlot.LocalPosition.X + Globals.HOTBAR_SLOT_SIZE + Globals.HOTBAR_SLOT_MARGIN;

                LocalPosition = new Vector2(x, this.LocalPosition.Y);
            }
            else
            {
                var numberOfSlots = Globals.HOTBAR_SLOTS;
                var x = Globals.SCREENWIDTH / 2 - (numberOfSlots * (Globals.HOTBAR_SLOT_SIZE + Globals.HOTBAR_SLOT_MARGIN)) / 2;
                LocalPosition = new Vector2(x, LocalPosition.Y);
            }

            spriteBatch.Draw(rectangle, new Rectangle((int)LocalPosition.X, (int)LocalPosition.Y, Globals.HOTBAR_SLOT_SIZE, Globals.HOTBAR_SLOT_SIZE), Color.LightGray);

            if (Item != null)
            {
            }

        }
    }
}
