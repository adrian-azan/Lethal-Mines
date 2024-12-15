using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Linq;

public partial class Lantern : Control
{
    private Slot _fuel;

    private Array<OmniLight3D> _lights;

    public override void _Ready()
    {
        _fuel = GetNode<Slot>("Fuel");
        _lights = Variant.From(GetNode<Node>("SubViewportContainer/SubViewport/Camera3D/Lights").GetChildren()).AsGodotArray<OmniLight3D>();
    }

    public void LightsOn(bool on = false)
    {
        foreach (var light in _lights)
        {
            light.Visible = on;
        }
    }

    public bool Fueled()
    {
        return _fuel.Amount() > 0;
    }
}