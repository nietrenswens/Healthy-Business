using HealthyBusiness.Objects.GUI;
using System.Collections.Generic;

namespace HealthyBusiness.Data
{
    public class GameData
    {
        public int ShiftCount { get; set; } = 0;
        public int Balance { get; set; } = 0;
        public List<HotbarSlot> HotbarSlots { get; set; }
        public Quota Quota { get; set; }

        public GameData()
        {
            HotbarSlots = new();
            Quota = new Quota(1, this);
        }
    }
}
