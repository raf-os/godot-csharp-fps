using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class StateMachine : Node
{
	[Export]
	public Node initialState;

	private Dictionary<Type, State> _states;
	private State _currentState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_states = new Dictionary<Type, State>();
		foreach (Node node in GetChildren())
		{
			if (node is State s)
			{
				_states[s.GetType()] = s;
				s.fsm = this;
				s.OnReady();
				s.Exit();
			}
		}

		if (initialState is State)
		{
			_currentState = (State)initialState;
			_currentState.Enter();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_currentState.OnUpdate((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState.OnPhysicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState.OnHandleInput(@event);
		@event.Dispose();
	}

	private void SetState(State state)
	{
		if (_currentState == state)
		{
			return;
		}

		_currentState.Exit();
		_currentState = state;
		_currentState.Enter();
	}

	public void Transition<T>() where T : State
	{
		if (_states.TryGetValue(typeof(T), out var _state))
		{
			SetState(_state);
		}
		else
		{
			GD.PrintErr("Error switching state: state does not exist.");
		}
	}

	public void Transition(string stateName)
	{
		if (GetNodeOrNull<State>(stateName) is State s)
		{
			SetState(s);
		}
		else
		{
			GD.PrintErr("Error switching state: state does not exist.");
		}
	}
}
