using System;

namespace HealthyBusiness.Collision;

[Flags]
public enum CollisionGroup
{
    None = 0,
    Player = 1,
    Floor = 2,
    Wall = 3,
}