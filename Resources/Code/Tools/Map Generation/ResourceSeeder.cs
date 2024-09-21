using Godot;
using System;
using System.Collections;

public class ResourceSeeder
{
    public int _endurance;
    public int _maxEndurance;

    public int _X;
    public int _Y;

    public PackedScene _resource;

    protected RandomNumberGenerator rng;

    public ResourceSeeder(int x, int y, int endurance, int maxEndurance)
    {
        _X = x;
        _Y = y;
        _endurance = endurance;
        _maxEndurance = maxEndurance;

        rng = new RandomNumberGenerator();
    }

    public void Energize()
    {
        _endurance = _maxEndurance;
    }

    public void SetPos(int x, int y)
    {
        _X = x;
        _Y = y;
    }

    public bool Work(int _Width, int _Height)
    {
        var direction = rng.RandiRange(0, 3);

        if (direction == 0 && _X < _Width - 1)
        {
            _X += 1;
        }
        else if (direction == 1 && _X > 0)
        {
            _X -= 1;
        }
        else if (direction == 2 && _Y < _Height - 1)
        {
            _Y += 1;
        }
        else if (direction == 3 && _Y > 0)
        {
            _Y -= 1;
        }

        _endurance -= 1;
        return _endurance > 0;
    }
}