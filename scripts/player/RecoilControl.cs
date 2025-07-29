using Godot;
using System;

namespace WeaponSystem;

[GlobalClass]
public partial class RecoilControl : Node3D
{
	[Export]
	private float _recoilRecovery = 5f;
	public float recoilAmount = 0f;
	private float actualRecoil = 0f;
	private float maxRecoil = 20f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void ApplyRecoil(float amount)
	{
		recoilAmount = Mathf.Min(recoilAmount + amount, maxRecoil + _recoilRecovery);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		actualRecoil = Mathf.MoveToward(actualRecoil, Mathf.Min(recoilAmount, maxRecoil), 40f * (float)delta);
		if (!Mathf.IsZeroApprox(recoilAmount))
		{
			recoilAmount = Mathf.Lerp(recoilAmount, 0f, _recoilRecovery * (float)delta);
		}

		Rotation = new Vector3(
			actualRecoil * (Mathf.Pi / 180f),
			Rotation.Y,
			Rotation.Z
		);
	}
}
