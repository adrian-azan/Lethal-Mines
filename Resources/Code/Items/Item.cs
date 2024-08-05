using Godot;
using System;

public abstract partial class Item : Node3D
{
    public PackedScene _packedScene;
    public Sprite2D _icon;
    public MeshInstance3D _mesh;

    public String _name;
    public String _description;

    public abstract void Use(Player player);
}