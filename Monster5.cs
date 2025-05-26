using Godot;
using System;

public partial class Monster5 : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	private Vector2 startPosition = new Vector2(4706f, 2875f);

	public float Speed = 300;
	private Vector2 direction = Vector2.Zero;

	public int Health = 100;
	private Node2D player = null;
	private RandomNumberGenerator rng = new();
	public override void _Ready()
	{
		Position = startPosition;
		rng.Randomize();
		PickNewDirection();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		if (player != null)
		{
			Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
			GD.Print("Player entered the monster's area.");
			MoveAndSlide();
		}
		Vector2 velocity = direction * Speed;
		KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);

		if (collision != null)
		{
			PickNewDirection(); // Change direction on collision
		}
	}

	public void TakeDamage(int amount)
	{
		Health -= amount;
		if (Health <= 0)
		{
			QueueFree(); // Destroy the monster
		}
	}

	private void OnArea2DBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			player = body;
		}
	}

	private void OnArea2DBodyExited(Node2D body)
	{
		if (body == player)
		{
			player = null;
		}
	}
	private void PickNewDirection()
	{
		// Pick a random normalized direction (up, down, left, right, or diagonal)
		float angle = rng.RandfRange(0, Mathf.Tau); // Tau = 2π
		direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).Normalized();
	}
}
