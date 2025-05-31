using Godot;
using System;

// Main character class inheriting from CharacterBody2D
public partial class Character : CharacterBody2D
{

	// Static variables for global states
	public static bool IsLogin = false; // Tracks if the user is logged in
	public static Vector2 startPosition = new Vector2(800, 210); // Starting position of the character

	// Positions for various gameplay elements
	private Vector2 targetPosition = new Vector2(1000, 0); // Target position for movement
	private Vector2 Boss_fight_recover_Position = new Vector2(5000f, 5000f); // Recovery position after boss fight
	private Vector2 Level2Position = new Vector2(4500, 3892); // Position for level 2

	// Gameplay variables
	public float session = 0; // Tracks the session duration
	public float BgWidth = 5680f; // Background width
	public float BgHeight = 3180f; // Background height
	public Vector2 CharacterSize = new Vector2(32, 32); // Size of the character sprite
	private float moveDuration = 2.0f; // Duration for movement
	private float elapsedTime = 0.0f; // Tracks elapsed time
	private bool moving = true; // Flag for movement state
	public float Speed = 600f; // Movement speed

	// Node references for UI and gameplay elements
	private Node touchControlsInstance; // Instance of touch controls
	private Sprite2D heart1; // Heart sprite 1
	private Sprite2D heart2; // Heart sprite 2
	private Sprite2D heart3; // Heart sprite 3
	private Sprite2D gameover; // Game over sprite

	// Cooldown and delay variables
	private float cooldownTime = 1f; // Cooldown time for actions
	private float delayTime = 1f; // Delay time for actions
	private float currentdelay = 0f; // Tracks current delay
	private float currentCooldown = 0f; // Tracks current cooldown

	// References for gameplay objects
	private Sprite2D Key; // Current key sprite
	private Sprite2D Next_Key; // Next key sprite
	private StaticBody2D Sword; // Sword object
	private StaticBody2D TheGate; // Gate object
	private StaticBody2D Freezer; // Freezer object
	private CharacterBody2D The_Boss; // Boss object
	private Sprite2D A_MONSTER; // Monster sprite

	// Collision shapes for gameplay objects
	private CollisionShape2D Key_Collision; // Collision shape for current key
	private CollisionShape2D Next_Key_Collision; // Collision shape for next key
	private CollisionShape2D Sword_Collision; // Collision shape for sword
	private CollisionShape2D Freezer_Collision; // Collision shape for freezer

	// Boss health and progress bar
	public static float Boss_health = 5f; // Boss health
	private ProgressBar The_Boss_health; // Progress bar for boss health

	// Flags for gameplay states
	public static bool holdingSword = false; // Tracks if the character is holding a sword
	public static bool holdingFreezer = false; // Tracks if the character is holding a freezer
	public static bool gamestart = false; // Tracks if the game has started

	// Score and key tracking
	private bool socre_released = false; // Tracks if the score has been released
	private TouchScreenButton RetryButton; // Retry button reference
	public static float KeyAmount = 0; // Tracks the number of keys collected
	public static float scoreValue = 0; // Tracks the score value
	public static bool bossfight = false; // Tracks if a boss fight is active
	private Label Key_amount_label; // Label to display key amount
	public static int Health = 3; // Tracks the character's health
	private AnimatedSprite2D animatedSprite2D; // Animated sprite for the character


	// Called when the node is added to the scene
	public override void _Ready()
	{
		GD.Print("Hello, Godot!"); // Debug message
		Position = startPosition; // Set the initial position
		elapsedTime = 0.0f; // Reset elapsed time
		moving = true; // Enable movement
		

		// Set the camera as the current camera
		var cam = GetNode<Camera2D>("Camera2D");
		cam.MakeCurrent();
		GD.Print("Camera is now current."); // Debug message


		// Initialize heart sprites
		heart1 = GetNode<Sprite2D>("../Touchcontrols/heart1");
		heart2 = GetNode<Sprite2D>("../Touchcontrols/heart2");
		heart3 = GetNode<Sprite2D>("../Touchcontrols/heart3");

		 // Initialize animated sprite
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	}




	// Called every frame
	public override void _Process(double delta)
	{
		// Handle input for movement
		Vector2 input = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up")
		);
	}

	// Called every physics frame
	public override void _PhysicsProcess(double delta)
	{

		// Reduce cooldown over time
		if (currentCooldown > 0)
			currentCooldown -= (float)delta;

		Vector2 direction = Vector2.Zero; // Initialize movement direction

		// If the game hasn't started, exit early
		if (gamestart == false)
		{
			return;
		}
		
		// Handle movement input
		if (gamestart == true)
		{
			if (Input.IsActionPressed("right"))
				direction.X += 1;
			if (Input.IsActionPressed("left"))
				direction.X -= 1;
			if (Input.IsActionPressed("down"))
				direction.Y += 1;
			if (Input.IsActionPressed("up"))
				direction.Y -= 1;

			// Normalize direction and set velocity
			direction = direction.Normalized();
			Velocity = direction * Speed;

			// Play animations based on movement
			if (Velocity.Length() > 0f)
			{
				animatedSprite2D.Play("run");
			}
			else
			{
				animatedSprite2D.Play("idie");
			}

			// Flip sprite based on direction
			if (Velocity.X != 0)
			{
				animatedSprite2D.FlipH = Velocity.X > 0;
			}

			// Move the character and handle collisions
			MoveAndSlide();
		}

		// Handle collisions
		int collisionCount = GetSlideCollisionCount();
		for (int i = 0; i < collisionCount; i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node collider = collision.GetCollider() as Node;

			
			// Collision handling logic for various objects (e.g., monsters, keys, gate, freezer, sword)
			if (collider is Monster1) 
			{
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-1/Sprite2D");
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						GD.Print("You froze the monster 1!");
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
			}
			if (collider is Monster2)
			{
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-2/Sprite2D");
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						GD.Print("You froze the monster 2!");
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
			}
			if (collider is Monster3)
			{
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-3/Sprite2D");
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						GD.Print("You froze the monster 3!");
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
			}
			if (collider is Monster4)
			{
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-4/Sprite2D");
				if (currentCooldown <= 0f)
				{
					GD.Print("You touch the monster 4!");
					if (holdingFreezer == true)
					{
						GD.Print("You froze the monster 4!");
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
			}
			if (collider is Monster5)
			{
				GD.Print("You touch the monster 5!");;
				A_MONSTER = GetParent().GetNode<Sprite2D>("Monster/monster-5/Sprite2D");
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer == true)
					{
						GD.Print("You froze the monster 5!");
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
			}
			if (collider is Key1)
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
					Key_amount_label.Text = KeyAmount.ToString() + " / 8";
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key2)
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
					Key_amount_label.Text = KeyAmount.ToString() + " / 8";
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}			
				}
			}
			if (collider is Key3)
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
					KeyAmount += 1;
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString() + " / 8";
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key4)
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
					Key_amount_label.Text = KeyAmount.ToString() + " / 8";
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key5)
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
					KeyAmount += 1;
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString() + " / 8";
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key6) 
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
					KeyAmount += 1; 
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString() + " / 8"; 
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key7)
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
					KeyAmount += 1; 
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString() + " / 8"; 
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Key8) 
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key 888");
					Key = GetParent().GetNode<Sprite2D>("keyset/StaticBody2D-key-8/Sprite2D");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-8/CollisionShape2D");
					Key_Collision.Disabled = true;
					Key.Visible = false;
					KeyAmount += 1; 
					GD.Print("Key Amount: ", KeyAmount);
					Key_amount_label = GetParent().GetNode<Label>("Touchcontrols/Key_amount");
					Key_amount_label.Text = KeyAmount.ToString() + " / 8"; 
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Gate)
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Gate");
					TheGate = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate"); // Disable the collision shape of the gate
					if (KeyAmount < 8)
					{
						GD.Print("You need 8 keys to open the gate!");
						return;
					}
					CollisionShape2D TheGate_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-Gate/CollisionShape2D");
					TheGate_Collision.Disabled = true;
					GD.Print("Key Amount: ", KeyAmount);
					Timer.Wingame = true;
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
					GD.Print("test 53" + timeremaining);
					float spentTime = 300f - timeremaining;
					String spentTimeText = "Time spent: " + spentTime.ToString("F1") + " s";
					time_spent.Text = spentTimeText;
					GD.Print("test 56");
					gamestart = false; 
					if (bossfight)
					{
						bossfight_label.Text = "Boss Fight: Yes";
					}
					else
					{
						bossfight_label.Text = "Boss Fight: No";
					}

					scoreValue = scoreValue + Timer.countdownTime * 10;
					String scoreText = "Score: " + Mathf.RoundToInt(scoreValue).ToString("F0");
					score.Text = scoreText;
					GD.Print("test 59");
					wingamescreen.Visible = true; 
					TouchScreenButton Button = GetNode<TouchScreenButton>("../Touchcontrols/Button");
					TouchScreenButton return_to_main_win_Button = GetNode<TouchScreenButton>("../Touchcontrols/return_to_main_win");
					return_to_main_win_Button.Visible = true; 
					Label username = GetNode<Label>("../Touchcontrols/username");
					username.Visible = false; 
					Button.Visible = true; 
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
						Freezer_icon.Visible = true;
						GD.Print("You are now holding a freezer!");
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Freezer2)
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
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Freezer3) 
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
						Freezersquarrel();
					}
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Sword1)
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return;
					}
					GD.Print("Collided with a sword");
					Sprite2D Sword_icon = GetNode<Sprite2D>("../Touchcontrols/sword_icon");
					Sword_icon.Visible = true; 
					Sword = GetParent().GetNode<StaticBody2D>("swordset/StaticBody2D-sword-1");
					Sword_Collision = GetParent().GetNode<CollisionShape2D>("swordset/StaticBody2D-sword-1/CollisionShape2D");
					Sword_Collision.Disabled = true;
					Sword.Visible = false;
					holdingSword = true;
					GD.Print("You are now holding a sword!");
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Sword2) 
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return;
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
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Sword3)
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; 
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
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Sword4) 
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; 
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
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Sword5)
			{
				if (currentCooldown <= 0f)
				{
					if (holdingFreezer || holdingSword)
					{
						GD.Print("You already have a sword!");
						return; 
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
					ChangeCharacterTexture();
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
				}
			}
			if (collider is Monster_boss)
			{
				HandleMonsterBossCollision();
			}
		}
	}

	// Handles collision with the boss monster
	private void HandleMonsterBossCollision()
	{
		 // Logic for boss collision
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
			The_Boss_health.Value = Boss_health;
			GD.Print("Boss health: ", Boss_health);
			holdingSword = false;
			RestoreCharacterTexture();
			if (Boss_health == 0f)
			{
				bossfight = true;
				scoreValue += 4000;
				GD.Print("You defeated the boss!");
				The_Boss = GetParent().GetNode<CharacterBody2D>("Monster/monster-boss");
				CollisionShape2D The_Boss_Collision = GetParent().GetNode<CollisionShape2D>("Monster/monster-boss/CollisionShape2D");
				if (The_Boss_Collision == null)
				{
					GD.Print("The_Boss_Collision is null, check the node path.");
				}
				The_Boss_Collision.Disabled = true; 
				The_Boss.Visible = false;
			}
		}
		else if (holdingSword == false)
		{

			SetHeartInvisible();
			Position = startPosition;
			GD.Print("You need a sword to attack the boss!");
		}

		currentCooldown = cooldownTime;
	}

	 // Changes the character's texture to indicate holding a freezer
	private void Freezersquarrel()
	{
		// Logic for changing texture
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); 
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/ice_squarrel.png"); 
		GD.Print("Character texture changed successfully.");
	}

	 // Restores the character's texture after dropping the freezer
	private void RestoreFreezersquarrel()
	{
		 // Logic for restoring texture
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D");
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); 
		GD.Print("Character texture changed successfully.");
	}

   // Changes the character's texture to indicate holding a sword
	private void ChangeCharacterTexture()
	{
		// Logic for changing texture
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); 
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/Squirrel_sword.png");
		GD.Print("Character texture changed successfully.");
	}

	// Restores the character's texture after dropping the sword
	public void RestoreCharacterTexture()
	{
		// Logic for restoring texture
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); 
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png");
		GD.Print("Character texture changed successfully.");
	}

   // Handles collision with a monster
	private void OnMonsterCollision(Monster1 monster)
	{
		// Logic for monster collision
		GD.Print("Handling collision with monster: ", monster.Name);
		
	}

	// Sets a heart sprite to invisible when health decreases
	private void SetHeartInvisible()
	{
		 // Logic for handling health decrease
		Health = Health - 1;
		if (Health == 2)
		{
			heart1.Visible = false;
			GD.Print("Heart1 is now invisible: ", heart1.Visible);
			return;
		}
		if (Health == 1)
		{

			heart2.Visible = false;
			GD.Print("Heart2 is now invisible: ", heart2.Visible);
			return;
		}
		if (Health == 0)
		{
			heart3.Visible = false;
			GD.Print("Heart3 is now invisible: ", heart3.Visible);
			gameover = GetParent().GetNode<Sprite2D>("Touchcontrols/gameover");
			gameover.Visible = true;
			GD.Print("Game Over! You have no hearts left. 1");
			RetryButton = GetParent().GetNode<TouchScreenButton>("Touchcontrols/Button");
			RetryButton.Visible = true;
			TouchScreenButton ReturnButton = GetParent().GetNode<TouchScreenButton>("Touchcontrols/return_to_main_failed");
			ReturnButton.Visible = true;
			Label username = GetParent().GetNode<Label>("Touchcontrols/username");
			username.Visible = false;
			GD.Print("Game Over! You have no hearts left.");
			return;

		}

	}



}
