using Godot;
using System;

public static class MapStitcher
{
    public static int[,] MirrorOnYAxis(int[,] source, int height, ref int width)
    {
        width *= 2;

        int[,] mirrored = new int[width, height];

        for (int i = 0; i < width / 2; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mirrored[i, j] = source[i, j];
                mirrored[(width - 1) - i, j] = source[i, j];
            }
        }
        return mirrored;
    }

    public static int[,] MirrorOnXAxis(int[,] source, ref int height, int width)
    {
        height *= 2;

        int[,] mirrored = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height / 2; j++)
            {
                mirrored[i, j] = source[i, j];
                mirrored[i, (height - 1) - j] = source[i, j];
            }
        }
        return mirrored;
    }
}