using Godot;
using System;

public partial class Slot : Control
{
    public PackedScene _item;

    private Sprite2D _border;
    public Sprite2D _content;

    public override void _Ready()
    {
        _border = GetNode<Sprite2D>("Border");
        _content = GetNode<Sprite2D>("Content");
    }

    public void DeHighlight()
    {
        _border.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot.png") as Texture2D;
    }

    public void Highlight()
    {
        _border.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot-Highlighted.png") as Texture2D;
    }
}