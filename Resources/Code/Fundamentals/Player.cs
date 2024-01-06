
using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public partial class Player : Node3D
{
	private RigidBody3D _rigidBody;
	private Rigid_Body _RigidBody;
	private float _acceleration = 50f;
	private float _speed = 4f;

	public bool _Prepared {get; private set;}
  public override void _PhysicsProcess(double delta)
{
	Vector3 velocity = new Vector3(0, 0, 0);
		Vector3 rotation = new Vector3(0, 0, 0);

	
		if (Input.IsActionPressed("MoveForward"))
		{
			velocity.Z -= 1.0f; // Move forward
		}
		if (Input.IsActionPressed("MoveBackward"))
		{
			velocity.Z += 1.0f; // Move backward
		}
		if (Input.IsActionPressed("MoveRight"))
		{
			velocity.X += 1.0f; // Move left
		}
		if (Input.IsActionPressed("MoveLeft"))
		{
			velocity.X -= 1.0f; // Move right
		}

		if (Input.IsActionPressed("RotateLeft"))
		{
            rotation.Y += 1.0f; // Move right
        }
		if (Input.IsActionPressed("RotateRight"))
		{
            rotation.Y -= 1.0f; // Move right
        }

		// Normalize the velocity vector to ensure consistent speed when moving diagonally.
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * _acceleration;
		}

		// Normalize the velocity vector to ensure consistent speed when moving diagonally.
		if (rotation.Length() > 0)
		{
			rotation = rotation.Normalized() * _acceleration;
		}

		if ( _Prepared && Input.IsActionJustReleased("Sprint"))
		{
			velocity *= 3.0f; 
			Dash();
		}

	
		
	_rigidBody.ApplyTorque(rotation);
	_rigidBody.AngularVelocity = _rigidBody.AngularVelocity.LimitLength(.1f);

	var directionDegrees = _rigidBody.GlobalRotationDegrees;

	//velocity.X = Godot.Mathf.Cos(directionDegrees.Y) *velocity.X - 
		//	Godot.Mathf.Sin(directionDegrees.Y) * velocity.Z;

	
//	velocity.Z = Godot.Mathf.Sin(directionDegrees.Y) *velocity.X + 
	//		Godot.Mathf.Cos(directionDegrees.Y) * velocity.Z;
		


	_rigidBody.ApplyCentralForce(velocity);
	_rigidBody.LinearVelocity = _rigidBody.LinearVelocity.LimitLength(_speed);

	_rigidBody.ApplyTorque(rotation);

	_rigidBody.AngularVelocity = _rigidBody.AngularVelocity.LimitLength(.1f);

	//	_rigidBody.GlobalRotate(Vector3.Up, Tools.DegToRad(rotation.Y));
	
	
			
}

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rigidBody = GetNode<RigidBody3D>("Rigid_Body/RigidBody3D");
		_RigidBody = GetNode<Rigid_Body>("Rigid_Body");
		
		_Prepared = true;
		_rigidBody.GravityScale = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
	
	public async void Dash()
	{
		_speed *= 2;
		_Prepared = false;
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);		
		_Prepared = true;
		_speed /= 2;
	}

	public void Check(bool state)
	{

		for (int i = 0; i < GetChildCount(); i++)
		{
			var child = GetChild<Node3D>(i);
			GD.Print(child.Name);
			if (child.Name == "Exclamation")
			{
				child.Visible = state;
			}
			else
			{
				for (int j = 0; j < child.GetChildCount(); j++)
				{
					var grandchild = child.GetChild<Node3D>(j);
					GD.Print(grandchild.Name);
					if (grandchild.Name == "Exclamation")
					{
						grandchild.Visible = state;
					}
				}
			}
		}		
	}
}







