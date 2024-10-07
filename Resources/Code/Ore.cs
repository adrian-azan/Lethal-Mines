using Godot;
using System;

public partial class Ore : Block
{
    public new void TakeDamage(float damage)
    {
        float felta = (float)GetPhysicsProcessDeltaTime();

        if (_health > 25)
        {
            _health -= Mathf.Clamp(damage - _defense, 0, 9999) * felta;

            var scalePercentage = _health / 100;
            _mesh.Scale = _originalScale * scalePercentage;
        }
        else
        {
            var drop = GD.Load<PackedScene>(Paths.Scenes.DROP).Instantiate() as Drop;
            GetTree().Root.AddChild(drop);  //Add drop to root scene

            drop.SetObject(this);

            //Blocks StaticBody3D is no longer needed
            GetNode<StaticBody3D>("Static_Body/StaticBody3D").QueueFree();
        }
    }
}