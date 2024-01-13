
using Godot;

public partial class Player : Node3D
{
	
	private Node3D _Camera;
	private RigidBody3D _rigidBody;
	private Rigid_Body _RigidBody;

	[ExportCategory("Physics")]
	[Export(PropertyHint.Range, "0,80,2,hide_slider")]
	private float _acceleration = 50f;
	[Export]
	private float _speed = 4f;

	[ExportCategory("Controls")]
	[Export]
	private float _mouseSensitivity = .05f;


	private Vector2 _rotation;
	public bool _prepared {get; private set;}



    public override void _Ready()
	{
		_rigidBody = GetNode<RigidBody3D>("Rigid_Body/RigidBody3D");
		_RigidBody = GetNode<Rigid_Body>("Rigid_Body");
		_Camera = GetNode<Node3D>("Rigid_Body/Head");
		
		_prepared = true;
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

        
		if ( _prepared && Input.IsActionJustReleased("Sprint"))
		{
			velocity *= 3.0f; 
			Dash();
		}


		//Linear Movements
		_rigidBody.ApplyCentralForce(velocity);
		_rigidBody.LinearVelocity = _rigidBody.LinearVelocity.LimitLength(_speed);
		


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
	}

	
	
	public async void Dash()
	{
		_speed *= 2;
		_prepared = false;
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);		
		_prepared = true;
		_speed /= 2;
	}

}