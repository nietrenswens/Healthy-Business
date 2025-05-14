using HealthyBusiness.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.Objects.Items
{
    public class ValuedItem : Item
    {
        public int price;

        public ItemRarity ItemRarity;
        public ValuedItem(TileLocation tileLocation, string textureName, string name, ItemRarity rarity) : base(tileLocation, textureName, name)
        {
            ItemRarity = rarity;

            price = InitilizePrice(rarity);
        }
        private int InitilizePrice(ItemRarity rarity)
        {
            Random random = new Random();

            switch (rarity)
            {
                case ItemRarity.Low:
                    return random.Next(1, 15);
                case ItemRarity.Medium:
                    return random.Next(16, 30);
                case ItemRarity.High:
                    return random.Next(31, 60);
                default:
                    throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
            }
        }
    }
}
