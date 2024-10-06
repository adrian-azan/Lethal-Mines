using Godot;
using Godot.Collections;

public partial class SceneManager : Node
{
    private Array packedScenes;

    public override void _Ready()
    {
        ResourceLoader.Load<PackedScene>("res://Resources/Scenes/Environment/SmallMap.tscn");
    }
}