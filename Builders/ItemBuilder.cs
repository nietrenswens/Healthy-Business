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
            return new ValuedItem(location, "items\\fries", "Fries", ItemRarity.Low);
        }

        public static Item CreateColonelFries(TileLocation location)
        {
            return new ValuedItem(location, "items\\colonel_fries", "Colonel fries", ItemRarity.High);
        }

        public static Item CreateBurger(TileLocation location)
        {
            return new ValuedItem(location, "items\\burger", "Burger", ItemRarity.Medium);
        }

        public static Item CreateDinoNugget1(TileLocation location)
        {
            return new ValuedItem(location, "items\\dino1", "Dino nugget", ItemRarity.Low);
        }

        public static Item CreateDinoNugget2(TileLocation location)
        {
            return new ValuedItem(location, "items\\dino2", "Dino nugget", ItemRarity.Medium);
        }

        public static Item CreateKetchup(TileLocation location)
        {
            return new ValuedItem(location, "items\\ketchup", "Ketchup", ItemRarity.Low);
        }

        public static GameObject CreateRandomItem(TileLocation randomTileLocation)
        {
            Func<Item>[] itemCreationMethods = new[]
            {
                () => CreateFries(randomTileLocation),
                () => CreateColonelFries(randomTileLocation),
                () => CreateBurger(randomTileLocation),
                () => CreateDinoNugget1(randomTileLocation),
                () => CreateDinoNugget2(randomTileLocation)
            };

            int index = GameManager.GetGameManager().RNG.Next(itemCreationMethods.Length);
            return itemCreationMethods[index]();
        }
    }
}
