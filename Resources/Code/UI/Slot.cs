using Godot;
using System;

public partial class Slot : Control
{
    public Item _item;
    private Sprite2D _border;
    public Sprite2D _content;

    private PlayerMouse _playerMouse;

    public override void _Ready()
    {
        _item = null;
        _border = GetNode<Sprite2D>("Border");
        _content = GetNode<Sprite2D>("Content");

        var player = Tools.GetRoot<Player>(this) as Player;
        _playerMouse = player.GetNode("UI/PlayerMouse") as PlayerMouse;
    }

    public override void _Process(double delta)
    {
        if (_item != null)
            _content.Texture = _item._icon.Texture;
        else
            _content.Texture = null;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.ButtonMask == MouseButtonMask.Left)
            {
                _playerMouse.Swap(ref _item);
            }
        }
    }

    public void AddItem(Item newItem)
    {
        _item = newItem;
        _content.Texture = _item._icon.Texture;

        AddSibling(_item._packedScene.Instantiate());
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