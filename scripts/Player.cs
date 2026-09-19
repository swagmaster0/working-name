using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private float Speed = DEFAULT_SPEED;
	[Export] private float JumpVelocity = DEFAULT_JUMP_VELOCITY;
	[Export] private float SizeX = 1.0f;
	[Export] private float SizeY = 1.0f; 
	[Export] private bool CanClimb = false;

	private const float DEFAULT_SPEED = 220.0f;
	private const float DEFAULT_JUMP_VELOCITY = 400.0f;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// adjust sizing
		Vector2 size = Scale;
		size.X = SizeX;
		size.Y = SizeY;
		Scale = size;

		if (MotionMode is MotionModeEnum.Grounded)
		{
			velocity = GetVelocityForGroundedMovement(velocity, (float)delta);
		}
		else
		{
			velocity = GetVelocityForFloatingMovement(velocity, (float)delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private Vector2 GetVelocityForGroundedMovement(Vector2 velocity, float delta)
	{
		if (!IsOnFloor())
		{
			velocity += GetGravity() * delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = -JumpVelocity;
		}

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (CanClimb)
		{
			KinematicCollision2D collision = GetLastSlideCollision();
			if (collision == null || !IsInstanceValid(collision)) return velocity;

			float dot = collision.GetNormal().Dot(direction.Normalized());
			if (dot <= -0.8f && Mathf.Abs(direction.Dot(Vector2.Up)) < 0.75)
			{
				velocity = Vector2.Up * Speed;
			}
		}


		return velocity;
	}

	private Vector2 GetVelocityForFloatingMovement(Vector2 velocity, float delta)
	{
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		velocity = direction * Speed;

		return velocity;
	}


	private void OnHitboxBodyEntered(Node2D body)
	{
		if (body is Creature creature) SetParametersToCreature(creature);
	}

	private void SetParametersToCreature(Creature creature)
	{
		// set creature parameters
		Speed = creature.GetSpeed();
		JumpVelocity = creature.GetJumpVelocity();
		SizeX = creature.GetSizeX();
		SizeY = creature.GetSizeY();

		if (creature.IsAdheringToGravity()) MotionMode = MotionModeEnum.Grounded;
		else MotionMode = MotionModeEnum.Floating;

		CanClimb = creature.CanClimb();
	}
}
