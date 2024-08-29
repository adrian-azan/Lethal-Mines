using Godot;
using System;

public interface IStation
{
    public void Use(Player player);

    public void DisplayUI(bool visible);
}