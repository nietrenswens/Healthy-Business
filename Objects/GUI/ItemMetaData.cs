using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.Objects.GUI
{
    class ItemMetaData : GameObject
    {
        private Item _item;
        public ItemMetaData(Item item)
        {
            _item = item;
        }

        public override void Load(ContentManager content)
        {
            Add(new Text("fonts\\pixelated_elegance\\medium", $"{_item.Name} ${_item.price}", Color.White, guiStyling: new(
                verticalFloat: VerticalAlign.Bottom, marginBottom: Globals.HOTBAR_SLOT_SIZE + 20, horizontalFloat: HorizontalAlign.Center)));
        }


    }
}
