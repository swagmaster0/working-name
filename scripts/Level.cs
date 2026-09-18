using Godot;
using System;

[GlobalClass]
public partial class Level : Node2D
{
	[Export] private Area2D WinArea;
	[Export] private PackedScene NextLevel;

	public override void _PhysicsProcess(double delta)
	{
		if (WinArea == null || !IsInstanceValid(WinArea))
		{
			GD.PushError("Win area is not set for this level. The game is effectively softlocked");
			return;
		}

		foreach (Node2D node in WinArea.GetOverlappingBodies())
		{
			if (node is Player plr)
			{
				if (NextLevel == null || !IsInstanceValid(NextLevel))
				{
					GD.PushError("Next level is not set. The game is effectively softlocked");
					return;
				}

				GetTree().ChangeSceneToPacked(NextLevel);
			}
		}
	}

}
