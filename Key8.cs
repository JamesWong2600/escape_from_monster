using Godot;
using System;

public partial class Key8 : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.

	public float Speed = 300;
	private Vector2 direction = Vector2.Zero;

	public int Health = 100;

	private Sprite2D Key;
	private Node2D player = null;
	public override void _Ready()
	{
		GD.Print("start");
		Key = GetNode<Sprite2D>("sprite2D");
		if (Key == null)
		{
			GD.Print("Key sprite not found in the parent node.");
		}
		else
		{
			GD.Print("Key founded.");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public override void _PhysicsProcess(double delta)
	{
		if (player != null) 
		{
			Key.Visible = false; // Show the key when the player is in range
			GD.Print("Player entered the monster's area.");
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

}
