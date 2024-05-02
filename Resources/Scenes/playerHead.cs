using Godot;
using System;

public partial class PlayerHead : Node3D
{

	AnimationPlayer _animationPlayer;

	[Export(PropertyHint.Range, "0,1,0.05")]
	private float _bobbingWeight = 1.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("Camera3D/AnimationPlayer");
        _animationPlayer.Play("player_headBob");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void SetSpeed(float speed)
	{

        _animationPlayer.SpeedScale = speed*_bobbingWeight;
    }
}
