using Godot;
using System;


public partial class return_to_main_page_failed : TouchScreenButton
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{
		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen/main_screen");
		if(main_screen == null)
		{
			GD.PrintErr("main_screen node not found at path!");
			return;
		}
		Sprite2D main_screen_background = GetParent().GetParent().GetNode<Sprite2D>("main_screen/main_screen_background");
		if(main_screen_background == null)
		{
			GD.PrintErr("main_screen_background node not found at path!");
			return;
		}

		Sprite2D wingame_screen = GetParent().GetNode<Sprite2D>("gameover");
		if(wingame_screen == null)
		{
			GD.PrintErr("wingame_screen node not found at path!");
			return;
		}
		GD.Print("wingame_screen node found, proceeding to hide it. 2");

		TouchScreenButton Button = GetParent().GetNode<TouchScreenButton>("Button");
		if(Button == null)
		{
			GD.PrintErr("Button node not found at path!");
			return;
		}
		GD.Print("return_to_main_page node found, proceeding to hide it. 3");
		TouchScreenButton return_to_main_win = GetParent().GetNode<TouchScreenButton>("return_to_main_failed");
		if(return_to_main_win == null)
		{
			GD.PrintErr("return_to_main_win node not found at path!");
			return;
		}
		GD.Print("return_to_main_win node found, proceeding to hide it. 4");
		return_to_main_win.Visible = false; 
		main_screen.Visible = true; 
		wingame_screen.Visible = false;
		Button.Visible = false; 
		main_screen_background.Visible = true; 
		GD.Print("Button pressed! Returning to main page. 5");


	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
