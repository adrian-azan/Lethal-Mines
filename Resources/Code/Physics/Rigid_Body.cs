using Godot;
using Godot.Collections;
using System;

public partial class Rigid_Body : Node3D
{
    private partial class Details : GodotObject
    {
        public Vector3 Position;
        public Vector3 Scale;

        public Details(Vector3 pos, Vector3 scale)
        {
            Position = pos;
            Scale = scale;
        }

        public Details()
        {
            Position = Vector3.Zero;
            Scale = Vector3.Zero;
        }
    }

    [Export]
    private bool _Visible = true;

    private Dictionary<Node3D, Details> _ChildrenAndPos;
    private RigidBody3D _RigidBody;
    private Collision_Body _Area;

    public override void _Ready()
    {
        Visible = _Visible;

        Array<Node> _Children = this.GetChildren();
        _ChildrenAndPos = new Dictionary<Node3D, Details>();

        /*Rigid_Body is in charge of moving all children
        based on movements from RigidBody3D.
        This is to avoid having all children under RigidBody3D
        And to give more control over the children.*/
        foreach (Node3D child in _Children)
        {
            if (child is not RigidBody3D)
                _ChildrenAndPos.Add(child, new Details(child.Position, child.Scale));

            if (child is RigidBody3D)
                _RigidBody = child as RigidBody3D;

            if (child is Collision_Body)
                _Area = child as Collision_Body;
        }
    }

    public override void _Process(double delta)
    {
        foreach (var child in _ChildrenAndPos)
        {
            if (child.Key != _RigidBody)
            {
                child.Key.GlobalPosition = _RigidBody.GlobalPosition + _ChildrenAndPos[child.Key].Position;

                Vector3 rotation = child.Key.GlobalRotation;
                rotation.Y = _RigidBody.GlobalRotation.Y;
                child.Key.GlobalRotation = rotation;

                //When GlobalRotation is set, scale is reset to 1,1,1
                //Here we are saving that intial scale
                child.Key.Scale = _ChildrenAndPos[child.Key].Scale;
            }
        }
    }

    public void AddChild(Node3D child)
    {
        AddChild(child, Vector3.Zero, Vector3.One);
    }

    public void AddChild(Node3D child, Vector3 localPos, Vector3 localScale)
    {
        Rigid_Body rb = Tools.FindRigidBodyFromRoot(child);

        child.Position = Vector3.Zero;
        rb.SetPosition(Vector3.Zero);

        try
        {
            _ChildrenAndPos.Add(child as Node3D, new Details(localPos, localScale));
        }
        catch (ArgumentException ae)
        {
            GD.Print("Child already added");
        }
    }

    public void RemoveChild(Node3D child)
    {
        _ChildrenAndPos.Remove(child);

        //TODO: Revaluate if this is needed 1/22/24
        //var rb = Tools.FindRigidBodyFromRoot(child);
        //rb.Enable();
    }

    public uint ChildrenSize()
    {
        return (uint)_ChildrenAndPos.Count;
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

    public void ApplyTorque(Vector3 dir)
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

    public override string ToString()
    {
        return _RigidBody.LinearVelocity.ToString();
    }
}