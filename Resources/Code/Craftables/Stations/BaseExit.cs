using Godot;
using System;

public partial class BaseExit : Node3D, IStation
{
    void IStation.DisplayUI(bool visible)
    {
        throw new NotImplementedException();
    }

    void IStation.Use(Player player)
    {
        player.ExitBase();
    }
}