using Godot;
using System;

public partial class To_Login_page : TouchScreenButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{

		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		Node2D register_page = GetParent().GetParent().GetNode<Node2D>("login_page");
		main_screen.Visible = false; // Show the main screen
		register_page.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
