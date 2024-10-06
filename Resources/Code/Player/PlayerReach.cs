using Godot;
using System;

public partial class PlayerReach : Node3D
{
    private void OnBeingPercieved(Node3D other)
    {
        var target = other.GetOwnerOrNull<Node3D>();

        if (target is Player)
        {
            GD.Print("PERCIEVED {}", other);
        }
    }
}