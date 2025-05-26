using Godot;
using System;

public partial class character : CharacterBody2D
{
	private Vector2 startPosition = new Vector2(1180, 510);
	private Vector2 targetPosition = new Vector2(1000, 0);
	private Vector2 Level2Position = new Vector2(4500, 3892);
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
	private float cooldownTime = 0.5f; // seconds
	private float currentCooldown = 0f;
	private StaticBody2D Key;

	private StaticBody2D TheGate;
	private CollisionShape2D Key_Collision;
	public float KeyAmount = 0;
	private Label Key_amount_label; // Label to display key amount
	public int Health = 3; // Example health variable
	public override void _Ready()
	{
		GD.Print("Hello, Godot!");
		Position = startPosition;
		elapsedTime = 0.0f;
		moving = true;
		var cam = GetNode<Camera2D>("Camera2D");
		cam.MakeCurrent();
		GD.Print("Camera is now current.");

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

		int collisionCount = GetSlideCollisionCount();
		for (int i = 0; i < collisionCount; i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node collider = collision.GetCollider() as Node;


			// Check if the collider is a monster
			if (collider is Monster1 || collider is Monster2 || collider is Monster3 || collider is Monster4 || collider is Monster5) // Replace 'Monster' with the actual class name of your monster
			{
				GD.Print("Collided with a monster!");
				Position = startPosition;
				if (currentCooldown <= 0f)
				{
					SetHeartInvisible();
					currentCooldown = cooldownTime;
				}
				OnMonsterCollision(collider as Monster1);
			}
			if (collider is Key1) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-1");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-1/CollisionShape2D");
					Key_Collision.Disabled = true;
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
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-2");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-2/CollisionShape2D");
					Key_Collision.Disabled = true;
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
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-3");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-3/CollisionShape2D");
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
			if (collider is Key4) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-4");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-4/CollisionShape2D");
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
			if (collider is Key5) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-5");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-5/CollisionShape2D");
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
			if (collider is Key6) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-6");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-6/CollisionShape2D");
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
			if (collider is Key7) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-7");
					Key_Collision = GetParent().GetNode<CollisionShape2D>("keyset/StaticBody2D-key-7/CollisionShape2D");
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
			if (collider is Key8) // Replace 'Monster' with the actual class name of your monster
			{
				if (currentCooldown <= 0f)
				{
					GD.Print("Collided with a Key");
					Key = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-key-8");
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
					TheGate = GetParent().GetNode<StaticBody2D>("keyset/StaticBody2D-Gate");
					if (KeyAmount < 8)
					{
						GD.Print("You need 7 keys to open the gate!");
						return; // Exit if not enough keys
					}
					TheGate.Visible = false;
					Position = Level2Position; // Move the character to the target position
					GD.Print("Key Amount: ", KeyAmount);
					if (currentCooldown <= 0f)
					{
						currentCooldown = cooldownTime;
					}
					// Hide the key sprite
				}
			}
		}

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
			return;

		}

	}
		
		
}
