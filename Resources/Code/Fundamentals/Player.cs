
using Godot;
using System.Diagnostics;

public partial class Player : Node3D
{
	
	private PlayerHead _Camera;
	private RigidBody3D _rigidBody;
	private Rigid_Body _RigidBody;

	[ExportCategory("Physics")]
	[Export(PropertyHint.Range, "0,80,2,hide_slider")]
	private float _acceleration = 50f;
	
	private float _maxSpeed = 4f;

	[Export]
	private float _baseSpeed = 2f;




	[Export]
	private float _dashSpeed = 3f;

	private float _stamina = 20f;
	private bool _exhausted = false;



	[ExportCategory("Controls")]
	[Export]
	private float _mouseSensitivity = .05f;


	private Vector2 _rotation;




    public override void _Ready()
	{
		_rigidBody = GetNode<RigidBody3D>("Rigid_Body/RigidBody3D");
		_RigidBody = GetNode<Rigid_Body>("Rigid_Body");
		_Camera = GetNode<Node3D>("Rigid_Body/Head") as PlayerHead;
		
		_rotation = new Vector2();

		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

  public override void _PhysicsProcess(double delta)
{
	Vector3 velocity = new Vector3(0, 0, 0);
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
		velocity = direction * _acceleration;	

        
		if ( _exhausted == false && _stamina > 0 && Input.IsActionPressed("Sprint"))
		{
			velocity *= _dashSpeed;
			_stamina -= 20.0f * (float)delta;
			_maxSpeed = _dashSpeed;
			_Camera.SetSpeed(20);
		}
		else
		{
			_Camera.SetSpeed(0);
			_maxSpeed = _baseSpeed;
		}


		//Linear Movements
		_rigidBody.ApplyCentralForce(velocity);
		_rigidBody.LinearVelocity = _rigidBody.LinearVelocity.LimitLength(_maxSpeed);
		


		//Rotational Movements		
		_rigidBody.RotateObjectLocal(Vector3.Up,Mathf.DegToRad(_rotation.X));
		_Camera.RotateObjectLocal(Vector3.Right,Mathf.DegToRad(_rotation.Y));

		var _CameraRotation = _Camera.RotationDegrees;
		_CameraRotation.X = Mathf.Clamp(_Camera.RotationDegrees.X, -80, 80);
		_Camera.RotationDegrees = _CameraRotation;
	
	
	
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
		if (_stamina <= 0)
		{
            _exhausted = true;
        }
        else if(_stamina >= 20)
		{
            _exhausted = false;
        }

		if(_stamina < 20)
		{
            _stamina += 5.0f*(float)delta;
			
        }
		
		GD.Print(_stamina);

	}
}