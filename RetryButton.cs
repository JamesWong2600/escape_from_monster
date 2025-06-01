using Godot;
using System;

// RetryButton class inheriting from TouchScreenButton
public partial class RetryButton : TouchScreenButton
{

	// Reference to the background sprite
	private Sprite2D Background;

	// References to the character and monsters
	private CharacterBody2D thecharacter;
	private CharacterBody2D monster1;
	private CharacterBody2D monster2;
	private CharacterBody2D monster3;
	private CharacterBody2D monster4;
	private CharacterBody2D monster5;


	// References to key sprites
	private Sprite2D key1;
	private Sprite2D key2;
	private Sprite2D key3;
	private Sprite2D key4;

	private Sprite2D key5;
	private Sprite2D key6;
	private Sprite2D key7;
	private Sprite2D key8;

	// References to key collision shapes
	private CollisionShape2D key1_collisionShape;
	private CollisionShape2D key2_collisionShape;
	private CollisionShape2D key3_collisionShape;
	private CollisionShape2D key4_collisionShape;
	private CollisionShape2D key5_collisionShape;
	private CollisionShape2D key6_collisionShape;
	private CollisionShape2D key7_collisionShape;
	private CollisionShape2D key8_collisionShape;

	// References to freezer objects and their collision shapes
	private StaticBody2D freezer1;
	private StaticBody2D freezer2;
	private StaticBody2D freezer3;
	private CollisionShape2D freezer1_collisionShape;
	private CollisionShape2D freezer2_collisionShape;
	private CollisionShape2D freezer3_collisionShape;


	// Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		Visible = false;
		Pressed += OnButtonPressed;
	}

	// Method triggered when the RetryButton is pressed
	private void OnButtonPressed()
	{
		// Reset game states
		Character.Health = 3;
		Character.gamestart = true;
		Timer.Wingame = false;
		Timer.countdownTime = 300f;
		Character.bossfight = false; 

		// Get references to UI elements
		Background = GetParent().GetNode<Sprite2D>("gameover");
		Label username = GetParent().GetNode<Label>("username");
		username.Visible = true; 
		Sprite2D wingame_screen = GetParent().GetNode<Sprite2D>("wingame_screen");
		wingame_screen.Visible = false;

		// Debug message if the background is not found
		if (Background == null)
		{
			GD.Print("Key sprite not found in the parent node.");
			return;
		}

		// Debug message and hide the background
		GD.Print("Button pressed!");
		Background.Visible = false;
		Visible = false;


		// Hide return-to-main buttons
		TouchScreenButton return_to_main_win_button = GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_win");
		return_to_main_win_button.Visible = false; 
		TouchScreenButton return_to_main_failed = GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_failed");
		return_to_main_failed.Visible = false; 

		// Reset character position
		thecharacter = GetParent().GetParent().GetNode<CharacterBody2D>("character");
		thecharacter.Position = Character.startPosition;


		// Get references to monsters		
		monster1 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-1");
		monster2 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-2");
		monster3 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-3");
		monster4 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-4");
		monster5 = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-5");


		// Get references to monster sprites
		Sprite2D monster1_sprite = GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-1/Sprite2D");
		Sprite2D monster2_sprite = GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-2/Sprite2D");
		Sprite2D monster3_sprite = GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-3/Sprite2D");
		Sprite2D monster4_sprite = GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-4/Sprite2D");
		Sprite2D monster5_sprite = GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-5/Sprite2D");
		CharacterBody2D monster_boss_sprite = GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
		CollisionShape2D monster_boss_sprite_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("Monster/monster-boss/CollisionShape2D");

		// Reset monster boss collision shape and health
		if (monster_boss_sprite_Collision == null)
		{
			GD.Print("Monster boss collision shape not found in the parent node.");
		}
		monster_boss_sprite_Collision.Disabled = false;
		ProgressBar monster_boss_health_bar = GetParent().GetParent().GetNode<ProgressBar>("Monster/monster-boss/HealthBar2");
		monster_boss_sprite.Position = Monster_boss.startPosition;
		monster_boss_sprite.Visible = true;
		monster_boss_health_bar.Value = 5;


		// Debug message if monster sprites are not found
		if (monster1_sprite == null || monster2_sprite == null || monster3_sprite == null || monster4_sprite == null || monster5_sprite == null)
		{
			GD.Print("One or more monster sprites are not found in the parent node.");
			return;
		}

		// Reset monsters' positions
		GD.Print("Monsters sprites found and reset.");
		monster1_sprite.Texture = GD.Load<Texture2D>("res://character/monster.png");
		monster2_sprite.Texture = GD.Load<Texture2D>("res://character/monster.png");
		monster3_sprite.Texture = GD.Load<Texture2D>("res://character/monster.png");
		monster4_sprite.Texture = GD.Load<Texture2D>("res://character/monster.png");
		monster5_sprite.Texture = GD.Load<Texture2D>("res://character/monster.png");
		if (monster1 == null || monster2 == null || monster3 == null || monster4 == null || monster5 == null)
		{
			GD.Print("One or more monsters are not found in the parent node.");
			return;
		}
		GD.Print("Monsters found and reset.");

		// Restore the character's texture to its default state
		RestoreCharacterTexture();


		// Get references to key sprites and their collision shapes
		key1 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-1/Sprite2D");
		key1_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
		key2 = GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
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
		key8_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");


		// Get references to the gate and its collision shape
		StaticBody2D gate = GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
		CollisionShape2D gate_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");

		// Debug message if keys are not found
		if (key1 == null)
		{
			GD.Print("One or more keys are not found in the parent node.");
			return;
		}

		// Reset visibility and collision states for keys and gate
		GD.Print("Keys found and reset.");
		key1.Visible = true;
		key1_collisionShape.Disabled = false;
		gate.Visible = true;
		gate_collisionShape.Disabled = false; 
		key2.Visible = false;
		key2_collisionShape.Disabled = true;
		key3.Visible = false;
		key3_collisionShape.Disabled = true;
		key4.Visible = false;
		key4_collisionShape.Disabled = true;
		key5.Visible = false;
		key5_collisionShape.Disabled = true;
		key6.Visible = false;
		key6_collisionShape.Disabled = true;
		key7.Visible = false;
		key7_collisionShape.Disabled = true;
		key8.Visible = false;
		key8_collisionShape.Disabled = true;

		// Get references to freezer objects and their collision shapes
		freezer1 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-1");
		freezer1_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-1/CollisionShape2D");
		freezer2 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-2");
		freezer2_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-2/CollisionShape2D");
		freezer3 = GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-3");
		freezer3_collisionShape = GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-3/CollisionShape2D");

		// Reset visibility and collision states for freezers
		freezer1.Visible = true;
		freezer1_collisionShape.Disabled = false;
		freezer2.Visible = true;
		freezer2_collisionShape.Disabled = false;
		freezer3.Visible = true;
		freezer3_collisionShape.Disabled = false;


		Label key_amount = GetParent().GetParent().GetNode<Label>("Touchcontrols/Key_amount");
		Sprite2D heart1 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart1");
		Sprite2D heart2 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart2");
		Sprite2D heart3 = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart3");
		Sprite2D freezer_icon = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/freezer_icon");
		Sprite2D sword_icon = GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/sword_icon");

		// Reset visibility for freezer and sword icons
		freezer_icon.Visible = false; 
		sword_icon.Visible = false; 
		Monster1.Monster_currentCooldown = 0f; 
		Monster2.Monster_currentCooldown = 0f; 
		Monster3.Monster_currentCooldown = 0f; 
		Monster4.Monster_currentCooldown = 0f; 
		Monster5.Monster_currentCooldown = 0f; 

		// Check if hearts are found
		if (heart1 == null || heart2 == null || heart3 == null)
		{
			GD.Print("One or more hearts are not found in the parent node.");
			return;
		}

		// Reset visibility for hearts
		GD.Print("Hearts found and reset.");
		heart1.Visible = true; 
		heart2.Visible = true; 
		heart3.Visible = true; 


		// Reset key amount label
		key_amount.Text = "0 / 8"; 

		// Reset boss health and score
		Character.Boss_health = 5f;
		Character.scoreValue = 0;

		// Check if freezers are found
		if (freezer1 == null || freezer2 == null || freezer3 == null)
		{
			GD.Print("One or more freezers are not found in the parent node.");
			return;
		}
		GD.Print("Freezers found and reset.");

	   // Reset win screen visibility
		Sprite2D win_screen = GetParent().GetNode<Sprite2D>("wingame_screen");
		Character.KeyAmount = 0; // Reset the key count
		if (win_screen != null)
		{
			GD.Print("Win screen not found in the parent node.");
			win_screen.Visible = false; // Hide the win screen if it exists
		}
		else
		{
			GD.Print("Win screen not found in the parent node.");
		}

		// Reset monster positions
		monster1.Position = Monster1.startPosition;
		monster2.Position = Monster2.startPosition;
		monster3.Position = Monster3.startPosition;
		monster4.Position = Monster4.startPosition;
		monster5.Position = Monster5.startPosition;

		// Reset character state
		Character.holdingFreezer = false;
		Character.holdingSword = false; 

		// Reset sword visibility and collision states
		StaticBody2D Sword_1 = GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-1");
		CollisionShape2D Sword_1_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-1/CollisionShape2D");
		Sword_1_Collision.Disabled = false;
		Sword_1.Visible = true;

		StaticBody2D Sword_2 = GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-2");
		CollisionShape2D Sword_2_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-2/CollisionShape2D");
		Sword_2_Collision.Disabled = false;
		Sword_2.Visible = true;

		StaticBody2D Sword_3 = GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-3");
		CollisionShape2D Sword_3_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-3/CollisionShape2D");
		Sword_3_Collision.Disabled = false;
		Sword_3.Visible = true;

		StaticBody2D Sword_4 = GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-4");
		CollisionShape2D Sword_4_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-4/CollisionShape2D");
		Sword_4_Collision.Disabled = false;
		Sword_4.Visible = true;

		StaticBody2D Sword_5 = GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-5");
		CollisionShape2D Sword_5_Collision = GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-5/CollisionShape2D");
		Sword_5_Collision.Disabled = false;
		Sword_5.Visible = true;



		AnimatedSprite2D Monster_1_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-1/AnimatedSprite2D");
		AnimatedSprite2D Monster_2_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-2/AnimatedSprite2D");
		AnimatedSprite2D Monster_3_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-3/AnimatedSprite2D");
		AnimatedSprite2D Monster_4_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-4/AnimatedSprite2D");
		AnimatedSprite2D Monster_5_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-5/AnimatedSprite2D");
		AnimatedSprite2D Monster_boss_animatedSprite2D = GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-boss/AnimatedSprite2D");
	
	
		// Check if Monster 1 animated sprite exists
		if (Monster_1_animatedSprite2D == null)
		{
			GD.Print("Monster_1_animatedSprite2D is not found in the parent node.");
			return;
		}

		
		Monster_1_animatedSprite2D.Play("run");
		Monster_2_animatedSprite2D.Play("run");
		Monster_3_animatedSprite2D.Play("run");
		Monster_4_animatedSprite2D.Play("run");
		Monster_5_animatedSprite2D.Play("run");
		Monster_boss_animatedSprite2D.Play("boss");
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.

	// Method to restore the character's texture
	public void RestoreCharacterTexture()
	{

		// Get the character's Sprite2D node
		Sprite2D characterSprite = GetParent().GetParent().GetParent().GetNodeOrNull<Sprite2D>("character/Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character from replay Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
}
