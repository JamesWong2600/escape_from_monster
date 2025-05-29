using Godot;
using System;

public partial class Swordanimationplayer : Node3D
{
	// Called when the node enters the scene tree for the first time.
	private AnimationPlayer animationPlayer;
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.AnimationFinished += OnAnimationFinished;
		animationPlayer.Play("new_animation");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnAnimationFinished(StringName animName)
	{
		GD.Print($"Animation '{animName}' finished!");
		animationPlayer.Play("new_animation");
		// You can trigger more logic here
	}
	
}
