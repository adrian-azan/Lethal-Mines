using Godot;
using System;

public partial class Block : Node3D
{
    private float _health = 100f;
    public float _defense = 1f; //this shuld mabe be an enum

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void TakeDamage(float damage, float felta)
    {
        _health -= Mathf.Clamp(damage - _defense, 0, 9999) * felta;

        var scalePercentage = _health / 100;

        Scale = new Vector3(Scale.X * scalePercentage, Scale.Y * scalePercentage, Scale.Z * scalePercentage);

        if (_health <= 0)
        {
        }
    }

    public String ToString()
    {
        return $"Block: {_health}";
    }
}