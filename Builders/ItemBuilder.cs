using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Items;

namespace HealthyBusiness.Builders
{
    public static class ItemBuilder
    {
        public static Item CreateFries(TileLocation location)
        {
            return new Item(location, "items\\fries");
        }

        public static Item CreateColonelFries(TileLocation location)
        {
            return new Item(location, "items\\colonel_fries");
        }
    }
}
