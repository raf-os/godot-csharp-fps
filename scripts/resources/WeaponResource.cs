using Godot;
using System;

public enum GunTypes
{
    SemiAutomatic,
    Automatic,
    Melee
}

[GlobalClass]
public partial class WeaponResource : Resource
{
    [Export]
    public PackedScene ViewModel;
    [Export]
    public PackedScene WorldModel;

    [ExportSubgroup("Stats")]
    [Export]
    public float Damage;
    [Export]
    public GunTypes AttackType;

    public WeaponResource() : this(null, null, 0f) { }
    public WeaponResource(
        PackedScene viewModel,
        PackedScene worldModel,
        float damage = 0f
        )
    {
        ViewModel = viewModel;
        WorldModel = worldModel;
        Damage = damage;
    }
}
