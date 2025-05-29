using Godot;
using System;

public partial class StartButton : TouchScreenButton
{

	private Sprite2D Key;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{
		Key = GetParent().GetNode<Sprite2D>("main_screen_background");
		if (Key == null)
		{
			GD.Print("this not found in the parent node.");
			return;
		}
		GD.Print("Button pressed!");
		Key.Visible = false;
		Visible = false;
		Character.gamestart = true; // Assuming character is a globally accessible object or singleton
		
		// Add your logic here (e.g., start the game, load a new scene, etc.)
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
