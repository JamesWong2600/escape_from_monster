using Godot;
using System;


public partial class return_to_main_page : TouchScreenButton
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{


		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		Node2D register_page = GetParent().GetParent().GetNode<Node2D>("ranking_page");
		main_screen.Visible = true; // Show the main screen
		register_page.Visible = false;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
