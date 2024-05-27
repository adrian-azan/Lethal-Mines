using Godot;
using System;

public partial class Drop : Node3D
{
    private MeshInstance3D _mesh;
    public const String PATH_MESH = "Static_Body/StaticBody3D/MeshInstance3D";

    public void SetMesh(MeshInstance3D mesh)
    {
        Scale = (Tools.GetRoot(mesh.GetParentNode3D()) as Node3D).Scale * .25f; //.25 because 25 is the min health of a block

        _mesh = GetNode<MeshInstance3D>(PATH_MESH);
        _mesh.Mesh = mesh.Mesh;
        _mesh.Mesh.SurfaceSetMaterial(0, mesh.Mesh.SurfaceGetMaterial(0));
    }

    public void Grabbed(Node3D other)
    {
        Player test = Tools.GetRoot<Player>(other) as Player;
        if (test is Player)
        {
            QueueFree();
        }
    }
}