using Godot;
using Godot.Collections;
using System;

public partial class FurnaceUI : Control
{
    private new Slot _Input;
    private Slot _Output;
    private Slot _Fuel;

    private Furnace _furnace;

    public override void _Ready()
    {
        _Input = GetNode<Slot>("Input");
        _Output = GetNode<Slot>("Output");
        _Fuel = GetNode<Slot>("Fuel");
        _furnace = null;
    }

    public void LoadContent(Dictionary<string, Item_UI> content, Furnace furnace)
    {
        _Input._item = content["Input"];
        _Output._item = content["Output"];
        _Fuel._item = content["Fuel"];

        _furnace = furnace;
    }

    public void SaveContent()
    {
        _furnace.SaveContent(new Dictionary<string, Item_UI> { { "Input", _Input._item }, { "Output", _Output._item }, { "Fuel", _Fuel._item } });

        _furnace = null;
    }
}