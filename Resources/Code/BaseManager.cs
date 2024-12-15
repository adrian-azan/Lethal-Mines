using Godot;
using System;

public partial class BaseManager : Node
{
    private BaseHatch _baseHatch;
    private Node3D _baseSpawn;

    public override void _Ready()
    {
        _baseHatch = GetNode<BaseHatch>("BaseHatch");
        _baseSpawn = GetNode<Node3D>("Inside/SPAWN");
    }

    public void Init(Player player)
    {
        RandomNumberGenerator temp = new RandomNumberGenerator();
        Vector3 playerPos = player.GetGlobalPosition();
        Vector3 baseHatchPos = new Vector3(temp.RandiRange(-20, 20) + playerPos.X, 1, temp.RandiRange(-20, 20) + playerPos.Z);

        _baseHatch.SetPosition(baseHatchPos);
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