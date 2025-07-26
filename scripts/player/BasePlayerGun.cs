using Godot;
using System;
using WeaponSystem;

[GlobalClass]
public partial class BasePlayerGun : Node3D, IWeapon
{
	[Export]
	private Node3D model;
	[Export]
	private AnimationPlayer animPlayer;
	[Export]
	public WeaponStatsResource Stats;

	

	private bool canFire = true;
	private bool isADS = false;
	private Vector2 mouseMovement = Vector2.Zero;
	private Vector2 minWeaponSway = new Vector2(-10, -10);
	private Vector2 maxWeaponSway = new Vector2(10, 10);
	private float positionWeaponSway = 0.1f;
	private float rotationWeaponSway = 30f;
	private Vector3 initialPosition = Vector3.Zero;
	private Vector3 adsPosition;
	private Tween adsTween;
	private float _aimSpeed;
	private Timer FireDelayTimer;

	public override void _Ready()
	{
		initialPosition = Stats.DefaultCameraPosition;
		adsPosition = Stats.ADSCameraPosition;
		model.Position = initialPosition;
		_aimSpeed = Stats.AimSpeed;

		FireDelayTimer = new Timer();
		FireDelayTimer.OneShot = true;
		FireDelayTimer.WaitTime = Stats.FireRate;
		FireDelayTimer.Timeout += () =>
		{
			canFire = true;
		};
		AddChild(FireDelayTimer);
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
			FireDelayTimer.Stop();
			animPlayer.Stop();
			animPlayer.Play("Attack");
			canFire = false;
			FireDelayTimer.Start();
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void SecondaryAttackPress()
	{
		adsTween?.Kill();
		adsTween = CreateTween();
		adsTween.TweenProperty(
			model,
			"position",
			adsPosition,
			_aimSpeed
		);
		adsTween.SetEase(Tween.EaseType.InOut);
		isADS = true;
	}

	public void SecondaryAttackRelease()
	{
		adsTween?.Kill();
		adsTween = CreateTween();
		adsTween.TweenProperty(
			model,
			"position",
			initialPosition,
			_aimSpeed
		);
		adsTween.SetEase(Tween.EaseType.InOut);
		isADS = false;
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

    public void OnEquip()
    {
		return;
    }

    public void OnUnequip()
    {
		return;
    }
}
