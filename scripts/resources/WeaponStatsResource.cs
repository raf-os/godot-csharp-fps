using Godot;
using System;

namespace WeaponSystem;

public enum WeaponTypes
{
    None,
    SemiAutomatic,
    Automatic,
    Melee
}

[GlobalClass]
public partial class WeaponStatsResource : Resource
{
    [Export]
    public string Name;
    [Export]
    public WeaponTypes WeaponType;
    [ExportSubgroup("Stats")]
    [Export]
    public float Damage;

    public WeaponStatsResource() : this(null, WeaponTypes.None, 0f) {}

    public WeaponStatsResource(
        string name,
        WeaponTypes weaponType,
        float damage = 0f
    )
    {
        Name = name;
        WeaponType = weaponType;
        Damage = damage;
    }
}