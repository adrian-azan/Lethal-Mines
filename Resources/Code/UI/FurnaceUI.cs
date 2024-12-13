using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;

public partial class FurnaceUI : Control
{
    private Slot _input;
    private Slot _output;
    private Slot _fuel;

    private Array<String> _possibleFuels;
    private Array<String> _possibleInputs;

    private Dictionary<string, string> _possibleOutputs;

    private Timer _coolDown;

    public override void _Ready()
    {
        _input = GetNode<Slot>("Input");
        _output = GetNode<Slot>("Output");
        _fuel = GetNode<Slot>("Fuel");
        _coolDown = GetNode<Timer>("CoolDown");
        _coolDown.OneShot = true;

        _possibleInputs = new Godot.Collections.Array<String> { "Iron", "Copper" };
        _possibleFuels = new Godot.Collections.Array<String> { "Coal" };
        _possibleOutputs = new Dictionary<string, string> { { "Iron", Paths.Items.UI_Data.IRON_INGOT },
            {"Copper", Paths.Items.UI_Data.COPPER_INGOT } };
    }

    public override void _Process(double delta)
    {
        /*
         * Furnace is off cooldown
         * AND
         * Both fuel and input must have acceptable items stored
         * AND
         * Output must either be empty OR item currently in output is the item you'd get from smelting the input
         */
        if (_coolDown.IsStopped() && (_possibleInputs.Contains(_input.GetItemName()) && _possibleFuels.Contains(_fuel.GetItemName())
            && (_output._item == null || _possibleOutputs[_input.GetItemName()].Contains(_output.GetItemName()))))
        {
            _coolDown.Start();
            string output = _possibleOutputs[_input.GetItemName()];

            _input.RemoveItem();
            _fuel.RemoveItem();

            _output.AddItem(output);
        }
    }
}