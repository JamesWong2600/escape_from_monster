using Godot;
using System;

public partial class timer : Node
{
	public static float countdownTime = 300f; // 120 seconds
	private Label timerLabel; // Reference to a Label node to display the timer
	private Sprite2D Gameover;

	private Button Retrybutton;
	public static bool Wingame = false;
	public override void _Ready()
	{
		// Assuming there is a Label node as a child of this node
		timerLabel = GetNode<Label>("Label");
		Gameover = GetParent().GetNode<Sprite2D>("gameover");
		Retrybutton = GetParent().GetNode<Button>("Button");
		GD.Print("Timer started with " + countdownTime + " seconds.");
		UpdateTimerLabel();
	}

	public override void _Process(double delta)
	{
		if (countdownTime > 0)
		{
		if (character.gamestart == false)
		{
			return;
		}
		else if (character.gamestart == true &&  Wingame == false)
		{
			countdownTime -= (float)delta;
				if (countdownTime < 0)
				{
					countdownTime = 0;
					GD.Print("Time's up! Game over.");
					Gameover.Visible = true;
					Retrybutton.Disabled = false; // Enable the retry button
					Retrybutton.Visible = true;
		 	}      // Show the game over sprite when time runs out
		}

			UpdateTimerLabel();
		}
	}

	private void UpdateTimerLabel()
	{
		int minutes = (int)(countdownTime / 60);
		int seconds = (int)(countdownTime % 60);
		timerLabel.Text = $"{minutes:D2}:{seconds:D2}";
	}
}
