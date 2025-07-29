using Godot;
using System;

public partial class BulletDecal : Node3D
{
	private SceneTreeTimer despawnTimer;

	public override void _Ready()
	{
		despawnTimer = GetTree().CreateTimer(5.0f);
		despawnTimer.Timeout += BeginDespawn;
	}

	private void BeginDespawn()
	{
		QueueFree();
	}
}
