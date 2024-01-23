using Godot;
using System;
using System.Collections;

public partial class MinerGenerator : MapGenerator
{
    [ExportCategory("Miner Stats")]
    [Export]
    private int endurance;
    [Export]
    private int maxEndurance;
    [Export]
    private int _Size;

    private int _X;
    private int _Y;

    
    public override void _Ready()
    {
        base._Ready();
        Seed();

        _Size = 1;
        _X = _Width/2;
        _Y = _Height/2;
        maxEndurance = 100;
        endurance = maxEndurance;

        Seed();
        base.FinalizeWorld();

        nextStep();

     
    }

    public void _Process(float delta)
    {
       
    }

    public async void nextStep()
    {
        await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);

        if (Move());
        {
            if (WeakRef(_Walls[_X,_Y]) != null)
            {
                _Walls[_X,_Y].QueueFree();
                _Walls[_X,_Y] = null;
            }
            nextStep();
        }

        

    }




    public void Seed()
    {
        for (int i = 0; i < _Width-1; i++)
        {
            for (int j = 0; j < _Height-1; j++)
            {
                _Map[i,j] = 1;
            }
        }
    }

    public void Reset()
    {
        endurance = maxEndurance;
    }

    public bool Move()
    {
        var direction = rng.RandiRange(0,3);

        if (direction == 0 && _X + _Size < _Width -1)
        {
            _X += _Size;
        }
        else if (direction == 1 && _X - _Size > 0)
        {
            _X -= _Size;
        }
        else if (direction == 2 && _Y + _Size < _Height -1)
        {
            _Y += _Size;
        }
        else if (direction == 3 && _Y - _Size > 0)
        {
            _Y -= _Size;
        }

        endurance -= 1;
        return endurance > 0;
    }

}
