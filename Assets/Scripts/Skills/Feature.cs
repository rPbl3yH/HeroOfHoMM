using System;

[Flags]
public enum Feature
{
    Colldown = 1 << 0,
    DPS = 1 << 1,
    Damage = 1 << 2,
    Radius = 1 << 3,
    Duration = 1 << 4,
    ProjectileCount = 1 << 5
}
