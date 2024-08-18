using Godot;
using System;

public partial class CraftingTable : Item
{
    public override void Use(Player player)
    {
        GD.Print("Using Crafting Table");
    }
}