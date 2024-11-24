using Godot;

public partial class Block : Node3D
{
    protected float _health = 100f;
    protected float _defense = 1f; //this shuld mabe be an enum

    protected Vector3 _originalScale; //Allows the block to shrink but then go back to its original size when it heals
    public MeshInstance3D _mesh;

    public override void _Ready()
    {
        try
        {
            //Meshes imported from crocotile are called default and mesh is called Name of the object
            //If I figure out how to change that this will change too
            _mesh = GetNode<Node>("Static_Body/default")?.GetChild(0) as MeshInstance3D;
            if (_mesh == null) { return; }
            _originalScale = _mesh.Scale;
        }
        catch
        {
            GD.PushError("FAILED: " + this.Name + " Ready()");
        }
    }

    public void TakeDamage(float damage)
    {
        if (_mesh == null) { return; }
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