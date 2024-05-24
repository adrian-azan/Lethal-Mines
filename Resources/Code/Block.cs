using Godot;
using System;

public partial class Block : Node3D
{
    private float _health = 100f;
    private float _defense = 1f; //this shuld mabe be an enum

    private Vector3 _originalScale; //Allows the block to shrink but then go back to its original size when it heals
    private MeshInstance3D _mesh;

    private const String PATH_MESH = "Static_Body/MeshInstance3D";
    private const String PATH_DROP = "res://Resources/Scenes/Drop.tscn";

    public override void _Ready()
    {
        _originalScale = Scale;
        _mesh = GetNode<MeshInstance3D>(PATH_MESH);
    }

    public void TakeDamage(float damage, float felta)
    {
        if (_health > 25)
        {
            _health -= Mathf.Clamp(damage - _defense, 0, 9999) * felta;

            var scalePercentage = _health / 100;
            _mesh.Scale = Vector3.One * scalePercentage;
        }
        else
        {
            QueueFree(); //Delete the block now that it has no health

            var drop = GD.Load<PackedScene>(PATH_DROP).Instantiate() as Drop;

            drop.Transform = Transform;     //Set drop to position of block
            drop.SetMesh(_mesh);            //Make drop look like block

            GetTree().Root.AddChild(drop);  //Add drop to root scene
        }
    }

    public override String ToString()
    {
        return $"Block: {_health}";
    }
}