using HealthyBusiness.Objects.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.InGameGUIObjects
{
    class Hotbar
    {
        List<Item> hotbarItems;

        public Hotbar()
        {
            this.hotbarItems = new List<Item>();
        }

        public void Add(Item item) => hotbarItems.Add(item);

        public void Remove(Item item)
        {
            if (hotbarItems.Contains(item))
            {
                hotbarItems.Remove(item);
            }
        }
    }
}
