using Godot;
using System;

public abstract partial class Item : Node3D
{
    public PackedScene _packedScene;

    public String _name;
    public String _description;

    public Sprite2D _icon;

    public abstract void Use(Player player);
}