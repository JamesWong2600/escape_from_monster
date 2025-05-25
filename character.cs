using Godot;
using System;

public partial class character : CharacterBody2D
{
	private Vector2 startPosition = new Vector2(0, 0);
	private Vector2 targetPosition = new Vector2(1000, 0);
	public float BgWidth = 3780f;   // Set to your background width
	public float BgHeight = 2100f;  // Set to your background height
	public Vector2 CharacterSize = new Vector2(32, 32);
	private float moveDuration = 2.0f; // seconds
	private float elapsedTime = 0.0f;
	private bool moving = true;
	public float Speed = 600f;
	public override void _Ready()
	{
		GD.Print("Hello, Godot!");
		Position = startPosition;
		elapsedTime = 0.0f;
		moving = true;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 input = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up")
		);

		if (input.Length() > 0)
		{
			input = input.Normalized();
			//Position += input * Speed * (float)delta;
			Vector2 newPosition = Position + input * Speed * (float)delta;

			newPosition.X = Mathf.Clamp(newPosition.X, 100, BgWidth - CharacterSize.X);
			newPosition.Y = Mathf.Clamp(newPosition.Y, 100, BgHeight - CharacterSize.Y);

			Position = newPosition;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("right"))
			direction.X += 1;
		if (Input.IsActionPressed("left"))
			direction.X -= 1;
		if (Input.IsActionPressed("down"))
			direction.Y += 1;
		if (Input.IsActionPressed("up"))
			direction.Y -= 1;

		direction = direction.Normalized();
		Velocity = direction * Speed;

		MoveAndSlide();  // Moves and checks collisions internally

		int collisionCount = GetSlideCollisionCount();
		for (int i = 0; i < collisionCount; i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			GD.Print("Collided with: ", collision.GetCollider());
		}
	}
}
