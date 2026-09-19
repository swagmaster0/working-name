using Godot;
using System;

[GlobalClass]
public partial class Creature : CharacterBody2D
{
	[Export] private float Speed = 220.0f;
	[Export] private float JumpVelocity = 400.0f;
	[Export] private float SizeX = 1.0f;
	[Export] private float SizeY = 1.0f;
	[Export] private bool AdhereToGravity = true;


	public override void _PhysicsProcess(double delta)
	{
		if (AdhereToGravity)
		{
			if (!IsOnFloor())
			{
				Velocity += GetGravity() * (float)delta;
			}

			MoveAndSlide();
		}
	}

	public float GetSpeed() { return Speed; }
	public float GetJumpVelocity() { return JumpVelocity; }
	public float GetSizeX() { return SizeX; }
	public float GetSizeY() { return SizeY; }
	public bool IsAdheringToGravity() { return AdhereToGravity; }

}
