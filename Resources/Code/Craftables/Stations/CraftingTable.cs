using Godot;
using System;

public partial class CraftingTable : Node3D, IConsumable, IStation
{
    public void Use(Player player)
    {
        player._inventory.AddItem(Paths.Items.UI_Data.PICKAXE);
    }

    void IStation.DisplayUI(bool visible)
    {
        throw new NotImplementedException();
    }
}