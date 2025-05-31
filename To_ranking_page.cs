using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
public partial class To_ranking_page : TouchScreenButton
{
	//[Export] public NodePath RankingListPath; // Set this in Inspector to point to VBoxContainer
	public class PlayerScore
	{
		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("scores")]
		public int Scores { get; set; }
	}

	public class ScoreResponse
	{
		[JsonPropertyName("player_scores")]
		public List<PlayerScore> PlayerScores { get; set; }
	}
	private VBoxContainer _rankingList;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}

	/*private void ShowRanking(List<(string name, int score)> data)
	{
		int rank = 1;
		foreach (var entry in data)
		{
			var row = new HBoxContainer();

			var rankLabel = new Label { Text = $"{rank}." };
			var nameLabel = new Label { Text = entry.name };
			var scoreLabel = new Label { Text = $"{entry.score} pts" };

			// Optional: Set size flags for layout
			rankLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
			nameLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
			scoreLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;

			row.AddChild(rankLabel);
			row.AddChild(nameLabel);
			row.AddChild(scoreLabel);

			_rankingList.AddChild(row);
			rank++;
		}
	}*/
	private void OnButtonPressed()
	{
		_rankingList = GetParent().GetParent().GetNode<VBoxContainer>("ranking_page/ScrollContainer/VBoxContainer");
		// Ensure the ranking list is cleared before populating
		ClearRankingList();
		HttpRequest httpRequest = GetNode<HttpRequest>("HttpRequest");
		if (_rankingList != null)
		{
			GD.Print("RankingList yes found!");
		}
		else
		{
			GD.Print("RankingList not found!");
		}



		httpRequest.CancelRequest(); // Cancels the previous one


		// JSON payload

		httpRequest.RequestCompleted += OnRequestCompleted;
		string url = config.domain+"/get_player_scores/";
		// POST request
		
		httpRequest.Request(url); // Pass the JSON string directly
		GD.Print("testtt");
		// Example ranking data

		//ShowRanking(rankings);

		Node2D main_screen = GetParent().GetParent().GetNode<Node2D>("main_screen");
		Node2D register_page = GetParent().GetParent().GetNode<Node2D>("ranking_page");
		main_screen.Visible = false; // Show the main screen
		register_page.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void ClearRankingList()
{
	foreach (Node child in _rankingList.GetChildren())
	{
		_rankingList.RemoveChild(child);
		child.QueueFree(); // Free the child node
	}
}
	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		if (body != null && body.Length > 0)
		{
			string responseText = System.Text.Encoding.UTF8.GetString(body);
			GD.Print("Response Body: ", responseText);

			try
			{
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				ScoreResponse response = JsonSerializer.Deserialize<ScoreResponse>(responseText, options);

				if (response != null && response.PlayerScores != null)
				{
					int rank = 1;
					foreach (var player in response.PlayerScores)
					{
						var row = new HBoxContainer();

						var rankLabel = new Label { Text = $"{rank}." };
						var nameLabel = new Label { Text = player.Username };
						var scoreLabel = new Label { Text = $"{player.Scores} pts" };

						rankLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
						nameLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
						scoreLabel.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;

						row.AddChild(rankLabel);
						row.AddChild(nameLabel);
						row.AddChild(scoreLabel);

						_rankingList.AddChild(row);
						rank++;
					}
				}
			}
			catch (Exception e)
			{
				GD.PrintErr("Failed to parse JSON: ", e.Message);
			}
		}
	}

}
