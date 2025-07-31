using Godot;
using System;

[GlobalClass]
public partial class BaseEnemy : RigidBody3D, IDamageable
{
    [ExportGroup("Nodes")]
    [Export]
    HealthComponent healthComponent;
    [Export]
    ProgressBar HealthBar;

    public override void _Ready()
    {
        healthComponent.EntityDeath += OnHealthDepleted;
        healthComponent.EntityHealthChange += UpdateHealthValue;
    }

    public void TakeDamage(float amount)
    {
        healthComponent.OnHealthChange(amount);
    }

    public void UpdateHealthValue(float amount)
    {
        HealthBar.Value = amount / healthComponent.MaxHealth * 100f;
    }

    public void OnHealthDepleted()
    {
        QueueFree();
    }
}
