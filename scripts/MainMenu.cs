using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private PackedScene FirstLevel;

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
