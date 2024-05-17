using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Rigid_Body : Node3D
{

	[Export]
	public bool _Visible = true;


    Dictionary<Node3D, Vector3> _ChildrenAndPos;

	private RigidBody3D _RigidBody;
	private Collision_Body _Area;
	public override void _Ready()
	{

		Visible = _Visible;

		Array<Node> _Children = this.GetChildren();
		_ChildrenAndPos = new Dictionary<Node3D, Vector3>();

		foreach(Node3D child in _Children)
		{
			if (child is not RigidBody3D)
			{ 
				_ChildrenAndPos.Add(child, child.Position);
			}
			
			if(child is RigidBody3D)
			{
				_RigidBody = child as RigidBody3D;
			}

			if(child is Collision_Body)
			{
				_Area = child as Collision_Body;
			}
        }


	}

	public override void _Process(double delta)
	{
		foreach (var child in _ChildrenAndPos)
		{
			if (child.Key != _RigidBody)
			{
				child.Key.GlobalPosition = _RigidBody.GlobalPosition+_ChildrenAndPos[child.Key];
				
				
				var rotation = child.Key.GlobalRotation;
				rotation.Y = _RigidBody.GlobalRotation.Y;
				child.Key.GlobalRotation = rotation;
			}
			
		}
    }

	public void AddChild(Node3D child)
	{
		AddChild(child, Vector3.Zero);
	}

	public void AddChild(Node3D child, Vector3 localPos)
	{
		var rb = Tools.FindRigidBodyFromRoot(child);

		child.Position = Vector3.Zero;
		rb.SetPosition(Vector3.Zero);

		try { 
		_ChildrenAndPos.Add(child as Node3D, localPos);
		}
		catch (ArgumentException ae)
		{
            GD.Print("Child already added");
        }
		//rb.CollisionLayer (0);
		//rb.CollisionMask(0);
	}

	public void RemoveChild(Node3D child)
	{
        GD.Print(_ChildrenAndPos.Remove(child));
		 var rb = Tools.FindRigidBodyFromRoot(child);
		rb.Enable();
    }

	public uint ChildrenSize()
	{
		return (uint)_ChildrenAndPos.Count ;
	}


	public void CollisionLayer(uint layer)
	{
        _RigidBody.CollisionLayer = layer;
    }

	public void CollisionMask(uint mask)
	{
        _RigidBody.CollisionMask = mask;
    }

	public void Disable()
	{
		
		_RigidBody.ProcessMode = ProcessModeEnum.Disabled;
		_RigidBody.Sleeping = true;
		_Area.ProcessMode = ProcessModeEnum.Disabled;
	}

	public void Enable()
	{
		
		_RigidBody.ProcessMode = ProcessModeEnum.Inherit;
		_RigidBody.Sleeping = false;
		_Area.ProcessMode = ProcessModeEnum.Inherit;
	}

	public void AddImpulse(Vector3 dir)
	{
		_RigidBody.ApplyImpulse(dir, Vector3.Zero);

	}

	public void AddForce(Vector3 dir)
	{
		_RigidBody.ApplyForce(dir, Vector3.Zero);
	}

	public void SetLinearVelocity(Vector3 vel)
	{
        _RigidBody.LinearVelocity = vel;
    }

	public void AddAngularForce(Vector3 dir)
	{
        _RigidBody.ApplyTorque(dir);
    }

	public Vector3 GetPosition()
	{
		return _RigidBody.Position;
	}



	
	public void SetPosition(Vector3 newPos)
	{
		_RigidBody.Position = newPos;
	}

	public string ToString()
	{
		return _RigidBody.LinearVelocity.ToString();
	}
}