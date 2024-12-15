using Godot;

public partial class PlayerCamera : Node3D
{
    private AnimationPlayer _animationPlayer;

    [Export(PropertyHint.Range, "0,1,0.05")]
    private float _bobbingWeight = 1.0f;

    [Export]
    private bool WorldEnvironmentActive = false;

    [Export]
    private bool Debug_FullLight = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("Camera3D/AnimationPlayer");
        _animationPlayer.Play("player_headBob");

        if (WorldEnvironmentActive == false)
            GetNode<WorldEnvironment>("WorldEnvironment").Environment.FogDensity = 0;

        GetNode<DirectionalLight3D>("Sun").Visible = Debug_FullLight;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SetSpeed(float speed)
    {
        _animationPlayer.SpeedScale = speed * _bobbingWeight;
    }
}