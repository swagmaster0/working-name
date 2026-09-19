using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private float Speed = DEFAULT_SPEED;
	[Export] private float JumpVelocity = DEFAULT_JUMP_VELOCITY;
	[Export] private float SizeX = 1.0f;
	[Export] private float SizeY = 1.0f; 

	private const float DEFAULT_SPEED = 220.0f;
	private const float DEFAULT_JUMP_VELOCITY = 400.0f;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 size = Scale;
		size.X = SizeX;
		size.Y = SizeY;
		Scale = size;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = -JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnHitboxBodyEntered(Node2D body)
	{
		if (body is Creature creature)
		{
			// set creature parameters
			Speed = creature.GetSpeed();
			JumpVelocity = creature.GetJumpVelocity();
			SizeX = creature.GetSizeX();
			SizeY = creature.GetSizeY();
		}
	}
}
