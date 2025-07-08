using Godot;
using System;

public partial class State : Node
{
	public StateMachine fsm;

	public virtual void Enter() { }
	public virtual void Exit() { }

	public virtual void OnReady() { }
	public virtual void OnUpdate(float delta) { }
	public virtual void OnPhysicsUpdate(float delta) { }
	public virtual void OnHandleInput(InputEvent @event) {}
}
