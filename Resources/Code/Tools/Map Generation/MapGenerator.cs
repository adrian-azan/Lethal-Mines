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

    public PackedScene _layerOne;

    public PackedScene _layerTwo;

    public PackedScene _layerThree;

    public PackedScene _coal;

    [Export]
    public PackedScene floor;

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
        _Walls = new Node3D[_Width, _Height];

        _layerOne = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.STONE);
        _layerTwo = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.CLAY);
        _layerThree = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.DIRT);

        _coal = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.COAL);
    }

    public void FinalizeWorld()
    {
        var centerX = _Width / 2 + Position.X;
        var centerY = _Height / 2 + Position.Z;

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (_Map[i, j] != (int)BlockType.Air && _Map[i, j] != (int)BlockType.Coal)
                {
                    Node3D piece = null;
                    if (_Map[i, j] == (int)BlockType.Stone)
                        piece = _layerOne.Instantiate<Node3D>();
                    else if (_Map[i, j] == (int)BlockType.Clay)
                        piece = _layerTwo.Instantiate<Node3D>();
                    else
                        piece = _layerThree.Instantiate<Node3D>();

                    piece.Position = Position + new Vector3(i - centerX, 2, j - centerY);
                    AddChild(piece);
                    _Walls[i, j] = piece;
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

    public void FlipOnYAxis()
    {
        int[,] _Fliped = new int[_Width, _Height];

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                _Fliped[(_Width - 1) - i, j] = _Map[i, j];
            }
        }

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                _Map[i, j] = _Fliped[i, j];
            }
        }
    }

    public void FlipOnXAxis()
    {
        int[,] _Fliped = new int[_Width, _Height];

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                _Fliped[i, (_Height - 1) - j] = _Map[i, j];
            }
        }

        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                _Map[i, j] = _Fliped[i, j];
            }
        }
    }
}