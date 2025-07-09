using Godot;
using System;

namespace WeaponSystem;

public enum WeaponTypes
{
    SemiAutomatic,
    Automatic,
    Melee
}

[GlobalClass]
public partial class WeaponStatsResource : Resource
{
    [Export]
    public WeaponTypes WeaponType;
}