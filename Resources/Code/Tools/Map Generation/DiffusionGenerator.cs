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

    [Export]
    public int generationLimit;

    [Export]
    public float noise;

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

        _resources = new List<ResourceDetails>();
        _resources.Add(new ResourceDetails(BlockType.Coal, .02f, 6));
        _resources.Add(new ResourceDetails(BlockType.Iron, .01f, 3));
        _resources.Add(new ResourceDetails(BlockType.Copper, .01f, 6));

        SeedNoise();
        BuildWorld();
        SeedBlocks();
        SeedResources();

        if (FlipX)
            _Map = MapStitcher.MirrorOnYAxis(_Map, _Height, ref _Width);

        if (FlipY)
            _Map = MapStitcher.MirrorOnXAxis(_Map, ref _Height, _Width);

        FinalizeWorld();
    }

    public void SeedNoise()
    {
        for (int i = 1; i < _Width - 1; i++)
        {
            for (int j = 1; j < _Height - 1; j++)
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

    public void BuildWorld()
    {
        building = generation < generationLimit;
        if (!building)
        {
            return;
        }

        int[,] next = new int[_Width, _Height];

        for (int i = 1; i < _Width - 1; i++)
        {
            for (int j = 1; j < _Height - 1; j++)
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

        BuildWorld();
    }

    public void SeedBlocks()
    {
        for (int x = 0; x < _Width; x++)
        {
            for (int z = 0; z < _Height; z++)
            {
                if (_Map[x, z] != (int)BlockType.Air)
                {
                    float distanceFromCenter = Tools.distanceFromPoint(x, z, _Width - _CoreCenterY, _CoreCenterX);
                    if (distanceFromCenter < (_CoreCenterRadius) * .4f)
                        _Map[x, z] = (int)BlockType.Stone;
                    else if (distanceFromCenter < (_CoreCenterRadius) * .8f)
                        _Map[x, z] = (int)BlockType.Clay;
                    else
                        _Map[x, z] = (int)BlockType.Dirt;
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
                seeder.SetPos(rng.RandiRange((int)(_Width * .1f), (int)(_Width * .9f)), rng.RandiRange((int)(_Height * .1f), (int)(_Height * .9f)));
                seeder.Energize();

                do
                {
                    _Map[Mathf.Clamp(seeder._X, 0, _Width - 1), Mathf.Clamp(seeder._Y, 0, _Height - 1)] = (int)currentResource.resource;
                } while (seeder.Work(_Width, _Height));
            }
        }
    }
}