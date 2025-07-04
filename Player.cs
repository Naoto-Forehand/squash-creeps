using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	private int _speed { get; set; } = 14;
	
	[Export]
	private int _fallAcceleration { get; set; } = 75;
	
	[Export]
	private int _jumpImpulse { get; set; } = 20;
	
	[Export]
	private int _bounceImpulse { get; set; } = 16;
	
	[Signal]
	public delegate void HitEventHandler();
	
	private Vector3 _targetVelocity = Vector3.Zero;
	
	private Node3D _pivot;
	private AnimationPlayer _animationPlayer;
	
	private void Die()
	{
		GD.Print("Emit Signal For player hit");
		EmitSignal(SignalName.Hit);
		QueueFree();
	}
	
	public void GameOver(Node3D body)
	{
		GD.Print("HIT");
		Die();
	}
	
	public override void _Ready()
	{
		_pivot = GetNode<Node3D>("Pivot");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
			_animationPlayer.SpeedScale = 4;
		}
		else
		{
			_animationPlayer.SpeedScale = 1;
		}
		
		_targetVelocity.X = direction.X * _speed;
		_targetVelocity.Z = direction.Z * _speed;
		
		if (!IsOnFloor())
		{
			_targetVelocity.Y -= _fallAcceleration * (float)delta;
		}
		
		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			_targetVelocity.Y = _jumpImpulse;
		}
		
		for (int index = 0; index < GetSlideCollisionCount(); ++index)
		{
			KinematicCollision3D collision = GetSlideCollision(index);
			
			if (collision.GetCollider() is Mob mob)
			{
				if (Vector3.Up.Dot(collision.GetNormal()) > 0.1f)
				{
					mob.Squash();
					_targetVelocity.Y = _bounceImpulse;
					break;
				}
			}
		}
		
		Velocity = _targetVelocity;
		MoveAndSlide();
		_pivot.Rotation = new Vector3((Mathf.Pi / 6f * Velocity.Y / _jumpImpulse), _pivot.Rotation.Y, _pivot.Rotation.Z);
	}
}
