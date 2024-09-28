using Godot;
using System;
using System.Collections.Generic;

public partial class DiffusionGenerator : MapGenerator
{
    protected class ResourceDetails
    {
        public BlockType resource;
        public float resourceRarity;    //A percentage of how much if this resource should exist
        public int veinSize;         //Max size of how many resources should be in a vein

        public ResourceDetails(BlockType typeOfResource, float frequency, int size)
        {
            resource = typeOfResource;
            resourceRarity = frequency;
            veinSize = size;
        }
    }

    public int limit = 3;
    public float noise;

    [Export]
    public int generation = 0;

    public bool building;

    [Export]
    public bool FlipY;

    [Export]
    public bool FlipX;

    private List<ResourceDetails> _resources;

    public override void _Ready()
    {
        base._Ready();
        building = true;
        noise = .7f;

        _resources = new List<ResourceDetails>();
        _resources.Add(new ResourceDetails(BlockType.Coal, .05f, 5));

        SeedNoise();
        BuildWorld();
        SeedBlocks();
        SeedResources();

        if (FlipY)
            FlipOnYAxis();
        if (FlipX)
            FlipOnXAxis();

        FinalizeWorld();
    }

    public void SeedNoise()
    {
        for (int i = 1; i < _Height - 1; i++)
        {
            for (int j = 1; j < _Width - 1; j++)
            {
                float chance = (float)rng.RandfRange(0, 100) / 100;

                if (chance >= noise)
                {
                    _Map[i, j] = 0;
                }
                else
                {
                    _Map[i, j] = 1;
                }
            }
        }
    }

    public void SeedResources()
    {
        foreach (ResourceDetails currentResource in _resources)
        {
            int estimatedNumberOfSeedersRequired = (int)(_Width * _Height * currentResource.resourceRarity) / currentResource.veinSize;
            ResourceSeeder seeder = new ResourceSeeder(0, 0, currentResource.veinSize);

            for (int i = 0; i < estimatedNumberOfSeedersRequired; i++)
            {
                seeder.SetPos(rng.RandiRange(15, 130), rng.RandiRange(15, 130));
                seeder.Energize();

                do
                {
                    _Map[Mathf.Clamp(seeder._X, 0, _Width - 1), Mathf.Clamp(seeder._Y, 0, _Height - 1)] = (int)currentResource.resource;
                } while (seeder.Work(_Width, _Height));
            }
        }
    }

    public void SeedBlocks()
    {
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (_Map[i, j] != (int)BlockType.Air)
                {
                    if (Tools.distanceFromCenter(i, j, _Width * 2, _Height * 2) < _Width * .4f)
                        _Map[i, j] = (int)BlockType.Stone;
                    else if (Tools.distanceFromCenter(i, j, _Width * 2, _Height * 2) < _Width * .7f)
                        _Map[i, j] = (int)BlockType.Clay;
                    else
                        _Map[i, j] = (int)BlockType.Dirt;
                }
            }
        }
    }

    public void BuildWorld()
    {
        int[,] next = new int[_Width, _Height];

        for (int i = 1; i < _Height - 1; i++)
        {
            for (int j = 1; j < _Width - 1; j++)
            {
                var sumCell = 0;
                sumCell += _Map[i + 1, j];
                sumCell += _Map[i - 1, j];
                sumCell += _Map[i, j + 1];
                sumCell += _Map[i, j - 1];
                sumCell += _Map[i + 1, j + 1];
                sumCell += _Map[i + 1, j - 1];
                sumCell += _Map[i - 1, j + 1];
                sumCell += _Map[i - 1, j - 1];

                if (sumCell >= 6)
                    next[i, j] = 1;
                else if (sumCell == 3)
                    next[i, j] = _Map[i, j];
                else
                    next[i, j] = 0;
            }
        }
        _Map = next;
        generation++;

        building = generation < limit;
        if (!building)
        {
            return;
        }
        BuildWorld();
    }
}