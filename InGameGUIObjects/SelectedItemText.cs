using HealthyBusiness.Engine;
using HealthyBusiness.Objects.Creatures.Player;
using HealthyBusiness.Objects.Creatures.Player.Modules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.InGameGUIObjects
{
    public class SelectedItemText : GameObject
    {
        private SpriteFont _font = null!;

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (Parent is not Player)
                throw new Exception("SelectedItemText can only be added to a Player.");
            _font = content.Load<SpriteFont>("fonts\\pixelated_elegance\\small");
        }

        public override void Update(GameTime gameTime)
        {
            var selectedItem = ((Player)Parent!).GetGameObject<ItemPickupModule>()!.SelectedItem;
            if (selectedItem == null)
                return;
            WorldPosition = selectedItem.Center + new Vector2(0, 20);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var selectedItem = ((Player)Parent!).GetGameObject<ItemPickupModule>()!.SelectedItem;
            if (selectedItem == null)
                return;

            var text = "Press e to pick up " + selectedItem.Name;
            var textSize = _font.MeasureString(text);
            var pos = WorldPosition + new Vector2(0, -40) + new Vector2(-textSize.X / 2, -textSize.Y / 2);
            spriteBatch.DrawString(_font, text, pos, Color.White);
        }
    }
}
