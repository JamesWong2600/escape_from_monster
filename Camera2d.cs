using Godot;
using System;

public partial class Camera2d : Camera2D
{
	//initialize the camera node
	public override void _Ready()
	{

		var camera = GetNodeOrNull<Camera2D>("Camera2D");

		if (camera == null)
		{
			GD.PrintErr("Camera2D node not found at path!");
		}
		else
		{
			camera.MakeCurrent();
		}
	}
}
