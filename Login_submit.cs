using Godot;
using System;
using System.Text.Json;
using System.Collections.Generic;
public partial class Login_submit : TouchScreenButton
{
	public static string username_value = "visior";
	//private HttpRequest http;
	// Called when the node enters the scene tree for the first time.
	private Sprite2D Key;
	public override void _Ready()
	{
		LineEdit inputBox_username = GetNode<LineEdit>("username_input");
		LineEdit inputBox_password = GetNode<LineEdit>("password_input");
		inputBox_username.TextChanged += Username_OnTextChanged;
		inputBox_password.TextChanged += Password_OnTextChanged;
		Pressed += OnButtonPressed;

	}
	
	private void Username_OnTextChanged(string newText)
	{
		GD.Print("User typed: ", newText);
	}
		private void Password_OnTextChanged(string newText)
	{
		GD.Print("User typed: ", newText);
	}
	private void OnButtonPressed()
	{
		GD.Print("Button pressed, sending registration request...");
		LineEdit inputBox_username = GetNode<LineEdit>("username_input");
		LineEdit inputBox_password = GetNode<LineEdit>("password_input");
		HttpRequest httpRequest = GetNode<HttpRequest>("httpRequest");
		GD.Print("User sent: ", inputBox_username.Text);
		GD.Print("User sent: ", inputBox_password.Text);
		string url = config.domain+"/login/";
		username_value = inputBox_username.Text;
		

		// JSON payload
		string json = $"{{\"username\": \"{inputBox_username.Text}\", \"password\": \"{inputBox_password.Text}\"}}";

		// Headers
		var headers = new string[] { "Content-Type: application/json" };

		httpRequest.RequestCompleted += OnRequestCompleted;
		// POST request
		httpRequest.Request(url, headers, HttpClient.Method.Post, json); // Pass the JSON string directly
		GD.Print("testtt");
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print("Response code: ", responseCode);
		//GD.Print("Response body: ", responseBody);
		//var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
		//GD.Print("Response data: ", jsonData);
		if (responseCode == 200)
		{
			GD.Print("Login successful!");
			Key = GetParent().GetParent().GetNode<Sprite2D>("main_screen_background");
			Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("login_page");
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
			Label username = GetParent().GetParent().GetParent().GetNode<Label>("Touchcontrols/username");
			username.Text = "Welcome " + username_value;
			username.Visible = true; // Show the username label
			main_screen.Visible = false; // Show the main screen
			Key.Visible = false;
			Character.gamestart = true;
			Timer.countdownTime = 300f; // Reset the countdown timer to 5 minutes (300 seconds)
			Character.IsLogin = true; // Assuming Character is a globally accessible object or sing
			CharacterBody2D thecharacter = GetParent().GetParent().GetParent().GetNode<CharacterBody2D>("character");
			thecharacter.Position = Character.startPosition;
			// 
			// leton
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
			StaticBody2D gate = GetParent().GetParent().GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
			CollisionShape2D gate_collisionShape = GetParent().GetParent().GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");
			gate.Visible = true; // Show the gate
			gate_collisionShape.Disabled = false; // Enable the gate collision shape
			freezer_icon.Visible = false; // Show the freezer icon
			sword_icon.Visible = false; // Show the sword icon
			Character.Health = 3; // Reset character health to 3
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
			Timer.Wingame = false; // Reset the game state
			Node2D mainscreen = GetParent().GetParent().GetNode<Node2D>("main_screen");
			if (mainscreen == null)
			{
				GD.Print("Main screen node not found in the parent node.");
			}
			else
			{ 
				mainscreen.Visible = false; // Hide the main screen
			}
			Sprite2D win_screen = GetParent().GetParent().GetParent().GetNode<Sprite2D>("Touchcontrols/wingame_screen");
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

		}
		else
		{
			if (responseCode == 400)
			{
				Node2D mainscreen = GetParent().GetParent().GetNode<Node2D>("main_screen");
				if (mainscreen == null)
				{
					GD.Print("Main screen node not found in the parent node.");
				}
				mainscreen.Visible = false;
				//GD.Print("Error: invaid username or password.");
				//AcceptDialog alert = GetNode<AcceptDialog>("AlertDialog");
				//alert.Visible = true;
				//alert.DialogText = "Error: Passwords do not match.";
				//alert.PopupCentered();
			}
		}
	}
	public void RestoreCharacterTexture()
	{
		Sprite2D characterSprite = GetParent().GetParent().GetNodeOrNull<Sprite2D>("character/Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character from replay Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
   }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
