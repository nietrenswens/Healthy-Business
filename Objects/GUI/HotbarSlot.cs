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
        public ValuedItem? Item; // is nullable because not every slot has an item
        public Texture2D? rectangle;

        public Vector2 position;

        public bool isSelected = false;

        public HotbarSlot(ValuedItem? item = null)
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

            // positioning of the slot
            if (previousSlot != null)
            {
                float x = previousSlot.LocalPosition.X + Globals.HOTBAR_SLOT_SIZE + Globals.HOTBAR_SLOT_MARGIN;
                LocalPosition = new Vector2(x, LocalPosition.Y);
            }
            else
            {
                int numberOfSlots = Globals.HOTBAR_SLOTS;
                float x = Globals.SCREENWIDTH / 2f - (numberOfSlots * (Globals.HOTBAR_SLOT_SIZE + Globals.HOTBAR_SLOT_MARGIN)) / 2f;
                LocalPosition = new Vector2(x, LocalPosition.Y);
            }

            // scale the slot if it is selected
            double sideSize = isSelected ? Globals.HOTBAR_SLOT_SIZE * 1.1 : Globals.HOTBAR_SLOT_SIZE;
            float offset = (float)(sideSize - Globals.HOTBAR_SLOT_SIZE) / 2f;
            float scale = (float)(sideSize / Globals.HOTBAR_SLOT_SIZE);

            // arne slot drawing ;)
            spriteBatch.Draw(
                rectangle,
                new Rectangle(
                    (int)(LocalPosition.X - offset),
                    (int)(LocalPosition.Y - offset),
                    (int)sideSize,
                    (int)sideSize),
                Color.LightGray
            );

            if (Item != null)
            {
                var itemTexture = Item.Texture;

                if (itemTexture != null)
                {
                    // scale the new item width and height with the scale of the slot
                    int itemWidth = (int)(itemTexture.Width * scale);
                    int itemHeight = (int)(itemTexture.Height * scale);

                    // scale the position of the texture with the scale of the slot
                    var itemPosition = new Vector2(
                        (float)(LocalPosition.X - offset + (sideSize / 2) - (itemWidth / 2)),
                        (float)(LocalPosition.Y - offset + (sideSize / 2) - (itemHeight / 2))
                    );

                    spriteBatch.Draw(
                        itemTexture,
                        new Rectangle((int)itemPosition.X, (int)itemPosition.Y, itemWidth, itemHeight),
                        Color.White
                    );
                }
            }
        }

    }
}
