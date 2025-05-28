using HealthyBusiness.Engine.Managers;

namespace HealthyBusiness.Builders
{
    public static class LevelBuilder
    {
        public static void AddDefaultLevel(this LevelManager levelManager)
        {
            levelManager.Levels.Add(new("Maps\\test\\order_room.tmx", "order_room", bottomLevelId: "restroom", topLevelId: "kitchen", rightLevelId: "party_room"));
            levelManager.Levels.Add(new("Maps\\test\\restroom.tmx", "restroom", topLevelId: "order_room"));
            levelManager.Levels.Add(new("Maps\\test\\kitchen.tmx", "kitchen", bottomLevelId: "order_room"));
            levelManager.Levels.Add(new("Maps\\test\\party_room.tmx", "party_room", leftlevelId: "order_room"));
        }

        public static void AddApartment(this LevelManager levelManager)
        {
            levelManager.Levels.Add(new("Maps\\test\\apartment.tmx", "apartment"));
        }
    }
}
