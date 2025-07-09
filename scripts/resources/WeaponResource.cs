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
    public string Name;
    [Export]
    public PackedScene ViewModel;
    [Export]
    public PackedScene WorldModel;

    [ExportSubgroup("Stats")]
    [Export]
    public float Damage;
    [Export]
    public GunTypes AttackType;

    public WeaponResource() : this(null, null, null, 0f) { }
    public WeaponResource(
        string name,
        PackedScene viewModel,
        PackedScene worldModel,
        float damage = 0f
        )
    {
        Name = name;
        ViewModel = viewModel;
        WorldModel = worldModel;
        Damage = damage;
    }
}
