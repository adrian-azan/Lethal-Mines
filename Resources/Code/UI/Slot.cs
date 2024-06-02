using Godot;
using System;

public partial class Slot : Control
{
    private Item _item;
    private Boolean _full;
    private Sprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public void DeHighlight()
    {
        _sprite.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot.png") as Texture2D;
    }

    public void Highlight()
    {
        _sprite.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot-Highlighted.png") as Texture2D;
    }
}