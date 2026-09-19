using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private Button PlayButton;
	[Export] private PackedScene FirstLevel;

	public override void _Ready()
	{
		PlayButton.GrabFocus();
	}


	private void OnPlayButtonPressed()
	{
		if (FirstLevel is null || !IsInstanceValid(FirstLevel))
		{
			GD.PushError("First level not set");
			return;
		}

		GetTree().ChangeSceneToPacked(FirstLevel);
	}
}
