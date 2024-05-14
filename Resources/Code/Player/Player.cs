
using Godot;
using System;

public partial class Player : Node3D
{
	
	private PlayerCamera _camera;
	private RigidBody3D _rigidBody;
	private Rigid_Body _RigidBody;
	private StaminaBar _staminaBar;
	private Vector2 _rotation;


	private float _maxSpeed = 4f;

	[Export]
	private float _baseSpeed;

	[Export]
	private float _dashSpeed;

	private float _stamina = 50f;
	private float _staminaDrain = 60f;
	private float _staminaRecovery = 20f;



	[ExportCategory("Controls")]
	[Export]
	private float _mouseSensitivity = .05f;



    public override void _Ready()
	{
		_rigidBody = GetNode<RigidBody3D>("Rigid_Body/RigidBody3D");
		_RigidBody = GetNode<Rigid_Body>("Rigid_Body");
		_camera = GetNode<PlayerCamera>("Rigid_Body/PlayerCamera");
		_staminaBar = GetNode<StaminaBar>("UI/StaminaBar");

		
		_rotation = new Vector2();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

  public override void _PhysicsProcess(double delta)
{
	Vector3 velocity;
	Vector3 direction = new Vector3(0, 0, 0);
	
		if (Input.IsActionPressed("MoveForward"))
		{
			direction.Z -= 1.0f; // Move forward
		}
		if (Input.IsActionPressed("MoveBackward"))
		{
			direction.Z += 1.0f; // Move backward
		}
		if (Input.IsActionPressed("MoveRight"))
		{
			direction.X += 1.0f; // Move left
		}
		if (Input.IsActionPressed("MoveLeft"))
		{
			direction.X -= 1.0f; // Move right
		}

		direction = (_rigidBody.Basis * direction).Normalized();
		velocity = direction;	

        
		if ( _staminaBar._exhausted == false && _stamina > 0 && Input.IsActionPressed("Sprint"))
		{
			velocity *= _dashSpeed;
			_staminaBar.Drain((float)delta);
			_camera.SetSpeed(20);
		}
		else
		{
			_camera.SetSpeed(0);
			velocity *= _baseSpeed;
		}

	
		_rigidBody.ApplyCentralForce(velocity);	//Linear Movements
		_rigidBody.LinearVelocity = _rigidBody.LinearVelocity.LimitLength(_maxSpeed);
		_rigidBody.ApplyCentralForce(new Vector3(0,-80,0)); // Gravity


	


		//Rotational Movements		
		_rigidBody.RotateObjectLocal(Vector3.Up,Mathf.DegToRad(_rotation.X));
		_camera.RotateObjectLocal(Vector3.Right,Mathf.DegToRad(_rotation.Y));

		var _CameraRotation = _camera.RotationDegrees;
		_CameraRotation.X = Mathf.Clamp(_camera.RotationDegrees.X, -80, 80);
		_camera.RotationDegrees = _CameraRotation;
	
	
		if (Input.IsActionPressed("QUIT"))
		{
			GetTree().Quit();
		}
				
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			if(eventMouseMotion.Relative.X < -5 || eventMouseMotion.Relative.X > 5
				|| eventMouseMotion.Relative.Y < -5 || eventMouseMotion.Relative.Y > 5)
			{ 
				_rotation.X = eventMouseMotion.Relative.X * -_mouseSensitivity;
				_rotation.Y = eventMouseMotion.Relative.Y * -_mouseSensitivity;
			}
			else
			{
				_rotation = new Vector2(0,0);
			}

        }
		
    }




	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_staminaBar.Process((float)delta);
	}
}