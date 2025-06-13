using Godot;
using System;

public partial class Main : Node
{
	[Export]
	private PackedScene _mobScene { get; set; }
	
	private PathFollow3D _mobSpawnLocation;
	private Timer _mobTimer;
	private ScoreLabel _scoreLabel;
	private Player _player;
	private Control _retry;
	
	private void OnPlayerHit()
	{
		GD.Print("Stop Mob Timer");
		_mobTimer.Stop();
		_retry.Show();
	}
	
	private void OnMobTimerTimeout()
	{
		Mob mob = _mobScene.Instantiate<Mob>();
		_mobSpawnLocation.ProgressRatio = GD.Randf();
		mob.Initialize(_mobSpawnLocation.Position, _player.Position);
		mob.Squashed += _scoreLabel.OnMobSquashed;
		
		AddChild(mob);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		_mobTimer = GetNode<Timer>("MobTimer");
		_scoreLabel = GetNode<ScoreLabel>("UserInterface/ScoreLabel");
		_retry = GetNode<Control>("UserInterface/Retry");
		_retry.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept") && _retry.Visible)
		{
			GetTree().ReloadCurrentScene();
		}
	}
}
