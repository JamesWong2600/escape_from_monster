using Godot;
using System;

public partial class To_Register_page : TouchScreenButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{

		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		if (main_screen == null)
		{
			GD.Print("main_screen node not found at path!");
		}
		else
		{
			GD.Print("main_screen node found, proceeding to hide it.");
		}
		Node2D register_page = GetParent().GetParent().GetNode<Node2D>("register_page");
		
		if (register_page == null)
		{
			GD.Print("register_page node not found at path!");
		}
		else
		{
			GD.Print("register_page not found, proceeding to show it.");
		}
		main_screen.Visible = false; // Show the main screen
		register_page.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
