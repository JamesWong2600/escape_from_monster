using Godot;
using System;


public partial class return_to_main_page_from_login : TouchScreenButton
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{


		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		Node2D login_page = GetParent().GetParent().GetNode<Node2D>("login_page");
		main_screen.Visible = true; // Show the main screen
		login_page.Visible = false;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
