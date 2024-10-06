using Godot;
using Godot.Collections;
using System;

public partial class Furnace : Node3D, IConsumable, IStation
{
    private FurnaceUI _UI;

    public override void _Ready()
    {
        _UI = GetNode<FurnaceUI>("Furnace");
    }

    public void Use(Player player)
    {
        if (player._State != PlayerState.Normal)
            return;

        _UI.Visible = true;
        player._inventory.Visible = true;
        player._State = PlayerState.Furnace;
    }

    public void SaveContent(Dictionary<string, Item_UI> content)
    {
    }

    void IStation.DisplayUI(bool visible)
    {
        _UI.Visible = visible;
    }
}