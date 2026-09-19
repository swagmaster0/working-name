using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private Label TitleLabel;
	[Export] private Button PlayButton;
	[Export] private PackedScene FirstLevel;

	public override void _Ready()
	{
		TitleLabel.Text = (string)ProjectSettings.GetSetting("application/config/name");
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

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
