using Godot;
using System;

public partial class BaseHatch : Node3D, IStation
{
    public override void _Ready()
    {
        RandomNumberGenerator temp = new RandomNumberGenerator();
        GlobalPosition = new Vector3(temp.RandiRange(-10, 10), 1, temp.RandiRange(-10, 10));
    }

    public void DisplayUI(bool visible)
    {
        throw new NotImplementedException();
    }

    public void Use(Player player)
    {
        player.EnterBase();
    }
}