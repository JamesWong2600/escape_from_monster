using Godot;
using System;


public partial class return_to_main_page_win : TouchScreenButton
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{


		Node2D main_screen = GetParent().GetNode<Node2D>("wingame_screen");
		Node2D wingame_screen = GetParent().GetParent().GetNode<Node2D>("main_screen/main_screen");
		main_screen.Visible = true; // Show the main screen
		wingame_screen.Visible = false;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
