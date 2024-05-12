using Godot;
using System;

public partial class DiffusionGenerator : MapGenerator
{

    public int limit = 3;
    public float noise;
    [Export]
    public int generation = 0;
    public bool building;

	public override void _Ready()
    {
        base._Ready();
        building = true;
        noise = .7f;

        Seed();    
        BuildWorld();
    }

    

    public void Seed()
    {
        for (int i = 1; i < _Height-1;i ++)
        {
            for (int j = 1; j < _Width-1; j++)
            {   
                float chance = (float)rng.RandfRange(0,100)/100;
                
                if (chance >= noise)
                {
                    _Map[i,j] = 0;
                }
                else
                {
                    _Map[i,j] = 1;
                }
            }

        }
    }

    public void BuildWorld()
    {
        int[,] next = new int[_Width,_Height];

        for (int i = 1; i < _Height-1;i ++)
        {
            for (int j = 1; j < _Width-1; j++)
            {               
                var sumCell = 0;
                sumCell += _Map[i+1,j]; 
                sumCell += _Map[i-1,j]; 
                sumCell += _Map[i,j+1]; 
                sumCell += _Map[i,j-1]; 
                sumCell += _Map[i+1,j+1];  
                sumCell += _Map[i+1,j-1];  
                sumCell += _Map[i-1,j+1];  
                sumCell += _Map[i-1,j-1];             
                        
                if (sumCell >= 6)
                    next[i,j] = 1;
                else if (sumCell == 3)
                    next[i,j] = _Map[i,j];
                else
                    next[i,j] = 0;
            }


        } 
        _Map = next;        
        generation++;

        building = generation < limit;
        if (!building)
        {
            FinalizeWorld();
            return;
        }
        BuildWorld();
    }    


}
