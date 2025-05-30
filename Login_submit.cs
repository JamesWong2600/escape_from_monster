using Godot;
using System;
using System.Text.Json;
using System.Collections.Generic;
public partial class Login_submit : TouchScreenButton
{
    public static string username_value;
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
		string url = "http://127.0.0.1:8000/login/";
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
			username.Text = "Welcome "+username_value;
			main_screen.Visible = false; // Show the main screen
			Key.Visible = false;
			Visible = false;
			Character.gamestart = true;
			Character.IsLogin = true; // Assuming Character is a globally accessible object or singleton
		}
		else
		{
			if (responseCode == 401)
			{
				GD.Print("Error: invaid username or password.");
				AcceptDialog alert = GetNode<AcceptDialog>("AlertDialog");
				alert.Visible = true;
				alert.DialogText = "Error: Passwords do not match.";
				alert.PopupCentered();
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
