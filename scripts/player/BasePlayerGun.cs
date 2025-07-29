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

	public Camera3D playerCameraNode;
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
	public PackedScene BulletHoleScene;
	public RecoilControl recoilNode;

	public override void _Ready()
	{
		initialPosition = Stats.DefaultCameraPosition;
		adsPosition = Stats.ADSCameraPosition;
		model.Position = initialPosition;
		_aimSpeed = Stats.AimSpeed;

		FireDelayTimer = new Timer();
		FireDelayTimer.OneShot = true;
		FireDelayTimer.WaitTime = Stats.FireRate;
		FireDelayTimer.Timeout += () => ResetAttackCooldown(false);
		AddChild(FireDelayTimer);

		BulletHoleScene = GD.Load<PackedScene>("res://scenes/misc/bulletDecal.tscn");
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

	public bool Attack()
	{
		// TODO: this
		if (canFire)
		{
			FireDelayTimer.Stop();
			animPlayer.Stop();
			animPlayer.Play("Attack");
			PerformAttack();
			recoilNode.ApplyRecoil(Stats.RecoilForce);
			canFire = false;
			FireDelayTimer.Start();
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool AttackAuto()
	{
		if (Stats.WeaponType == WeaponTypes.Automatic)
		{
			return Attack();
		}
		else return false;
	}

	public virtual void PerformAttack()
	{
		var spaceState = GetWorld3D().DirectSpaceState;
		var screenCenter = GetViewport().GetVisibleRect().Size / 2;

		var origin = playerCameraNode.ProjectRayOrigin(screenCenter);
		var end = origin + playerCameraNode.ProjectRayNormal(screenCenter) * Stats.Range;
		var query = PhysicsRayQueryParameters3D.Create(origin, end);

		query.CollideWithBodies = true;
		query.CollisionMask = 5;

		var result = spaceState.IntersectRay(query);
		if (result.ContainsKey("collider"))
		{
			var bHole = BulletHoleScene.Instantiate<Node3D>(); // haha
			GetTree().Root.AddChild(bHole);
			bHole.GlobalPosition = (Vector3)result["position"];
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

	public void ResetAttackCooldown(bool resetAnim = true)
	{
		canFire = true;
		if (resetAnim)
		{
			animPlayer.Play("Idle");
		}
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
