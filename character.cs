using Godot;
using System;

public partial class Character : CharacterBody2D
{
	public static Vector2 startPosition = new Vector2(-5, 210);
	private Vector2 targetPosition = new Vector2(1000, 0);
	private Vector2 Boss_fight_recover_Position = new Vector2(5000f, 5000f);
	private Vector2 Level2Position = new Vector2(4500, 3892);
	public float session = 0;
	public float BgWidth = 5680f;   // Set to your background width
									//5720 
	public float BgHeight = 3180f;  // Set to your background height
	public Vector2 CharacterSize = new Vector2(32, 32);
	private float moveDuration = 2.0f; // seconds
	private float elapsedTime = 0.0f;
	private bool moving = true;
	public float Speed = 600f;
	private Node touchControlsInstance;
	private Sprite2D heart1;
	private Sprite2D heart2;
	private Sprite2D heart3;
	private Sprite2D gameover;
	private float cooldownTime = 1f; // seconds
	private float delayTime = 1f; // seconds
	private float currentdelay = 0f;
	private float currentCooldown = 0f;
	private Sprite2D Key;
	private Sprite2D Next_Key;
	private StaticBody2D Sword;
	private StaticBody2D TheGate;
	private StaticBody2D Freezer;
	private CharacterBody2D The_Boss;
	private Sprite2D A_MONSTER;
	private CollisionShape2D Key_Collision;
	private CollisionShape2D Next_Key_Collision;
	private CollisionShape2D Sword_Collision;

	private CollisionShape2D Freezer_Collision;
	public static float Boss_health = 5f;
	private ProgressBar The_Boss_health;
	public static bool holdingSword = false; // Flag to check if the character is holding a sword
	public static bool holdingFreezer = false;
	public static bool gamestart = false;

	private bool socre_released = false; // Flag to check if score has been released
	private TouchScreenButton RetryButton;
	public static float KeyAmount = 0;
	public static float scoreValue = 0;
	public static bool bossfight = false;
	private Label Key_amount_label; // Label to display key amount
	public static int Health = 3; // Example health variable
	public override void _Ready()
	{
		GD.Print("Hello, Godot!");
		Position = startPosition;
		elapsedTime = 0.0f;
		moving = true;
		var cam = GetNode<Camera2D>("Camera2D");
		cam.MakeCurrent();
		GD.Print("Camera is now current.");
		//AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("../Key_SubViewport/Node3D/AnimationPlayer");
		//animationPlayer.Play("new_animation"); 
		heart1 = GetNode<Sprite2D>("../Touchcontrols/heart1");
		heart2 = GetNode<Sprite2D>("../Touchcontrols/heart2");
		heart3 = GetNode<Sprite2D>("../Touchcontrols/heart3");
		gameover = GetNode<Sprite2D>("../Touchcontrols/gameover");


		//var abcScene = GD.Load<PackedScene>("res://touchcontrols.tscn");
		//var abcInstance = abcScene.Instantiate();
		//GetTree().Root.AddChild(abcInstance);

		/* heart = abcInstance.FindChild("heart1", true, false) as Sprite2D;
		if (heart != null)
		{
			heart.Position = new Vector2(100, -100); // Set initial position of the heart sprite
			GD.Print(heart.Position);
		}
		else
		{
			GD.Print("Heart sprite not found!");
		
		}*/

	}




	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 input = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up")
		);


		//if (input.Length() > 0)
		//{
		//	input = input.Normalized();
		//Position += input * Speed * (float)delta;
		//	Vector2 newPosition = Position + input * Speed * (float)delta;

		//	newPosition.X = Mathf.Clamp(newPosition.X, 0, BgWidth - CharacterSize.X);
		//	newPosition.Y = Mathf.Clamp(newPosition.Y, 0, BgHeight - CharacterSize.Y);
		//
		//		Position = newPosition;
		//	}
	}
	public override void _PhysicsProcess(double delta)
	{

		if (currentCooldown > 0)
			currentCooldown -= (float)delta;

		Vector2 direction = Vector2.Zero;

		if (gamestart == false)
		{
			return;
		}
		else if (gamestart == true)
		{
			if (Input.IsActionPressed("right"))
				direction.X += 1;
			if (Input.IsActionPressed("left"))
				direction.X -= 1;
			if (Input.IsActionPressed("down"))
				direction.Y += 1;
			if (Input.IsActionPressed("up"))
				direction.Y -= 1;

			direction = direction.Normalized();
			Velocity = direction * Speed;

			MoveAndSlide();  // Moves and checks collisions internally
		}
		int collisionCount = GetSlideCollisionCount();
		for (int i = 0; i < collisionCount; i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node collider = collision.GetCollider() as Node;


			if (collider is Monster1) // Replace 'Monster' with the actual class name of your monster
			{
				GD.Print("Collided with a monster!");
				GD.Print("Current freezer status: ", holdingFreezer);
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-1/Sprite2D");
				//Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
				//GD.Print("Monster texture: ", A_MONSTER.Texture.ResourcePath);
				//Monster1 monster = collider as Monster1; // Cast to your monster class
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						//Monster1.IsFreeze = true;
						GD.Print("You froze the monster!");
						//RestoreCharacterTexture();
						currentCooldown = cooldownTime;
						return;
					}
					else if (holdingFreezer == false)
					{
						if (A_MONSTER.Texture.ResourcePath == "res://character/monster.png")
						{
							GD.Print("YES! " + A_MONSTER.Texture.ResourcePath);
							GD.Print("You need a freezer to freeze the monster!");
							Position = startPosition;
							SetHeartInvisible();
							currentCooldown = cooldownTime;
							return;
						}
					}
				}
				//OnMonsterCollision(collider as Monster1);
			}
			if (collider is Monster2) // Replace 'Monster' with the actual class name of your monster
			{
				//GD.Print("Collided with a monster!");
				//GD.Print("Current freezer status: ", holdingFreezer);
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-2/Sprite2D");
				///Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
				//GD.Print("Monster texture: ", A_MONSTER.Texture.ResourcePath);
				//Monster1 monster = collider as Monster1; // Cast to your monster class
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						//Monster2.IsFreeze = true;
						GD.Print("You froze the monster!");
						//RestoreCharacterTexture();
						//holdingFreezer = false;
						currentCooldown = cooldownTime;
						return;
					}
					else if (holdingFreezer == false)
					{
						if (A_MONSTER.Texture.ResourcePath == "res://character/monster.png")
						{
							GD.Print("YES! " + A_MONSTER.Texture.ResourcePath);
							GD.Print("You need a freezer to freeze the monster!");
							Position = startPosition;
							SetHeartInvisible();
							currentCooldown = cooldownTime;
							return;
						}
					}
				}
				//OnMonsterCollision(collider as Monster1);
			}
			if (collider is Monster3) // Replace 'Monster' with the actual class name of your monster
			{
				//GD.Print("Collided with a monster!");
				//GD.Print("Current freezer status: ", holdingFreezer);
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-3/Sprite2D");
				//Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
				//GD.Print("Monster texture: ", A_MONSTER.Texture.ResourcePath);
				//Monster1 monster = collider as Monster1; // Cast to your monster class
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						//Monster3.IsFreeze = true;
						GD.Print("You froze the monster!");
						//RestoreCharacterTexture();
						//holdingFreezer = false;
						currentCooldown = cooldownTime;
						return;
					}
					else if (holdingFreezer == false)
					{
						if (A_MONSTER.Texture.ResourcePath == "res://character/monster.png")
						{
							GD.Print("YES! " + A_MONSTER.Texture.ResourcePath);
							GD.Print("You need a freezer to freeze the monster!");
							Position = startPosition;
							SetHeartInvisible();
							currentCooldown = cooldownTime;
							return;
						}
					}
				}
				//OnMonsterCollision(collider as Monster1);
			}
			if (collider is Monster4) // Replace 'Monster' with the actual class name of your monster
			{
				//GD.Print("Collided with a monster!");
				//GD.Print("Current freezer status: ", holdingFreezer);
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-4/Sprite2D");
				//Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
				//GD.Print("Monster texture: ", A_MONSTER.Texture.ResourcePath);
				//Monster1 monster = collider as Monster1; // Cast to your monster class
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						//Monster3.IsFreeze = true;
						GD.Print("You froze the monster!");
						//RestoreCharacterTexture();
						//holdingFreezer = false;
						currentCooldown = cooldownTime;
						return;
					}
					else if (holdingFreezer == false)
					{
						if (A_MONSTER.Texture.ResourcePath == "res://character/monster.png")
						{
							GD.Print("YES! " + A_MONSTER.Texture.ResourcePath);
							GD.Print("You need a freezer to freeze the monster!");
							Position = startPosition;
							SetHeartInvisible();
							currentCooldown = cooldownTime;
							return;
						}
					}
				}
				//OnMonsterCollision(collider as Monster1);
			}
			if (collider is Monster5) // Replace 'Monster' with the actual class name of your monster
			{
				//GD.Print("Collided with a monster!");
				//GD.Print("Current freezer status: ", holdingFreezer);
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-5/Sprite2D");
				//Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
				//GD.Print("Monster texture: ", A_MONSTER.Texture.ResourcePath);
				//Monster1 monster = collider as Monster1; // Cast to your monster class
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						//Monster3.IsFreeze = true;
						GD.Print("You froze the monster!");
						//RestoreCharacterTexture();
						//holdingFreezer = false;
						currentCooldown = cooldownTime;
						return;
					}
					else if (holdingFreezer == false)
					{
						if (A_MONSTER.Texture.ResourcePath == "res://character/monster.png")
						{
							GD.Print("YES! " + A_MONSTER.Texture.ResourcePath);
							GD.Print("You need a freezer to freeze the monster!");
							Position = startPosition;
							SetHeartInvisible();
							currentCooldown = cooldownTime;
							return;
						}
					}
				}
				//OnMonsterCollision(collider as Monster1);
			}
			if (collider is Key1) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key111");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-1/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
					Key_Collision.Disabled = true;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
					
					if (Next_Key == null)
					{
						GD.Print("Next_Key is null, check the node path.");
					}
					else
					{
						GD.Print("Next_Key found: ", Next_Key.Name);
					}
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key.Visible = false;
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key2) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 222");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-2/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
					Key_Collision.Disabled = true;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-3/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					Key.Visible = false;
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key3) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 333");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-3/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-4/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key4) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 444");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-4/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-5/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key5) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 555");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-5/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-6/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key6) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 666 ");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-6/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-7/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key7) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 777 ");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-7/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					Next_Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
					Next_Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");
					Next_Key.Visible = true;
					Next_Key_Collision.Disabled = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Key8) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 888");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					KeyAmount += 1; // Increment the key amount
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString(); // Update the label with the key amount
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Gate) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Gate");
					TheGate = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate"); // Disable the collision shape of the gate
					if (KeyAmount < 8)
					{
						GD.Print("You need 8 keys to open the gate!");
						return; // Exit if not enough keys
					}
					CollisionShape2D TheGate_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");
					TheGate_Collision.Disabled = true;
					//TheGate.Visible = false;
					//Position = Level2Position; // Move the character to the target position
					GD.Print("Key Amount: ", KeyAmount);
					Timer.Wingame = true; // Set the timer to true to start the win game timer
					Sprite2D wingamescreen = GetNode<Sprite2D>("../Touchcontrols/wingame_screen");
					Label time_spent = GetNode<Label>("../Touchcontrols/wingame_screen/time_spent");
					Label score = GetNode<Label>("../Touchcontrols/wingame_screen/score");
					Label bossfight_label = GetNode<Label>("../Touchcontrols/wingame_screen/bossfight");
					if (wingamescreen == null)
					{
						GD.Print("wingamescreen is null, check the node path.");
					}
					else
					{
						GD.Print("wingamescreen found: ", wingamescreen.Name);
					}
					float timeremaining = Timer.countdownTime;
					GD.Print("test 53"+ timeremaining);
					float spentTime = 300f - timeremaining;
					String spentTimeText = "Time spent: " + spentTime.ToString("F1") + " s";
					time_spent.Text = spentTimeText;// Display the time spent
					GD.Print("test 56");
					gamestart = false; // Set gamestart to false to stop the game
					if (bossfight)
					{
						bossfight_label.Text = "Boss Fight: Yes"; // Display boss fight status
					}
					else
					{
						bossfight_label.Text = "Boss Fight: No"; // Display boss fight status
					}
					scoreValue = scoreValue + Timer.countdownTime * 10;
					String scoreText = "Score: " + scoreValue.ToString("F1");
					score.Text = scoreText;// Display the score based on key amount
					GD.Print("test 59");
					wingamescreen.Visible = true; // Show the win game screen
					TouchScreenButton Button = GetNode<TouchScreenButton>("../Touchcontrols/Button");
					Button.Visible = true; // Show the retry button
					//Button.Disabled = false; // Enable the retry button
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Freezer1) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a freezer!");

					}
					else
					{
						GD.Print("Collided with a freezer");
						Freezer = GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-1");
						Freezer_Collision = GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-1/CollisionShape2D");
						Freezer_Collision.Disabled = true;
						Freezer.Visible = false;
						holdingFreezer = true;
						Sprite2D Freezer_icon = GetNode<Sprite2D>("../Touchcontrols/freezer_icon");
						Freezer_icon.Visible = true; // Show the freezer icon
						GD.Print("You are now holding a freezer!");
						//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
						{
							currentCooldown = cooldownTime;
						}
					// Hide the key sprite
				}
			}
			if (collider is Freezer2) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a freezer!");
					}
					else
					{
						GD.Print("Collided with a freezer");
						Freezer = GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-2");
						Freezer_Collision = GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-2/CollisionShape2D");
						Freezer_Collision.Disabled = true;
						Freezer.Visible = false;
						Sprite2D Freezer_icon = GetNode<Sprite2D>("../Touchcontrols/freezer_icon");
						Freezer_icon.Visible = true;
						holdingFreezer = true;
						GD.Print("You are now holding a freezer!");
						//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
						{
							currentCooldown = cooldownTime;
						}
					// Hide the key sprite
				}
			}
			if (collider is Freezer3) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a freezer!");
					}
					else
					{
						GD.Print("Collided with a freezer");
						Freezer = GetParent().GetNode<StaticBody2D>("freezerset/StaticBody2D-freezer-3");
						Freezer_Collision = GetParent().GetNode<CollisionShape2D>("freezerset/StaticBody2D-freezer-3/CollisionShape2D");
						Freezer_Collision.Disabled = true;
						Freezer.Visible = false;
						holdingFreezer = true;
						Sprite2D Freezer_icon = GetNode<Sprite2D>("../Touchcontrols/freezer_icon");
						Freezer_icon.Visible = true;
						GD.Print("You are now holding a freezer!");
						//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Sword1) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; // Exit if already holding a sword
					}
					GD.Print("Collided with a sword");
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true; // Show the freezer icon
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-1");
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-1/CollisionShape2D");
					Sword_Collision.Disabled = true;//
					Sword.Visible = false;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Sword2) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; // Exit if already holding a sword
					}
					GD.Print("Collided with a sword");
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true;
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-2");
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-2/CollisionShape2D");
					Sword_Collision.Disabled = true;
					Sword.Visible = false;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Sword3) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; // Exit if already holding a sword
					}
					GD.Print("Collided with a sword");
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-3");
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true;
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-3/CollisionShape2D");
					Sword_Collision.Disabled = true;
					Sword.Visible = false;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Sword4) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; // Exit if already holding a sword
					}
					GD.Print("Collided with a sword");
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-4");
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true;
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-4/CollisionShape2D");
					Sword_Collision.Disabled = true;
					Sword.Visible = false;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Sword5) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; // Exit if already holding a sword
					}
					GD.Print("Collided with a sword");
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-5");
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-5/CollisionShape2D");
					Sword_Collision.Disabled = true;
					Sword.Visible = false;
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					//newTexture = (Texture2D)GD.Load("res://others/Squirrel_sword.png");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
			if (collider is Monster_boss) // Replace 'Monster' with the actual class name of your monster
			{
				HandleMonsterBossCollision();
				/*GD.Print("Collided with a monster!");

				if (currentCooldown <= 0f)
				{
					Boss_health = Boss_health - 1f;
					if (holdingSword)
					{
						GD.Print("You attacked the boss!");
						GD.Print("Boss health: ", Boss_health);
						The_Boss_health = GetParent().GetNode<ProgressBar>("Monster/monster-boss/HealthBar");
						The_Boss_health.Value = Boss_health; // Update the health bar value
						GD.Print("Boss health: ", Boss_health);
						holdingSword = false;
						RestoreCharacterTexture();
						if (Boss_health == 0f)
						{
							GD.Print("You defeated the boss!");
							The_Boss = GetParent().GetNode<StaticBody2D>("Monster/monster-boss");
							The_Boss.Visible = false;
							
							//Position = startPosition; // Move the character back to start position
							//Boss_health = 5f; // Reset boss health for next fight
						}
					}
					else
					{
						SetHeartInvisible();
						Position = Boss_fight_recover_Position;
						GD.Print("You need a sword to attack the boss!");
					}
					currentCooldown = cooldownTime;
				}
			}*/

			}
		}
	}
	private void HandleMonsterBossCollision()
	{
	GD.Print("Collided with a monster!");
	GD.Print("Current status: ", holdingSword);
	if (currentCooldown > 0)
		{
			GD.Print("Cooldown active, skipping collision logic.");
			return;
		}

	Boss_health -= 1f;
	if (holdingSword == true)
	{
		GD.Print("You attacked the boss!");
		GD.Print("Boss health: ", Boss_health);
		The_Boss_health = GetParent().GetNode<ProgressBar>("Monster/monster-boss/HealthBar2");
		Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
		Sword_icon.Visible = false;
		The_Boss_health.Value = Boss_health; // Update the health bar value
		GD.Print("Boss health: ", Boss_health);
		holdingSword = false;
		RestoreCharacterTexture();
		if (Boss_health == 0f)
		{
			bossfight = true;
			scoreValue += 500;
			GD.Print("You defeated the boss!");
			The_Boss = GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
			CollisionShape2D The_Boss_Collision = GetParent().GetNode<CollisionShape2D>("Monster/monster-boss/CollisionShape2D");
			if (The_Boss_Collision == null)
			{
				GD.Print("The_Boss_Collision is null, check the node path.");
			}
			The_Boss_Collision.Disabled = true; // Disable the collision shape of the boss
			The_Boss.Visible = false;
		}
	}
	else if (holdingSword == false)
	{

		SetHeartInvisible();
		Position = startPosition;
		GD.Print("You need a sword to attack the boss!");
	}

	currentCooldown = cooldownTime; // Reset the cooldown
	}
	private void Freezersquarrel()
	{
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/ice_squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
	private void RestoreFreezersquarrel()
	{
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
	private void ChangeCharacterTexture()
	{
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/Squirrel_sword.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
   	public void RestoreCharacterTexture()
   {
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
   }

	private void OnMonsterCollision(Monster1 monster)
	{
		// Example: Reduce health, restart level, or trigger game over
		GD.Print("Handling collision with monster: ", monster.Name);

		// Add your custom logic here
		// For example:
		// Health -= 10;
		// if (Health <= 0)
		// {
		//     GD.Print("Game Over!");
		//     GetTree().ReloadCurrentScene();
		// }
	}
	private void SetHeartInvisible()
	{
		Health = Health - 1;
		if (Health == 2)
		{
			heart1.Visible = false; // Ensure the sprite is visible
			GD.Print("Heart1 is now invisible: ", heart1.Visible);
			return; // Exit the method after hiding the first heart
		}
		if (Health == 1)
		{

			heart2.Visible = false; // Ensure the sprite is visible
			GD.Print("Heart2 is now invisible: ", heart2.Visible);
			return;
		}
		if (Health == 0)
		{
			heart3.Visible = false; // Ensure the sprite is visible
			GD.Print("Heart3 is now invisible: ", heart3.Visible);
			gameover.Visible = true; // Show the game over sprite
			RetryButton = GetParent().GetNode<TouchScreenButton>("Touchcontrols/Button");
			RetryButton.Visible = true; // Show the retry button
			GD.Print("Game Over! You have no hearts left.");
			return;

		}

	}
		
		
}
