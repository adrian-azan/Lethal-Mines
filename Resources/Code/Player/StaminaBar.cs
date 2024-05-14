using Godot;
using System;

public partial class StaminaBar : Control
{
    private ColorRect _background;
    
    private float _stamina = 50f;
	private float _staminaDrain = 60f;
	private float _staminaRecovery = 20f;
    public bool _exhausted = false;


    public override void _Ready()
    {
	    _background = GetNode<ColorRect>("Bar");
    }

    public void UpdateBar(float percentage)
    {
        //5 because thats 1/100th of the total size of the bar
        var total = 5 * percentage;

        _background.Scale = new Vector2(total, _background.Scale.Y);
    }

    public void Process(float felta)
    {
        if (_stamina <= 0)
		{
            _exhausted = true;
        }
        else if(_stamina >= 20)
		{
            _exhausted = false;
        }

		if(_stamina < 100)
		{
            _stamina += _staminaRecovery*felta;
        }

		UpdateBar(100-_stamina);
    }

    public void Drain(float felta)
    {
        _stamina -= _staminaDrain * felta;
    }
}
