using Godot;
using Godot.Collections;
using System;

public partial class MapGenerator : Node3D
{
    [ExportGroup("Map Dimensions")]
    [Export]
    public int _Width;

    [Export]
    public int _Height;

    [Export]
    public int _CoreCenterX;

    [Export]
    public int _CoreCenterY;

    [Export]
    public int _CoreCenterRadius;

    private Dictionary<BlockType, PackedScene> PackedScenes;

    private PackedScene _layerOne;

    private PackedScene _layerTwo;

    private PackedScene _layerThree;

    private PackedScene _coal;
    private PackedScene _iron;

    private PackedScene floor;

    protected RandomNumberGenerator rng;

    protected int[,] _Map;
    protected Node3D[,] _Walls;

    protected enum BlockType
    {
        Air,
        Stone,
        Coal,
        Iron,
        Copper,
        Dirt,
        Clay
    }

    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        _Map = new int[_Width, _Height];
        floor = ResourceLoader.Load<PackedScene>(Paths.Environment.FLOOR);

        PackedScenes = new Dictionary<BlockType, PackedScene>();

        PackedScenes.Add(BlockType.Stone, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.STONE));
        PackedScenes.Add(BlockType.Coal, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.COAL));
        PackedScenes.Add(BlockType.Iron, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.IRON));
        PackedScenes.Add(BlockType.Copper, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.COPPER));
        PackedScenes.Add(BlockType.Dirt, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.DIRT));
        PackedScenes.Add(BlockType.Clay, ResourceLoader.Load<PackedScene>(Paths.Items.Objects.CLAY));
    }

    public void FinalizeWorld()
    {
        _Walls = new Node3D[_Width, _Height];

        var centerX = _Width / 2 + Position.X;
        var centerY = _Height / 2 + Position.Z;

        var file = FileAccess.Open("MAP DEBUG.txt", FileAccess.ModeFlags.Write);
        String output = "";
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                output += _Map[i, j];
            }
            output += "\n";
        }
        file.StoreString(output);

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                PackedScene packedScene = null;
                Node3D piece = null;

                PackedScenes.TryGetValue((BlockType)_Map[i, j], out packedScene);

                piece = packedScene?.Instantiate() as Node3D;

                if (piece != null)
                {
                    piece.Position = Position + new Vector3(i - centerX, 2, j - centerY);
                    AddChild(piece);
                    _Walls[i, j] = piece;
                }
            }
        }

        var mainFloor = floor.Instantiate<Node3D>();
        mainFloor.Scale = new Vector3(_Width, 1, _Height);
        AddChild(mainFloor);
    }
}