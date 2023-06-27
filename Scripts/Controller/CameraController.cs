using Godot;
using System;

public partial class CameraController : Node2D
{
	[Export] private float _MoveSpeed = 10;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 movePosition = new Vector2();

		if (Input.IsActionPressed("MoveUp"))
			movePosition.Y -= 1;

		if (Input.IsActionPressed("MoveDown"))
			movePosition.Y += 1;

		if (Input.IsActionPressed("MoveLeft"))
			movePosition.X -= 1;

		if (Input.IsActionPressed("MoveRight"))
			movePosition.X += 1;

		Vector2 currentPos = this.Position;

		currentPos += movePosition * (_MoveSpeed * (float)delta);
		this.Position = currentPos;
	}
}
