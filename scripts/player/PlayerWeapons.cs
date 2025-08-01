using Godot;
using System;
using WeaponSystem;

namespace PlayerComponents;

public partial class PlayerWeapons : Node3D
{
	[Export]
	private Camera3D CameraNode;
	[Export]
	private RecoilControl recoilNode;

	private Godot.Collections.Array<BasePlayerGun> weapons = [];
	private int selectedWeaponIndex = 0;
#nullable enable
	private BasePlayerGun? currentWeapon;
#nullable disable

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is BasePlayerGun childGun) // America, fuck yeah
			{
				weapons.Add(childGun);
				childGun.playerCameraNode = CameraNode;
				childGun.recoilNode = recoilNode;
			}
		}
		EquipWeapon(0);
	}

	public bool EquipWeapon(int index)
	{
		if (index <= weapons.Count)
		{
			if (currentWeapon != null)
			{
				currentWeapon.OnUnequip();
			}
			selectedWeaponIndex = index;
			currentWeapon = weapons[index];
			currentWeapon.OnEquip();
			return true;
		}
		else
		{
			return false;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			currentWeapon.Attack();
		}
		else if (@event.IsActionPressed("secondaryAttack"))
		{
			currentWeapon.SecondaryAttackPress();
		}
		else if (@event.IsActionReleased("secondaryAttack"))
		{
			currentWeapon.SecondaryAttackRelease();
		}
	}

	public override void _Process(double delta)
	{
		GlobalTransform = CameraNode.GlobalTransform;
		if (Input.IsActionPressed("attack"))
		{
			currentWeapon.AttackAuto();
		}
	}
}
