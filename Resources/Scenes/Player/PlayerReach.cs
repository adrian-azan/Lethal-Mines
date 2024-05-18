using Godot;
using System;

public partial class PlayerReach : Node3D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OnBeingPercieved(Node3D other)
    {
        var target = other.GetOwnerOrNull<Node3D>();

        if (target is Player)
        {
            GD.Print("PERCIEVED {}", other);
        }
    }
}