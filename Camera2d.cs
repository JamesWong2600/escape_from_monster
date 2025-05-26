using Godot;
using System;

public partial class Camera2d : Camera2D
{
	public override void _Ready()
	{

		GetNode<Camera2D>("Camera2D").MakeCurrent();
	}
	/*private Node2D player;
	// Called when the node enters the scene tree for the first time.


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (player != null)
		{
			GlobalPosition = player.GlobalPosition;
		}
	
	}
	*/
}
