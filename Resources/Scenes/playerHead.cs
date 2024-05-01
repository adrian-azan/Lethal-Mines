using Godot;
using System;

public partial class playerHead : Node3D
{

	AnimationPlayer _animationPlayer;

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
}
