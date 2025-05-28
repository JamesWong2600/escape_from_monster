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
	private CharacterBody2D monster_boss;

	private StaticBody2D key1;
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
	private StaticBody2D freezer1;
	private StaticBody2D freezer2;
	private StaticBody2D freezer3;
	private CollisionShape2D freezer1_collisionShape;
    private CollisionShape2D freezer2_collisionShape;
	private CollisionShape2D freezer3_collisionShape;
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
		thecharacter.Position = character.startPosition; 
		monster1 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-1");
		monster2 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-2");
		monster3 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-3");
		monster4 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-4");
		monster5 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-5");
		monster_boss = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
		if (monster1 == null || monster2 == null || monster3 == null || monster4 == null || monster5 == null)
		{
			GD.Print("One or more monsters are not found in the parent node.");
			return;
		}
		GD.Print("Monsters found and reset.");
		
		key1 = GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-1");
		key1_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
		/*key2 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
		key2_collisionShape = GetParent().GetParent().GetNode<CollisionShapekeyset/StaticBody2D-key-2/CollisionShape2D");
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
		if (key1 == null)
		{
			GD.Print("One or more keys are not found in the parent node.");
			return;
		}
		GD.Print("Keys found and reset.");
		key1.Visible = true;
		key1_collisionShape.Disabled = false;
		freezer1 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-1");
		freezer1_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-1/CollisionShape2D");
		freezer2 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-2");
		freezer2_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-2/CollisionShape2D");
		freezer3 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-3");
		freezer3_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-3/CollisionShape2D");
		Label key_amount = GetParent().GetParent().GetNode<Label>("Touchcontrols/Key_amount");
		Sprite2D heart1 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart1");
		Sprite2D heart2 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart2");
		Sprite2D heart3 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart3");
		if (heart1 == null || heart2 == null || heart3 == null)
		{
			GD.Print("One or more hearts are not found in the parent node.");
			return;
		}
		GD.Print("Hearts found and reset.");
		heart1.Visible = true; // Show the first heart
		heart2.Visible = true; // Show the second heart
		heart3.Visible = true; // Show the third heart
		key_amount.Text = "0"; // Update the key amount label
		if (freezer1 == null || freezer2 == null || freezer3 == null)
		{
			GD.Print("One or more freezers are not found in the parent node.");
			return;
		}
		GD.Print("Freezers found and reset.");
		freezer1.Visible = true;
		freezer1_collisionShape.Disabled = false;
		freezer2.Visible = true;
		freezer2_collisionShape.Disabled = false;
		freezer3.Visible = true;
		freezer3_collisionShape.Disabled = false;
        timer.Wingame = false; // Reset the game state
		Sprite2D win_screen = GetParent().GetNode<Sprite2D>("wingame_screen");
		if (win_screen != null)
		{
			GD.Print("Win screen not found in the parent node.");
			win_screen.Visible = false; // Hide the win screen if it exists
		}
		else
		{
			GD.Print("Win screen not found in the parent node.");
		}
		monster1.Position = Monster1.startPosition;
		monster2.Position = Monster2.startPosition;
		monster3.Position = Monster3.startPosition;
		monster4.Position = Monster4.startPosition;
		monster5.Position = Monster5.startPosition;
		monster_boss.Position = Monster_boss.startPosition;
		character.bossfight = false; // Reset the boss fight state
		if (thecharacter == null)
		{
			GD.Print("that is not found in the parent node.");
			return;
		}
		// Reset character position to the start position
		// Add your logic here (e.g., start the game, load a new scene, etc.)
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
