using Godot;
using System;

public partial class Lantern : Control
{
    private Slot _fuel;
    private AnimationTree _lanternModel;

    public override void _Ready()
    {
        _fuel = GetNode<Slot>("Fuel");
        _lanternModel = GetNode<AnimationTree>("SubViewportContainer/SubViewport/Camera3D/default/AnimationTree");
    }

    public override void _Process(double delta)
    {
    }

    public bool Fueled()
    {
        return _fuel.Amount() > 0;
    }
}