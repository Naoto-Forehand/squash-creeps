using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	private int _speed { get; set; } = 14;
	
	[Export]
	private int _fallAcceleration { get; set; } = 75;
	
	private Vector3 _targetVelocity = Vector3.Zero;
	
	private Node3D _pivot;
	
	public override void _Ready()
	{
		_pivot = GetNode<Node3D>("Pivot");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;
		
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1f;
		}
		if (Input.IsActionPressed("move_back"))
		{
			direction.Z += 1f;
		}
		
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			_pivot.Basis = Basis.LookingAt(direction);
		}
		
		_targetVelocity.X = direction.X * _speed;
		_targetVelocity.Z = direction.Z * _speed;
		
		if (!IsOnFloor())
		{
			_targetVelocity.Y -= _fallAcceleration * (float)delta;
		}
		
		Velocity = _targetVelocity;
		MoveAndSlide();
	}
}
