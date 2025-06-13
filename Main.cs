using Godot;
using System;

public partial class Main : Node
{
	[Export]
	private PackedScene _mobScene { get; set; }
	
	private PathFollow3D _mobSpawnLocation;
	private Timer _mobTimer;
	private Player _player;
	
	private void OnPlayerHit()
	{
		_mobTimer.Stop();
	}
	
	private void OnMobTimerTimeout()
	{
		Mob mob = _mobScene.Instantiate<Mob>();
		_mobSpawnLocation.ProgressRatio = GD.Randf();
		mob.Initialize(_mobSpawnLocation.Position, _player.Position);
		
		AddChild(mob);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		_mobTimer = GetNode<Timer>("MobTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
