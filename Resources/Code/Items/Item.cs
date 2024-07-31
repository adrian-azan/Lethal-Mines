using Godot;
using System;

public abstract partial class Item : Node3D
{
    public PackedScene _packedScene;
    public Sprite2D _icon;

    public String _name;
    public String _description;

    public abstract void Use(Player player);

    public abstract void Load();
}