using Godot;
using System;

public partial class PauseMenu : Control
{
	private Button ResumeButton;
	private Button QuitButton;
	public override void _Ready()
	{
		ResumeButton = GetNode<Button>("%ResumeButton");
		ResumeButton.Pressed += OnResumeButtonPressed;
		QuitButton = GetNode<Button>("%QuitButton");
		QuitButton.Pressed += OnQuitButtonPressed;
		Visible = false;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("uiPause"))
		{
			HandlePause();
		}
	}

	private void HandlePause()
	{
		bool isPaused = GetTree().Paused;
		GetTree().Paused = !isPaused;
		if (isPaused)
		{
			Hide();
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		else
		{
			Show();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	private void OnResumeButtonPressed()
	{
		HandlePause();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
