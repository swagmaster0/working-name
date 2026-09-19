using Godot;
using System;

public partial class Spider : Creature
{
	private Vector2 _fixedCeilingPosition;

	public override void _Ready()
	{
		// Tells Godot to run the original script's setup first
		base._Ready();

		// Saves the exact position you placed the spider in the editor
		_fixedCeilingPosition = GlobalPosition;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Tells Godot to run the original script's physics loop
		base._PhysicsProcess(delta);

		// Forces the spider back to the ceiling after the original script tries to drop it
		GlobalPosition = _fixedCeilingPosition;
	}
}
