using System;

namespace HealthyBusiness.Collision;

[Flags]
public enum CollisionGroup
{
    None = 0,
    Player = 1,
    Solid = 2,
}