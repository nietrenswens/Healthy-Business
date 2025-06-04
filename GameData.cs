using HealthyBusiness.Objects.GUI;
using System;
using System.Collections.Generic;

namespace HealthyBusiness
{
    public class GameData
    {
        public int ShiftCount { get; set; } = 0;
        public int Balance { get; set; } = 0;
        public List<HotbarSlot> HotbarSlots { get; set; }

        public int EmployerLevel { get; set; } = 1;

        public int Deadline { get; set; } = 0;

        public int Quota { get; set; } = 0;

        public GameData()
        {
            HotbarSlots = new();
            Deadline = ShiftCount + 3;
        }
    }
}
