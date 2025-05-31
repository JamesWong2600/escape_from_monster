using Godot;
using System;

// Class for handling the "Return to Main Page" button when the game is won
public partial class return_to_main_page_win : TouchScreenButton
{


    // Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		// Connect the Pressed signal to the OnButtonPressed method
		Pressed += OnButtonPressed;
	}

	// Method triggered when the button is pressed
	private void OnButtonPressed()
	{
		// Get reference to the main screen node
		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen/main_screen");
		if (main_screen == null)
		{
			GD.PrintErr("main_screen node not found at path!");
			return;
		}

        // Get reference to the main screen background sprite
		Sprite2D main_screen_background = GetParent().GetParent().GetNode<Sprite2D>("main_screen/main_screen_background");
		if (main_screen_background == null)
		{
			GD.PrintErr("main_screen_background node not found at path!");
			return;
		}


        // Get reference to the win game screen sprite
		Sprite2D wingame_screen = GetParent().GetNode<Sprite2D>("wingame_screen");
		if (wingame_screen == null)
		{
			GD.PrintErr("wingame_screen node not found at path!");
			return;
		}
		GD.Print("wingame_screen node found, proceeding to hide it. 2");


        // Get reference to the button node
		TouchScreenButton Button = GetParent().GetNode<TouchScreenButton>("Button");
		if (Button == null)
		{
			GD.PrintErr("Button node not found at path!");
			return;
		}
		GD.Print("return_to_main_page node found, proceeding to hide it. 3");


        // Get reference to the "Return to Main Win" button
		TouchScreenButton return_to_main_win = GetParent().GetNode<TouchScreenButton>("return_to_main_win");
		if (return_to_main_win == null)
		{
			GD.PrintErr("return_to_main_win node not found at path!");
			return;
		}
		GD.Print("return_to_main_win node found, proceeding to hide it. 4");


		// Update visibility states for various nodes
		return_to_main_win.Visible = false; // Hide the return to main win button
		main_screen.Visible = true; // Show the main screen
		wingame_screen.Visible = false;
		Button.Visible = false; // Hide the button
		main_screen_background.Visible = true; // Show the main screen background
		GD.Print("Button pressed! Returning to main page. 5");


	}

}
