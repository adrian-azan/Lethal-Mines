using Godot;
using System;

public partial class Slot : Control
{
    public Item_UI _item;
    private Sprite2D _border;

    private PlayerMouse _playerMouse;

    public override void _Ready()
    {
        _item = null;
        _border = GetNode<Sprite2D>("Border");

        var player = GetTree().Root.GetNode("Root/Player");
        _playerMouse = player.GetNode("UI/PlayerMouse") as PlayerMouse;
    }

    public override void _Process(double delta)
    {
        if (_item != null)
        {
            _item.Show();
            _item.Position = new Vector2(50, 50);
        }
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.ButtonMask == MouseButtonMask.Left)
            {
                _playerMouse.Swap(this);
            }
        }
    }

    public void AddItem(string newItem)
    {
        if (_item != null && _item._stackable)
        {
            _item++;
        }
        else
        {
            _item?.QueueFree();
            _item = (ResourceLoader.Load(newItem) as PackedScene).Instantiate() as Item_UI;

            if (_item == null)
                GD.PushError("Failed to Instantiate: " + newItem);
            else
            {
                _item.Position = new Vector2(50, 50);
                AddChild(_item);
            }
        }
    }

    public void DeHighlight()
    {
        _border.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot.png") as Texture2D;
    }

    public void Highlight()
    {
        _border.Texture = ResourceLoader.Load("res://Resources/Art/UI/Slot-Highlighted.png") as Texture2D;
    }

    public bool IsEmpty()
    {
        return _item == null;
    }
}