using Godot;
using System;

public partial class ScoreLabel : Label
{ 
	private int _score = 0;
	
	public void OnMobSquashed()
	{
		_score += 1;
		Text = $"SCORE: {_score}";
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
