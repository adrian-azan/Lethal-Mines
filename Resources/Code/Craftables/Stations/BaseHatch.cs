using Godot;
using System;

public partial class BaseHatch : Node3D, IStation
{
    public override void _Ready()
    {
        RandomNumberGenerator temp = new RandomNumberGenerator();
        GlobalPosition = new Vector3(temp.RandiRange(-5, 5), 1, temp.RandiRange(-5, 5));
    }

    public void SetPosition(Vector3 pos)
    {
        GlobalPosition = pos;
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