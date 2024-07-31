using Godot;
using System;

public partial class PlayerMouse : Control
{
    public Item _item;
    public Sprite2D _content;

    public override void _Ready()
    {
        _content = GetNode<Sprite2D>("Content");
    }

    public override void _Process(double delta)
    {
        if (_item != null)
        {
            _content.Texture = _item._icon.Texture;
            _content.Position = GetGlobalMousePosition();
        }
        else
            _content.Texture = null;
    }

    public void Swap(ref Item incomingItem)
    {
        var temp = incomingItem;
        incomingItem = _item;
        _item = temp;
    }
}