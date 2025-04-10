using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Items;
using System;

namespace HealthyBusiness.Builders
{
    public static class ItemBuilder
    {
        public static Item CreateFries(TileLocation location)
        {
            return new Item(location, "items\\fries", "Fries");
        }

        public static Item CreateColonelFries(TileLocation location)
        {
            return new Item(location, "items\\colonel_fries", "Colonel fries");
        }

        public static GameObject CreateRandomItem(TileLocation randomTileLocation)
        {
            Func<Item>[] itemCreationMethods = new[]
            {
                () => CreateFries(randomTileLocation),
                () => CreateColonelFries(randomTileLocation)
            };

            int index = GameManager.GetGameManager().RNG.Next(itemCreationMethods.Length);
            return itemCreationMethods[index]();
        }
    }
}
