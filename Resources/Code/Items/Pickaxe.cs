using Godot;
using System;

public partial class Pickaxe : Item
{
    public override void _Ready()
    {
        _name = "Pickaxe";
        _description = "Whack dirt";

        _icon = new Sprite2D();
        _icon.Texture = ResourceLoader.Load("res://Resources/Art/UI/pickaxe.png") as Texture2D;
    }

    public override void Use()
    {
        GD.Print("PICKING");
    }
}