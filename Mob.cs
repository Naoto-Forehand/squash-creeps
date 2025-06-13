using Godot;
using System;

public partial class Mob : CharacterBody3D
{
	[Export]
	private int _minSpeed { get; set; } = 10;
	[Export]
	private int _maxSpeed { get; set; } = 18;
	
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
	
	public void Initialize(Vector3 startPosition, Vector3 playerPosition)
	{
		LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
		RotateY((float)GD.RandRange((-Mathf.Pi / 4f), (Mathf.Pi / 4f)));
		
		int randomSpeed = GD.RandRange(_minSpeed, _maxSpeed);
		Velocity = Vector3.Forward * randomSpeed;
		Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
	}
	
	private void OnScreenExit()
	{
		QueueFree();
	}
}
