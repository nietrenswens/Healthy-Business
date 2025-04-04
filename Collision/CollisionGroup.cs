using System;

namespace HealthyBusiness.Collision;

[Flags]
public enum CollisionGroup
{
    None = 0,
    Floor = 1,
    Player = 2,
    Solid = 4,
}