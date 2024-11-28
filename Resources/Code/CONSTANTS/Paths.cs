using Godot;
using System;

public static class Paths
{
    public static string MakeScenePath(string sceneName)
    {
        return string.Format("res://Resources/Scenes/Items/UI_Data/{0}.tscn", sceneName);
    }

    public static string GetNameFromScenePath(string scenePath)
    {
        int start = scenePath.RFind("/") + 1;
        int end = scenePath.RFind(".");

        return scenePath.Substr(start, end - start);
    }

    public static class Scenes
    {
        public const string DROP = "res://Resources/Scenes/Items/Objects/Blocks/Drop.tscn";
    }

    public static class Items
    {
        public static class UI_Data
        {
            public const string PICKAXE = "res://Resources/Scenes/Items/UI_Data/Pickaxe.tscn";

            public const string STONE = "res://Resources/Scenes/Items/UI_Data/Stone.tscn";

            public const string BASE_HATCH = "res://Resources/Scenes/Items/UI_Data/BaseHatch.tscn";
            public const string COAL = "res://Resources/Scenes/Items/UI_Data/Blocks/Coal.tscn";
            public const string IRON = "res://Resources/Scenes/Items/UI_Data/Blocks/Iron.tscn";
        }

        public static class Objects
        {
            public const string PICKAXE = "res://Resources/Scenes/Items/Objects/Pickaxe.tscn";

            //BLOCKS
            public const string STONE = "res://Resources/Scenes/Items/Objects/Blocks/Stone.tscn";

            public const string COAL = "res://Resources/Scenes/Items/Objects/Blocks/Coal.tscn";
            public const string IRON = "res://Resources/Scenes/Items/Objects/Blocks/Iron.tscn";
            public const string CLAY = "res://Resources/Scenes/Items/Objects/Blocks/Clay.tscn";
            public const string DIRT = "res://Resources/Scenes/Items/Objects/Blocks/Dirt.tscn";
        }
    }

    public static class Environment
    {
        public const string FLOOR = "res://Resources/Scenes/Environment/Floor.tscn";
    }
}