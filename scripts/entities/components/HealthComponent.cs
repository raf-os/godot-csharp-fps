using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node3D
{
    [Export]
    private float MaxHealth = 1.0f;

    private float _Health;

    [Signal]
    public delegate void EntityDeathEventHandler();

    public override void _Ready()
    {
        _Health = MaxHealth;
    }

    public void OnHealthChange(float amount)
    {
        _Health = Mathf.Clamp(_Health + amount, 0, MaxHealth);
        if (_Health <= 0f)
        {
            EmitSignal(SignalName.EntityDeath);
        }
    }
}