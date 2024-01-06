using Godot;
using System;
using System.Collections;


public partial class Miner : Node3D
{
	[Export(PropertyHint.Range, "0,100,.1")]
	public int endurance;
	[Export(PropertyHint.Range, "0,100,.1")]
    public int maxEndurance;


	[Export(PropertyHint.Range, "0,5,.1")]
    public int _Size;

	public int _X;
    public int _Y;


    protected RandomNumberGenerator rng;


	public async void KickStart(Node3D[,] walls,int _Width, int _Height)
    {
        await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);
 
        if (Move(walls, _Width, _Height))
        {
            if (WeakRef(walls[_X,_Y]) != null)
            {
                walls[_X,_Y].QueueFree();
                walls[_X,_Y] = null;
            }

            KickStart(walls,_Width,_Height);
        }    
    }

    
    public bool Move(Node3D[,] walls,int _Width,int _Height)
    {

        var direction = rng.RandiRange(0,3);

        if (direction == 0 &&  _X +  _Size < _Width -1)
        {
             _X +=  _Size;
        }
        else if (direction == 1 &&  _X -  _Size > 0)
        {
             _X -=  _Size;
        }
        else if (direction == 2 &&  _Y +  _Size < _Height -1)
        {
             _Y +=  _Size;
        }
        else if (direction == 3 &&  _Y -  _Size > 0)
        {
            _Y -=  _Size;
        }

         endurance -= 1;
        return  endurance > 0;
    }
}
