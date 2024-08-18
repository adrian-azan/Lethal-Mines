using Godot;
using System;

public abstract partial class Item : Node3D
{
    public PackedScene _packedScene;

    public MeshInstance3D _mesh;

    public abstract void Use(Player player);
}