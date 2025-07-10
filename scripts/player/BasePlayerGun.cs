using Godot;
using System;
using WeaponSystem;

[GlobalClass]
public partial class BasePlayerGun : Node3D, IWeapon
{
	[Export]
	private AnimationPlayer animPlayer;
	[Export]
	private Marker3D ADSPosition;
	[Export]
	public WeaponStatsResource Stats;

	private Transform3D ADSOffset;

	private bool canFire = true;
	private Vector2 mouseMovement = Vector2.Zero;
	private Vector2 minWeaponSway = new Vector2(-10, -10);
	private Vector2 maxWeaponSway = new Vector2(10, 10);
	private float positionWeaponSway = 0.1f;
	private float rotationWeaponSway = 30f;

	public override void _Ready()
	{
		ADSOffset = ADSPosition.Transform;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion)
		{
			mouseMovement = motion.Relative;
		}
	}

	private void ApplyWeaponSway(float delta)
	{
		// TODO
	}

	public override void _Process(double delta)
	{
	}

	public bool Attack()
	{
		// TODO: this
		if (canFire)
		{
			animPlayer.Play("Attack");
			canFire = false;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void OnMove()
	{
		throw new NotImplementedException();
	}

	public void OnIdle()
	{
		throw new NotImplementedException();
	}

	public void OnAir()
	{
		throw new NotImplementedException();
	}

	public void OnReload()
	{
		throw new NotImplementedException();
	}

	public void ResetAttackCooldown()
	{
		canFire = true;
		animPlayer.Play("Idle");
	}

	public Transform3D GetADSOffset()
	{
		return ADSOffset;
	}

    public void OnEquip()
    {
		return;
    }

    public void OnUnequip()
    {
		return;
    }
}
