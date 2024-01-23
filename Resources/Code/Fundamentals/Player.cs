
using Godot;
using System.Diagnostics;

public partial class Player : Node3D
{
	
	private Node3D _Camera;

	private Vector2 _rotation;
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
	private bool _CanSprint;


	//Collision	
	private RayCast3D _frontRayCast;


	//Debuging
	[Export]
	public bool _Debug = false;
	public DrawLine3D _DrawLine3D;
	public Node3D _ReachStart;
	public Node3D _ReachEnd;


    public override void _Ready()
	{
		_Camera = GetNode<Node3D>(NodePathConstants.PLAYER_CAMERA);

		_rotation = new Vector2();
		_rigidBody = GetNode<RigidBody3D>(NodePathConstants.PLAYER_RIGIDBODY);
		_frontRayCast = GetNode<RayCast3D>(NodePathConstants.PLAYER_RAYCAST);


		_CanSprint = true;		


		Input.MouseMode = Input.MouseModeEnum.Captured;


		if (_Debug)
		{
			_ReachEnd = GetNode<Node3D>("Rigid_Body/Reach/End");
			_ReachStart = GetNode<Node3D>("Rigid_Body/Reach/Start");
         
			_DrawLine3D = new DrawLine3D();
			AddChild(_DrawLine3D);
		}
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
		velocity = direction * _acceleration;	

        
		if ( _CanSprint && Input.IsActionJustReleased("Sprint"))
		{
			velocity *= 3.0f; 
			Dash();
		}

			
		if (Input.IsActionPressed("QUIT"))
		{
			GetTree().Quit();
		}


		//Linear Movements
		_rigidBody.ApplyCentralForce(velocity);
		_rigidBody.LinearVelocity = _rigidBody.LinearVelocity.LimitLength(_speed);
		


		//Rotational Movements		
		_rigidBody.RotateObjectLocal(Vector3.Up,Mathf.DegToRad(_rotation.X));
		_Camera.RotateObjectLocal(Vector3.Right,Mathf.DegToRad(_rotation.Y));

		//Clamp up and down looking angle
		var _CameraRotation = _Camera.RotationDegrees;
		_CameraRotation.X = Mathf.Clamp(_Camera.RotationDegrees.X, -80, 80);
		_Camera.RotationDegrees = _CameraRotation;
	
	

		if (Input.IsActionJustPressed("Action") && _frontRayCast.IsColliding())
		{
			
			var target  = _frontRayCast.GetCollider() as StaticBody3D;
			if (target != null)
			{ 
				var root = Tools.GetRoot(target); 
				root.QueueFree();
			}
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
		if (_Debug)
		{
			//Will draw ray infront of player
			_DrawLine3D.DrawLine(_ReachStart.GlobalPosition,_ReachEnd.GlobalPosition, new Color(1f,0,0),1);
		}
	}

	
	
	public async void Dash()
	{
		_speed *= 2;
		_CanSprint = false;
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);		
		_CanSprint = true;
		_speed /= 2;
	}

}