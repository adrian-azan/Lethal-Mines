using Godot;
using System;

public partial class Lantern : Control
{
    Slot _fuel;

    public override void _Ready()
    {
        _fuel = GetNode<Slot>("Fuel");
    }

    public bool Fueled()
    {
        return _fuel.Amount() > 0;
    }
}