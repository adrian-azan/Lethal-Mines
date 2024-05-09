using Godot;
using System;

public partial class StaminaBar : Control
{
    private ColorRect _background;

    public override void _Ready()
	{
		_background = GetNode<ColorRect>("Bar");
	}

    public void Drain(float percentage)
    {
        //5 because thats 1/100th of the total size of the bar
        var total = 5 * percentage;

        _background.Scale = new Vector2(total, _background.Scale.Y);       
    }
}
