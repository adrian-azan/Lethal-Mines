using Godot;
using System;

public partial class Compass : Node2D
{
    private Node3D _playerBase;
    private Player _player;
    private Sprite2D _arrow;

    public override void _Ready()
    {
        _player = GetNode("../..") as Player;
        _playerBase = _player.GetNode("Base/BaseHatch") as Node3D;

        _arrow = GetNode("Arrow") as Sprite2D;
    }

    public override void _Process(double delta)
    {
        var playerBaseXZ = new Vector2(_playerBase.GlobalPosition.X, _playerBase.GlobalPosition.Z);
        var playerXZ = new Vector2(_player.GetGlobalPosition().X, _player.GetGlobalPosition().Z);

        float directionToBase = Mathf.RadToDeg(playerXZ.AngleToPoint(playerBaseXZ)) + 90;
        float playerRotation = Mathf.RadToDeg(_player.GetRotation().Y);

        _arrow.RotationDegrees = directionToBase + playerRotation;
    }
}