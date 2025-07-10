using Godot;
using System;
using PlayerComponents;

public partial class PlayerWeapons : Node3D
{
	[Export]
	private Camera3D CameraNode;

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
			bool canAttack = currentWeapon.Attack();
		}
	}

	public override void _Process(double delta)
	{
		GlobalTransform = CameraNode.GlobalTransform;
	}
}
