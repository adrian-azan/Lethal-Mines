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
        var centerX = _Width / 2;
        var centerY = _Height / 2;
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (_Map[i, j] != 0 && _Map[i, j] != 2)
                {
                    Node3D piece = null;
                    if (Tools.distanceFromCenter(i, j, _Width, _Height) < 30)
                        piece = _layerOne.Instantiate<Node3D>();
                    else if (Tools.distanceFromCenter(i, j, _Width, _Height) < 60)
                        piece = _layerTwo.Instantiate<Node3D>();
                    else
                        piece = _layerThree.Instantiate<Node3D>();

                    piece.Position = Position + new Vector3(i - centerX, 2, j - centerY);
                    AddChild(piece);
                    _Walls[i, j] = piece;
                }
                else if (_Map[i, j] == 2)
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