using Godot;
using System;

public partial class Demo : TouchScreenButton
{

	private Sprite2D Key;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}
	private void OnButtonPressed()
	{
		Character.gamestart = true;
		Character.Health = 3; // Reset character health to 3
		Key = GetParent().GetParent().GetNode<Sprite2D>("main_screen_background");
		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		Timer.Wingame = false; // Reset the game state
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
		Timer.countdownTime = 300f;
		Key.Visible = false; // Assuming character is a globally accessible object or singleton
		CharacterBody2D thecharacter = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("character");
		thecharacter.Position = Character.startPosition;
		Label username = GetParent().GetParent().GetParent().GetNode<Label>("Touchcontrols/username");
		username.Text = "Welcome " + "Visitor"; // Set the username to "Visitor" for demo purposes
		username.Visible = true; // Hide the username label

		TouchScreenButton return_to_main_failed = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_failed");
		return_to_main_failed.Visible = false; // Hide the return to main failed button
		TouchScreenButton return_to_main_win = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_win");
		return_to_main_win.Visible = false; 

		CharacterBody2D monster1 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-1");
		CharacterBody2D monster2 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-2");
		CharacterBody2D monster3 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-3");
		CharacterBody2D monster4 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-4");
		CharacterBody2D monster5 = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-5");
		Sprite2D monster1_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-1/Sprite2D");
		Sprite2D monster2_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-2/Sprite2D");
		Sprite2D monster3_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-3/Sprite2D");
		Sprite2D monster4_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-4/Sprite2D");
		Sprite2D monster5_sprite = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Monster/monster-5/Sprite2D");
		CharacterBody2D monster_boss_sprite = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
		CollisionShape2D monster_boss_sprite_Collision = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("Monster/monster-boss/CollisionShape2D");
		if (monster_boss_sprite_Collision == null)
		{
			GD.Print("Monster boss collision shape not found in the parent node.");
		}
		monster_boss_sprite_Collision.Disabled = false;
		ProgressBar monster_boss_health_bar = GetParent().GetParent().GetParent().GetNode<ProgressBar>("Monster/monster-boss/HealthBar2");
		monster_boss_sprite.Position = Monster_boss.startPosition;
		monster_boss_sprite.Visible = true; // Ensure the boss monster is visible
		monster_boss_health_bar.Value = 5;
		if (monster1_sprite == null || monster2_sprite == null || monster3_sprite == null || monster4_sprite == null || monster5_sprite == null)
		{
			GD.Print("One or more monster sprites are not found in the parent node.");
			return;
		}
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
		RestoreCharacterTexture();
		Sprite2D key1 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-1/Sprite2D");
		CollisionShape2D key1_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
		Sprite2D key2 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
		CollisionShape2D key2_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
		Sprite2D key3 = GetParent(). GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-3/Sprite2D");
		CollisionShape2D key3_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
		Sprite2D key4 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-4/Sprite2D");
		CollisionShape2D key4_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
		Sprite2D key5 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-5/Sprite2D");
		CollisionShape2D key5_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
		Sprite2D key6 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-6/Sprite2D");
		CollisionShape2D key6_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
		Sprite2D key7 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-7/Sprite2D");
		CollisionShape2D key7_collisionShape =  GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
		Sprite2D key8 =  GetParent().GetParent().GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
		CollisionShape2D key8_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");
		StaticBody2D gate = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
		CollisionShape2D gate_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");
		if (key1 == null)
		{
			GD.Print("One or more keys are not found in the parent node.");
			return;
		}
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
		StaticBody2D freezer1 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-1");
		CollisionShape2D freezer1_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-1/CollisionShape2D");
		StaticBody2D freezer2 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-2");
		CollisionShape2D freezer2_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-2/CollisionShape2D");
		StaticBody2D freezer3 = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-3");
		CollisionShape2D freezer3_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-3/CollisionShape2D");

		Label key_amount = GetParent().GetParent().GetParent().GetNode<Label>("Touchcontrols/Key_amount");
		Sprite2D heart1 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart1");
		Sprite2D heart2 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart2");
		Sprite2D heart3 = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/heart3");
		Sprite2D freezer_icon = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/freezer_icon");
		Sprite2D sword_icon = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/sword_icon");
		//StaticBody2D gate = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
		//CollisionShape2D gate_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");
		//gate.Visible = true; // Show the gate
		//gate_collisionShape.Disabled = false; // Enable the gate collision shape
		freezer_icon.Visible = false; // Show the freezer icon
		sword_icon.Visible = false; // Show the sword icon
		Monster1.Monster_currentCooldown = 0f; // Reset monster cooldown
		Monster2.Monster_currentCooldown = 0f; // Reset monster cooldown
		Monster3.Monster_currentCooldown = 0f; // Reset monster cooldown
		Monster4.Monster_currentCooldown = 0f; // Reset monster cooldown
		Monster5.Monster_currentCooldown = 0f; // Reset monster cooldown
		TouchScreenButton return_to_main_win_button = GetParent().GetParent().GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_win");
		return_to_main_win_button.Visible = false; // Hide the return to main win button
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
		Character.Boss_health = 5f;
		Character.scoreValue = 0; // Reset the score value
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
		
		//return_to_main_failed
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
		monster1.Position = Monster1.startPosition;
		monster2.Position = Monster2.startPosition;
		monster3.Position = Monster3.startPosition;
		monster4.Position = Monster4.startPosition;
		monster5.Position = Monster5.startPosition;
		Character.holdingFreezer = false;
		Character.holdingSword = false; // Reset the holding freezer state
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
		AnimatedSprite2D Monster_boss_animatedSprite2D = GetParent().GetParent().GetParent().GetNode<AnimatedSprite2D>("Monster/monster-boss/AnimatedSprite2D");
		if (Monster_1_animatedSprite2D == null)
		{
			GD.Print("Monster_1_animatedSprite2D is not found in the parent node.");
			return;
		}
		Monster_boss_animatedSprite2D.Play("boss");
		username.Visible = true; // Hide the username label




		// Add your logic here (e.g., start the game, load a new scene, etc.)
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void RestoreCharacterTexture()
	{
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
