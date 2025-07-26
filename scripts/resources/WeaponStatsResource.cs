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

    [ExportSubgroup("Camera Positions")]
    [Export]
    public Vector3 DefaultCameraPosition;
    [Export]
    public Vector3 ADSCameraPosition;

    [ExportSubgroup("Stats")]
    [Export]
    public float Damage = 1.0f;
    [Export]
    public float AimSpeed = 1.0f;
    [Export]
    public float FireRate = 1.0f;

    public WeaponStatsResource() : this(null, WeaponTypes.None, 1.0f, 1.0f, 1.0f) { }

    public WeaponStatsResource(
        string name,
        WeaponTypes weaponType,
        float damage = 1.0f,
        float aimSpeed = 1.0f,
        float fireRate = 1.0f
    )
    {
        Name = name;
        WeaponType = weaponType;
        Damage = damage;
        AimSpeed = aimSpeed;
        FireRate = fireRate;
    }
}