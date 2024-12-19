using Godot;
using System;

public partial class Drop : Node3D
{
    private MeshInstance3D _mesh;
    public const String PATH_MESH = "Static_Body/StaticBody3D/MeshInstance3D";

    public Node3D _theDrop;
    private Area3D _area;

    public void SetObject(Node3D newObject)
    {
        //Place newObject under Drops static_body
        newObject.Reparent(GetNode("Static_Body"));

        //Move drop to location of the new object;
        GetNode<StaticBody3D>("Static_Body/StaticBody3D").GlobalPosition = newObject.GlobalPosition;
        _theDrop = newObject;

        var timer = GetNode<Godot.Timer>("CoolDownTimer");

        timer.Timeout += CoolDown;
        timer.Start(2);
        timer.Autostart = false;

        _area = GetNode<Area3D>("Static_Body/StaticBody3D/Area3D");
    }

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
            string dropName = Paths.GetNameFromScenePath(_theDrop.SceneFilePath).ToUpper();
            if (test._inventory.AddItem(Paths.Items.UI_Data.FindByName[dropName]))
                QueueFree();
        }
    }

    private void CoolDown()
    {
        GetNode<Node3D>("Static_Body/StaticBody3D/Sparkle").Visible = true;
        _area.Monitoring = true;
    }
}