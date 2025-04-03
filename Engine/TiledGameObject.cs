using HealthyBusiness.Engine.Utils;

namespace HealthyBusiness.Engine
{
    public abstract class TiledGameObject : GameObject
    {
        public TileLocation TileLocation { get; set; }

        public TiledGameObject(TileLocation tileLocation)
        {
            TileLocation = tileLocation;
            LocalPosition = tileLocation.ToVector2();
        }
    }
}
