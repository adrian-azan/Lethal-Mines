using Godot;

public partial class Block : Node3D
{
    protected float _health = 100f;
    protected float _defense = 1f; //this shuld mabe be an enum

    protected Vector3 _originalScale; //Allows the block to shrink but then go back to its original size when it heals
    public MeshInstance3D _mesh;

    public override void _Ready()
    {
        _mesh = GetNode<MeshInstance3D>("Static_Body/MeshInstance3D");
        _originalScale = _mesh.Scale;
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
            QueueFree();
        }
    }
}