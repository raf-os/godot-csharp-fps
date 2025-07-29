using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node3D
{
    [Export]
    public float MaxHealth = 1.0f;

    private float _Health;

    private bool isDying = false;

    [Signal]
    public delegate void EntityDeathEventHandler();
    [Signal]
    public delegate float EntityHealthChangeEventHandler();

    public override void _Ready()
    {
        _Health = MaxHealth;
    }

    public float GetCurrentHealth()
    {
        return _Health;
    }

    public void OverrideHealth(float amount)
    {
        _Health = Mathf.Min(MaxHealth, amount);
    }

    public void OnHealthChange(float amount)
    {
        if (isDying) return;
        _Health = Mathf.Clamp(_Health + amount, 0, MaxHealth);
        EmitSignal(SignalName.EntityHealthChange, _Health);
        if (_Health <= 0f)
        {
            EmitSignal(SignalName.EntityDeath);
            isDying = true;
        }
    }
}