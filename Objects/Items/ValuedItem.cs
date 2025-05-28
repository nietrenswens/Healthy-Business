using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using System;

namespace HealthyBusiness.Objects.Items
{
    public class ValuedItem : Item
    {
        public int Price;
        public ItemRarity ItemRarity;

        public ValuedItem(TileLocation tileLocation, string textureName, string name, ItemRarity rarity) : base(tileLocation, textureName, name)
        {
            ItemRarity = rarity;
            Price = InitilizePrice(rarity);
        }

        private int InitilizePrice(ItemRarity rarity)
        {
            Random rng = GameManager.GetGameManager().RNG;

            switch (rarity)
            {
                case ItemRarity.Low:
                    return rng.Next(1, 15);
                case ItemRarity.Medium:
                    return rng.Next(16, 30);
                case ItemRarity.High:
                    return rng.Next(31, 60);
                default:
                    throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
            }
        }
    }
}
