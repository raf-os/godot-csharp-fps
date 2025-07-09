using System;
using Godot;

[GlobalClass]
public partial class DamageData : Resource
{
    [Export]
    public float Physical { get; private set; }

    public DamageData() { }
    public DamageData(
        float physical)
    {
        Physical = physical;
    }
}