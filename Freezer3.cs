using Godot;
using System;

public partial class Freezer3 : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.

	public float Speed = 300;
	private Vector2 direction = Vector2.Zero;

	public int Health = 100;

	private Sprite2D The_Freezer;
	private Node2D player = null;
	public override void _Ready()
	{
		GD.Print("start");
		The_Freezer = GetNode<Sprite2D>("sprite2D");
		if (The_Freezer == null)
		{
			GD.Print("Sword sprite not found in the parent node.");
		}
		else
		{
			GD.Print("Sword founded.");
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
			The_Freezer.Visible = false;
			GD.Print("Player entered the sword area.");
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
