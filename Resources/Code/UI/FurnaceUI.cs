using Godot;
using Godot.Collections;
using System;

public partial class FurnaceUI : Control
{
    private Slot _input;
    private Slot _output;
    private Slot _fuel;

    public override void _Ready()
    {
        _input = GetNode<Slot>("Input");
        _output = GetNode<Slot>("Output");
        _fuel = GetNode<Slot>("Fuel");
    }
}