using Godot;
using System;

namespace WeaponSystem;

[GlobalClass]
public partial class RecoilControl : Node3D
{
	[Export]
	private float _recoilRecovery = 5f;
	public float recoilAmount = 0f;
	private Vector2 recoilPosition;
	private float smoothRecoil = 0f;
	private float maxRecoil = 10f;

	public void ApplyRecoil(float amount)
	{
		recoilAmount = Mathf.Min(recoilAmount + amount, maxRecoil * 1.5f);
	}

	public override void _Process(double delta)
	{
		if (!Mathf.IsZeroApprox(recoilAmount))
		{
			recoilAmount = Mathf.Lerp(recoilAmount, 0f, _recoilRecovery * (float)delta);
		}
		smoothRecoil = Mathf.Lerp(smoothRecoil, recoilAmount, 10f * (float)delta);

		Rotation = new Vector3(
			smoothRecoil * (Mathf.Pi / 180f),
			Rotation.Y,
			Rotation.Z
		);
	}
}
