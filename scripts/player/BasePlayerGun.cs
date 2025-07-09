using Godot;
using System;

public partial class BasePlayerGun : Node3D, IWeapon
{
	[Export]
	private AnimationPlayer animPlayer;
	[Export]
	private Marker3D ADSPosition;

	private Transform3D ADSOffset;

	private bool canFire = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ADSOffset = ADSPosition.Transform;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public bool Attack()
	{
		// TODO: this
		if (canFire)
		{
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
	}

	public Transform3D GetADSOffset()
	{
		return ADSOffset;
	}
}
