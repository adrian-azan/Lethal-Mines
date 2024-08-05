using Godot;
using System;

public partial class Block : Node3D
{
    private float _health = 100f;
    private float _defense = 1f; //this shuld mabe be an enum

    private Vector3 _originalScale; //Allows the block to shrink but then go back to its original size when it heals
    private MeshInstance3D _mesh;

    private const String PATH_MESH = "Static_Body/MeshInstance3D";

    public override void _Ready()
    {
        _originalScale = Scale;
        _mesh = GetNode<MeshInstance3D>(PATH_MESH);
    }

    public void TakeDamage(float damage)
    {
        float felta = (float)GetPhysicsProcessDeltaTime();

        if (_health > 25)
        {
            _health -= Mathf.Clamp(damage - _defense, 0, 9999) * felta;

            var scalePercentage = _health / 100;
            _mesh.Scale = Vector3.One * scalePercentage;
        }
        else
        {
            var drop = GD.Load<PackedScene>(ScenePaths.DROP).Instantiate() as Drop;
            GetTree().Root.AddChild(drop);  //Add drop to root scene

            drop.SetObject(this);
            drop.Position = (GetNode("Static_Body/StaticBody3D") as StaticBody3D).Position;

            //Blocks Static_body is no longer needed
            GetNode<StaticBody3D>("Static_Body/StaticBody3D").QueueFree();
        }
    }
}