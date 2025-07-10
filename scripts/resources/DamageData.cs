using System;
using Godot;

[GlobalClass]
public partial class DamageData : Resource
{
    [Export]
    public float Physical { get; private set; }

    public DamageData() : this(0f) { }
    public DamageData(
        float physical = 0f
        )
    {
        Physical = physical;
    }
}