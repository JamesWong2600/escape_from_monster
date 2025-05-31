using Godot;
using System;


// Demo class inheriting from TouchScreenButton
public partial class Demo : TouchScreenButton
{

	// Reference to the key sprite
	private Sprite2D Key;

	 // Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		 // Connect the button's Pressed signal to the OnButtonPressed method
		Pressed += OnButtonPressed;
	}

	 // Method triggered when the button is pressed 
	private void OnButtonPressed()
	{
		// Start the game and reset various states
		Character.gamestart = true; 
		Character.Health = 3; 
		Character.bossfight = false; 

		// Get references to UI and gameplay elements
		Key = GetParent().GetParent().GetNode<Sprite2D>("main_screen_background"); 
		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen"); 
		Timer.Wingame = false; 
		main_screen.Visible = false;

		// Get references to monster animated sprites
		AnimatedSprite2D Monster_1_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-1/AnimatedSprite2D");
		AnimatedSprite2D Monster_2_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-2/AnimatedSprite2D");
		AnimatedSprite2D Monster_3_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-3/AnimatedSprite2D");
		AnimatedSprite2D Monster_4_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-4/AnimatedSprite2D");
		AnimatedSprite2D Monster_5_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-5/AnimatedSprite2D");
		
		 // Check if the first monster sprite exists
		if (Monster_1_animatedSprite2D == null)
		{
			GD.Print("Monster_1_animatedSprite2D not found in the parent node.");
			return;
		}

		// Debug message and start monster animations
		GD.Print("Button pressed!");
		Monster_1_animatedSprite2D.Play("run");
		Monster_2_animatedSprite2D.Play("run");
		Monster_3_animatedSprite2D.Play("run");
		Monster_4_animatedSprite2D.Play("run");
		Monster_5_animatedSprite2D.Play("run");


		// Reset game timer
		Timer.countdownTime = 300f;

		// Hide the key sprite
		Key.Visible = false;

		// Reset character position
		CharacterBody2D thecharacter = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("character");
		thecharacter.Position = Character.startPosition;


		// Update username label
		Label username = GetParent().GetParent().GetParent().GetNode<Label>("Touchcontrols/username");
		username.Text = "Welcome " + "Visitor"; // Set the username to "Visitor" for demo purposes
		username.Visible = true; // Make the username label visible

		// Hide return-to-main buttons
		TouchScreenButton return_to_main_failed = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_failed");
		return_to_main_failed.Visible = false;
		TouchScreenButton return_to_main_win = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_win");
		return_to_main_win.Visible = false;
		// Reset monsters' positions and textures
		CharacterBody2D monster1 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-1");
		CharacterBody2D monster2 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-2");
		CharacterBody2D monster3 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-3");
		CharacterBody2D monster4 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-4");
		CharacterBody2D monster5 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-5");

		// Reset monster sprites
		Sprite2D monster1_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-1/Sprite2D");
		Sprite2D monster2_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-2/Sprite2D");
		Sprite2D monster3_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-3/Sprite2D");
		Sprite2D monster4_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-4/Sprite2D");
		Sprite2D monster5_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-5/Sprite2D");
		if (monster1_sprite == null || monster2_sprite == null || monster3_sprite == null || monster4_sprite == null || monster5_sprite == null)
		{
			GD.Print("One or more monster sprites are not found in the parent node.");
			return;
		}
		
		
		// Reset monster boss sprites
		CharacterBody2D monster_boss_sprite = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
		CollisionShape2D monster_boss_sprite_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("Monster/monster-boss/CollisionShape2D");
		if (monster_boss_sprite_Collision == null)
		{
			GD.Print("Monster boss collision shape not found in the parent node.");
		}

		
		// Enable collision and reset boss health
		monster_boss_sprite_Collision.Disabled = false;
		ProgressBar monster_boss_health_bar = GetParent().GetParent().GetParent().GetNode<ProgressBar>("Monster/monster-boss/HealthBar2");
		monster_boss_sprite.Position = Monster_boss.startPosition;
		monster_boss_sprite.Visible = true; // Ensure the boss monster is visible
		monster_boss_health_bar.Value = 5;

	   // Reset monster textures
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
		Sprite2D key1 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-1/Sprite2D");
		CollisionShape2D key1_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
		Sprite2D key2 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
		CollisionShape2D key2_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
		Sprite2D key3 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-3/Sprite2D");
		CollisionShape2D key3_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
		Sprite2D key4 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-4/Sprite2D");
		CollisionShape2D key4_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
		Sprite2D key5 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-5/Sprite2D");
		CollisionShape2D key5_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
		Sprite2D key6 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-6/Sprite2D");
		CollisionShape2D key6_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
		Sprite2D key7 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-7/Sprite2D");
		CollisionShape2D key7_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
		Sprite2D key8 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
		CollisionShape2D key8_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");

		// Get references to the gate and its collision shape
		StaticBody2D gate = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
		CollisionShape2D gate_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");

		// Check if all keys are found
		if (key1 == null)
		{
			GD.Print("One or more keys are not found in the parent node.");
			return;
		}
		GD.Print("Keys found and reset.");

		// Reset visibility and collision states for keys and gate
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
		StaticBody2D freezer1 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-1");
		CollisionShape2D freezer1_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-1/CollisionShape2D");
		StaticBody2D freezer2 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-2");
		CollisionShape2D freezer2_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-2/CollisionShape2D");
		StaticBody2D freezer3 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-3");
		CollisionShape2D freezer3_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-3/CollisionShape2D");

		// Get references to UI elements
		Label key_amount = GetParent().GetParent().GetParent().GetNode<Label>("Touchcontrols/Key_amount");
		Sprite2D heart1 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart1");
		Sprite2D heart2 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart2");
		Sprite2D heart3 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart3");
		Sprite2D freezer_icon = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/freezer_icon");
		Sprite2D sword_icon = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/sword_icon");


		// Reset visibility for freezer and sword icons
		freezer_icon.Visible = false;
		sword_icon.Visible = false; 


		// Reset monster cooldowns
		Monster1.Monster_currentCooldown = 0f; 
		Monster2.Monster_currentCooldown = 0f; 
		Monster3.Monster_currentCooldown = 0f; 
		Monster4.Monster_currentCooldown = 0f; 
		Monster5.Monster_currentCooldown = 0f; 

		// Hide return-to-main win button
		TouchScreenButton return_to_main_win_button = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_win");
		return_to_main_win_button.Visible = false; 

		// Check if hearts are found
		if (heart1 == null || heart2 == null || heart3 == null)
		{
			GD.Print("One or more hearts are not found in the parent node.");
			return;
		}
		GD.Print("Hearts found and reset.");

		// Reset visibility for hearts
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

		// Reset visibility and collision states for freezers
		freezer1.Visible = true;
		freezer1_collisionShape.Disabled = false;
		freezer2.Visible = true;
		freezer2_collisionShape.Disabled = false;
		freezer3.Visible = true;
		freezer3_collisionShape.Disabled = false;

	   // Reset win screen visibility
		Sprite2D win_screen = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/wingame_screen");
		Character.KeyAmount = 0; // Reset the key count
		if (win_screen != null)
		{
			GD.Print("Win screen founded in the parent node.");
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
		StaticBody2D Sword_1 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-1");
		CollisionShape2D Sword_1_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-1/CollisionShape2D");
		Sword_1_Collision.Disabled = false;
		Sword_1.Visible = true;

		StaticBody2D Sword_2 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-2");
		CollisionShape2D Sword_2_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-2/CollisionShape2D");
		Sword_2_Collision.Disabled = false;
		Sword_2.Visible = true;

		StaticBody2D Sword_3 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-3");
		CollisionShape2D Sword_3_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-3/CollisionShape2D");
		Sword_3_Collision.Disabled = false;
		Sword_3.Visible = true;

		StaticBody2D Sword_4 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-4");
		CollisionShape2D Sword_4_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-4/CollisionShape2D");
		Sword_4_Collision.Disabled = false;
		Sword_4.Visible = true;

		StaticBody2D Sword_5 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-5");
		CollisionShape2D Sword_5_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-5/CollisionShape2D");
		Sword_5_Collision.Disabled = false;
		Sword_5.Visible = true;

		// Play boss animation
		AnimatedSprite2D Monster_boss_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-boss/AnimatedSprite2D");
		if (Monster_1_animatedSprite2D == null)
		{
			GD.Print("Monster_1_animatedSprite2D is not found in the parent node.");
			return;
		}
		Monster_boss_animatedSprite2D.Play("boss");

		// Ensure username label is visible
		username.Visible = true; 
	}

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
