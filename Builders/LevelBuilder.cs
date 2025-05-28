using HealthyBusiness.Engine.Levels;
using HealthyBusiness.Engine.Managers;

namespace HealthyBusiness.Builders
{
    public static class LevelBuilder
    {
        public static void AddDefaultLevel(this LevelManager levelManager)
        {
            levelManager.Levels.Add(new JobLevel("Maps\\test\\order_room.tmx", "order_room", bottomLevelId: "restroom", topLevelId: "kitchen", rightLevelId: "party_room"));
            levelManager.Levels.Add(new JobLevel("Maps\\test\\restroom.tmx", "restroom", topLevelId: "order_room"));
            levelManager.Levels.Add(new JobLevel("Maps\\test\\kitchen.tmx", "kitchen", bottomLevelId: "order_room"));
            levelManager.Levels.Add(new JobLevel("Maps\\test\\party_room.tmx", "party_room", leftlevelId: "order_room"));
        }

        public static void AddApartment(this LevelManager levelManager)
        {
            levelManager.Levels.Add(new JoblessLevel("Maps\\test\\apartment.tmx", "apartment"));
        }
    }
}
