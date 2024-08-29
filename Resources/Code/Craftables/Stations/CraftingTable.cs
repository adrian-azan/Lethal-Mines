using Godot;
using System;

public partial class CraftingTable : Item, IConsumable, IStation
{
    public override void Use(Player player)
    {
        player._inventory.AddItem(Paths.Items.UI_Data.PICKAXE);
    }

    void IStation.DisplayUI(bool visible)
    {
        throw new NotImplementedException();
    }
}