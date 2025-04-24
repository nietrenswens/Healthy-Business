using HealthyBusiness.Engine.Utils;

namespace HealthyBusiness.Controllers.PathFinding
{
    public class Step
    {
        public Step? Previous { get; set; }
        public float Cost { get; set; }
        public TileLocation Location { get; set; }

        public Step(float cost, TileLocation location, Step? previous = null)
        {
            Previous = previous;
            Cost = cost;
            Location = location;
        }
    }
}
