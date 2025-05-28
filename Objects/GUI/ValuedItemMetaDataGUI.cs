using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HealthyBusiness.Objects.GUI
{
    public class ValuedItemMetaDataGUI : GameObject
    {
        public ValuedItem Item { get; set; }

        public ValuedItemMetaDataGUI(ValuedItem item)
        {
            Item = item;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            Add(new Text(
                "fonts\\pixelated_elegance\\medium",
                $"{Item.Name} (${Item.Price})",
                Color.White,
                new()
                {
                    horizontalFloat = HorizontalAlign.Center,
                    verticalFloat = VerticalAlign.Bottom,
                    marginBottom = Globals.HOTBAR_SLOT_SIZE + Globals.HOTBAR_SLOT_MARGIN + 20f,
                }));
        }

    }
}
