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

    [ExportGroup("Blocks")] 
    [Export]
    public PackedScene wall;
    [Export]
    public PackedScene floor;
    
    protected RandomNumberGenerator rng;

    protected int[,] _Map;
    protected Node3D[,] _Walls;

    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        _Map = new int[_Width,_Height];
        _Walls = new Node3D[_Width,_Height];
    }

     public void FinalizeWorld()
    {
        var centerX = _Width / 2;
        var centerY = _Height / 2;
        for (int i = 0; i < _Width;i ++)
        {
            for (int j = 0; j < _Height; j++)
            {               
                if (_Map[i,j] != 0)
                { 
                    Node3D piece = null;
                    if (distanceFromCenter(i,j) < 60)
                        piece = wall.Instantiate<Node3D>();
                        
                    else
                        piece = wall.Instantiate<Node3D>();


                   
                    piece.Position = Position + new Vector3(i-centerX,1,j-centerY);                
                    AddChild(piece);
                    _Walls[i,j] = piece;
                }
            }
        }

        var mainFloor = floor.Instantiate<Node3D>();
        mainFloor.Scale = new Vector3(_Width,1,_Height);
        AddChild(mainFloor);        
    }

       

    private float distanceFromCenter(int x, int y)
    {
        var centerX = _Width;
        var centerY = _Height;

        var distanceX = Mathf.Abs(centerX-x);
        var distanceY = Mathf.Abs(centerY-y);

        return Mathf.Sqrt(Mathf.Pow(distanceX,2) + Mathf.Pow(distanceY,2));

    }

}
