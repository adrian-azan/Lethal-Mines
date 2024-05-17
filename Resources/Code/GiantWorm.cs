using Godot;
using System;

public partial class GiantWorm : Node3D
{

	private Rigid_Body _RigidBody;
	public float direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		direction = 0;
		_RigidBody = Tools.FindRigidBody(this);
		_RigidBody.SetLinearVelocity(new Vector3(1, 0, 0));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		direction += .01f;
		var dirImplulse = new Vector3();

		_RigidBody.AddForce(dirImplulse);
		//_RigidBody.AddAngularForce(new Vector3(0,.1f,0));
	}
}
