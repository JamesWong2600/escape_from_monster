using Godot;
using System;

public partial class Monster3 : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public static Vector2 startPosition = new Vector2(3955f, 580f);

	public float Speed = 300;
	private Vector2 direction = Vector2.Zero;

	public int Health = 100;
	private Node2D player = null;
	private bool IsFreeze = false;
	private float Monster_cooldownTime = 15.0f; // Cooldown duration in seconds
	public static float Monster_currentCooldown = 0.0f; 
	private float cooldownTime = 1.0f; // Cooldown duration in seconds
	private float currentCooldown = 0.0f; 
	private Sprite2D Monster_texture;
	private bool alreadystarted = false;
	private ProgressBar The_Monster_Cooldown_bar;
	private AnimatedSprite2D animatedSprite;
	private RandomNumberGenerator rng = new();
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Position = startPosition;
		rng.Randomize();
		PickNewDirection();
		The_Monster_Cooldown_bar = GetNodeOrNull<ProgressBar>("cooldown_bar");
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Character.gamestart == false)
		{
			return;
		}
		else if (Character.gamestart == true && alreadystarted == false)
		{
			Position = startPosition;
			rng.Randomize();
			PickNewDirection();
			The_Monster_Cooldown_bar = GetNodeOrNull<ProgressBar>("cooldown_bar");
			alreadystarted = true;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Character.gamestart == false)
		{
			return;
		}
		else if (Character.gamestart == true)
		{
			if (Monster_currentCooldown > 0f)
			{
				Velocity = Vector2.Zero;
				Monster_currentCooldown -= (float)delta;
				The_Monster_Cooldown_bar.Value = Math.Round(Monster_currentCooldown, 2);
				//GD.Print("Monster is on cooldown. Remaining time: " + Monster_currentCooldown);
				//GD.Print("Monster position: ", GlobalPosition);
			}
			if (Monster_currentCooldown <= 0f && IsFreeze)
			{
				IsFreeze = false;
				The_Monster_Cooldown_bar.Visible = false;
				RestoreMonsterCharacterTexture();
				AnimatedSprite2D Monster_animatedSprite2D = GetParent().GetNode<AnimatedSprite2D>("../Monster/monster-3/AnimatedSprite2D");
				if (Monster_animatedSprite2D == null)
				{
					GD.PrintErr("Monster AnimatedSprite2D node not found!");
					return;
				}
				Monster_animatedSprite2D.Play("run");
				GD.Print("Monster cooldown finished. Character is no longer frozen.");
				return;
			}
			if (Monster_currentCooldown <= 0f)
			{
				//character.holdingFreezer = false;
				//PickNewDirection(); // Change direction on collision
				Monster_currentCooldown = 0;
				Vector2 velocity = direction * Speed;
				//GD.Print("v3: "+velocity);
				if (velocity.X > 0)
				{
					animatedSprite.FlipH = true;
				}
				else if (velocity.X < 0)
				{
					animatedSprite.FlipH = false;
				}
				//GD.Print("Flip3: ");
				MoveAndSlide();
				KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);
				if (collision != null)
				{
					Node collider = (Node)collision.GetCollider();
					if (collider is CharacterBody2D collidcharacter) // Check if the collider is the character
					{
						//GD.Print("mymy " + collidcharacter.Name);
						if (collidcharacter.Name == "character")
						{
							GD.Print(collidcharacter.Name);
							if (Monster_currentCooldown == 0f)
							{

								if (Character.holdingFreezer)
								{
									Character.holdingFreezer = false;
									IsFreeze = true;
									MonsterCharacterTexture();
									RestoreCharacterTexture();
									Monster_currentCooldown = Monster_cooldownTime;
									The_Monster_Cooldown_bar.Visible = true;
									Sprite2D Freezer_icon = GetParent().GetNode<Sprite2D>("../Touchcontrols/freezer_icon");
									AnimatedSprite2D Monster_animatedSprite2D = GetParent().GetNode<AnimatedSprite2D>("../Monster/monster-3/AnimatedSprite2D");
									Monster_animatedSprite2D.Play("cooldown");
									Freezer_icon.Visible = false;
									The_Monster_Cooldown_bar.Value = Monster_cooldownTime;
									GD.Print("Monster is freezing the character. " + Monster_currentCooldown);
									return;
								}
							}
						}
					}

					PickNewDirection();
				}
			}

			if (player != null)
			{
				Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
				Velocity = direction * Speed;
				GD.Print("Player entered the monster's area.");
				MoveAndSlide();
			}
		}
	}
	private void MonsterCharacterTexture()
	{
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/monster_cooldown.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
		//this
	private void RestoreCharacterTexture()
	{
		Sprite2D characterSprite =GetParent().GetNode<Sprite2D>("../character/Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/squarrel.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
   	private void RestoreMonsterCharacterTexture()
	{
		Sprite2D characterSprite = GetNodeOrNull<Sprite2D>("Sprite2D"); // Adjust the path to your character's Sprite2D node
		if (characterSprite == null)
		{
			GD.PrintErr("Character Sprite2D node not found!");
			return;
		}
		characterSprite.Texture = GD.Load<Texture2D>("res://character/monster.png"); // Set the new texture
		GD.Print("Character texture changed successfully.");
	}
	public void TakeDamage(int amount)
	{
		Health -= amount;
		if (Health <= 0)
		{
			QueueFree(); // Destroy the monster
		}
	}


	private void OnArea2DBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			player = body;
		}
	}

	private void OnArea2DBodyExited(Node2D body)
	{
		if (body == player)
		{
			player = null;
		}
	}
	private void PickNewDirection()
	{
		// Pick a random normalized direction (up, down, left, right, or diagonal)
		float angle = rng.RandfRange(0, Mathf.Tau); // Tau = 2π
		direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).Normalized();
	}
}
