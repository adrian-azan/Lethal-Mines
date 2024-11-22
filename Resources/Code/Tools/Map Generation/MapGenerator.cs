using Godot;
using System;
using System.Linq;

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

    private PackedScene _layerOne;

    private PackedScene _layerTwo;

    private PackedScene _layerThree;

    private PackedScene _coal;

    private PackedScene floor;

    protected RandomNumberGenerator rng;

    protected int[,] _Map;
    protected Node3D[,] _Walls;

    protected enum BlockType
    {
        Air,
        Stone,
        Coal,
        Dirt,
        Clay
    }

    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        _Map = new int[_Width, _Height];
        floor = ResourceLoader.Load<PackedScene>(Paths.Environment.FLOOR);

        _layerOne = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.STONE);
        _layerTwo = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.CLAY);
        _layerThree = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.DIRT);

        _coal = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.COAL);
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
                if (_Map[i, j] != (int)BlockType.Coal)
                {
                    Node3D piece = null;
                    if (_Map[i, j] == (int)BlockType.Stone)
                        piece = _layerOne.Instantiate<Node3D>();
                    else if (_Map[i, j] == (int)BlockType.Clay)
                        piece = _layerTwo.Instantiate<Node3D>();
                    else if (_Map[i, j] == (int)BlockType.Dirt)
                        piece = _layerThree.Instantiate<Node3D>();

                    if (piece != null)
                    {
                        piece.Position = Position + new Vector3(i - centerX, 2, j - centerY);
                        AddChild(piece);
                        _Walls[i, j] = piece;
                    }
                }
                else if (_Map[i, j] == (int)BlockType.Coal)
                {
                    Node3D piece = _coal.Instantiate<Node3D>();

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