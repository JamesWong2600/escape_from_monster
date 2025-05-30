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
		Key = GetParent().GetParent().GetNode<Sprite2D>("main_screen_background");
		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		main_screen.Visible = false; // Hide the main screen
		AnimatedSprite2D Monster_1_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-1/AnimatedSprite2D");
		AnimatedSprite2D Monster_2_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-2/AnimatedSprite2D");
		AnimatedSprite2D Monster_3_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-3/AnimatedSprite2D");
		AnimatedSprite2D Monster_4_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-4/AnimatedSprite2D");
		AnimatedSprite2D Monster_5_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-5/AnimatedSprite2D");
		if (Monster_1_animatedSprite2D == null)
		{
			GD.Print("this Monster_1_animatedSprite2D  not found in the parent node.");
			return;
		}
		GD.Print("Button pressed!");
		Monster_1_animatedSprite2D.Play("run");
		Monster_2_animatedSprite2D.Play("run");
		Monster_3_animatedSprite2D.Play("run");
		Monster_4_animatedSprite2D.Play("run");
		Monster_5_animatedSprite2D.Play("run");
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
