using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Block : Item
{
    private float _health = 100f;
    private float _defense = 1f; //this shuld mabe be an enum

    private Vector3 _originalScale; //Allows the block to shrink but then go back to its original size when it heals

    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("Static_Body/MeshInstance3D");
        _originalScale = _mesh.Scale;

        _packedScene = GD.Load<PackedScene>(Paths.Items.Objects.COAL);
    }

    //TODO: Don't love that this still doesnt have any functionality. Wondering
    // if block can just be a Node3D
    public override void Use(Player player)
    {
    }

    public void TakeDamage(float damage)
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