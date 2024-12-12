using Godot;
using System;

public partial class ViewPortIcon : SubViewportContainer
{
    public override void _Ready()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        var test = GetNode<Camera3D>("SubViewport/Camera3D");
        if (test != null)
            test.GlobalPosition = new Vector3(rng.RandiRange(-500, 500), -30, rng.RandiRange(-500, 500));
    }
}