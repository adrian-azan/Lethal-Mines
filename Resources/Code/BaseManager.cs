using Godot;
using System;

public partial class BaseManager : Node
{
    private Node3D _baseHatch;
    private Node3D _baseSpawn;

    public override void _Ready()
    {
        _baseHatch = GetNode<Node3D>("BaseHatch");
        _baseSpawn = GetNode<Node3D>("Inside/SPAWN");
    }

    public Vector3 GetInsideBaseLocation()
    {
        return _baseSpawn.GlobalPosition;
    }

    public Vector3 GetOutsideBaseLocation()
    {
        return _baseHatch.GlobalPosition;
    }
}