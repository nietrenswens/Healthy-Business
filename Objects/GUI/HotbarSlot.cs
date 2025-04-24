using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
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
    /// <summary>
    /// foreach item in the hotbar, a slot needs to be drawn.
    /// </summary>
    public class HotbarSlot : GameObject
    {
        private int id;
        private Item? _item; // is nullable because not every slot has an item
        public Texture2D? rectangle;

        public Vector2 position;

        public HotbarSlot(Item? item)
        {
            this.id = new Random().Next(1, 90); // TODO: change this to the right id
            this._item = item;
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

        public void Draw(SpriteBatch spriteBatch, HotbarSlot? previousSlot, Item? item)
        {
            base.Draw(spriteBatch);

            // TODO: kijken hoe we de X en Y dynamisch kunnen maken zodat het beneden komt te staan
            // TODO: item texture moet gecentreerd gedrawd worden in de slot

            // in order to make the spacing even, we need to calculate the position of the previous slot
            int margin = 20;

            if (previousSlot != null)
            {
                float x = previousSlot.LocalPosition.X + 100 + margin;

                this.LocalPosition = new Vector2(x, this.LocalPosition.Y);
            } else
            {
                this.LocalPosition = new Vector2(Globals.MAPWIDTH, 0);
            }

            spriteBatch.Draw(rectangle, new Rectangle((int)LocalPosition.X, (int)LocalPosition.Y, 100, 100), Color.LightGray);

        }
    }
}
