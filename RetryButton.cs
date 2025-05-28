using Godot;
using System;

public partial class RetryButton : Button
{

	private Sprite2D Background;

	private CharacterBody2D thecharacter;
	private CharacterBody2D monster1;
    private CharacterBody2D monster2;
	private CharacterBody2D monster3;
	private CharacterBody2D monster4;
	private CharacterBody2D monster5;

	private Sprite2D key1;
	private Sprite2D key2;
	private Sprite2D key3;
	private Sprite2D key4;

    private Sprite2D key5;
	private Sprite2D key6;
	private Sprite2D key7;
    private Sprite2D key8;
	private CollisionShape2D key1_collisionShape;
	private CollisionShape2D key2_collisionShape;
    private CollisionShape2D key3_collisionShape; 
    private CollisionShape2D key4_collisionShape;
	private CollisionShape2D key5_collisionShape;
	private CollisionShape2D key6_collisionShape;
    private CollisionShape2D key7_collisionShape;
	private CollisionShape2D key8_collisionShape;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
		Disabled = true;
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{
		Background = GetParent().GetNode<Sprite2D>("gameover");
		if (Background == null)
		{
			GD.Print("Key sprite not found in the parent node.");
			return;
		}
		GD.Print("Button pressed!");
		Background.Visible = false;
		Visible = false;
		Disabled = true;
		character.gamestart = true; // Assuming character is a globally accessible object or singleton
		timer.countdownTime = 300f;
		thecharacter = GetParent().GetParent().GetNode<CharacterBody2D>("character");
		monster1 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-1");
		monster2 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-2");
		monster3 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-3");
		monster4 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-4");
		monster5 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-5");
		if (monster1 == null || monster2 == null || monster3 == null || monster4 == null || monster5 == null)
		{
			GD.Print("One or more monsters are not found in the parent node.");
			return;
		}
		GD.Print("Monsters found and reset.");
		
		key1 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-1/Sprite2D");
		key1_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
		/*key2 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
		key2_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
		key3 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-3/Sprite2D");
		key3_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
		key4 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-4/Sprite2D");
		key4_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
		key5 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-5/Sprite2D");
		key5_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
		key6 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-6/Sprite2D");
		key6_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
		key7 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-7/Sprite2D");
		key7_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
		key8 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
		key8_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");*/
		if (key1 == null || key2 == null || key3 == null || key4 == null || key5 == null || key6 == null || key7 == null || key8 == null)
		{
			GD.Print("One or more keys are not found in the parent node.");
			return;
		}
		GD.Print("Keys found and reset.");
		key1.Visible = true;
		key1_collisionShape.Disabled = false;
		

		
		monster1.Position = Monster1.startPosition;
		monster2.Position = Monster2.startPosition;
		monster3.Position = Monster3.startPosition;
		monster4.Position = Monster4.startPosition;
		monster5.Position = Monster5.startPosition;
		if (thecharacter == null)
		{
			GD.Print("that is not found in the parent node.");
			return;
		}
		thecharacter.Position = character.startPosition; // Reset character position to the start position
		// Add your logic here (e.g., start the game, load a new scene, etc.)
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
