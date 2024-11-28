using Godot;
using System;

public partial class Slot : Control
{
    public Item_UI _item;
    private Sprite2D _border;

    private PlayerMouse _playerMouse;

    [Export]
    private String? _whiteList;

    public override void _Ready()
    {
        _item = null;
        _border = GetNode<Sprite2D>("Border");

        var player = GetTree().CurrentScene.GetNode("Player");
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
                if (_playerMouse._item == null)
                    _playerMouse.Swap(this);
                else if (WhiteListed(_playerMouse.GetItemName()) == true)
                    _playerMouse.Swap(this);
            }
        }
    }

    public int Amount()
    {
        if (_item == null) { return 0; }
        return _item.Amount();
    }

    public void AddItem(string newItem, int quantity = 1)
    {
        if (WhiteListed(newItem) == false)
        { return; }

        if (_item != null && _item._stackable)
        {
            _item += quantity;
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

    public void RemoveItem(int quantity = 1)
    {
        if (_item == null) return;

        if (_item._stackable && _item.Amount() > 1)
        {
            _item--;
        }
        else
        {
            _item.QueueFree();
            _item = (ResourceLoader.Load("res://Resources/Scenes/Items/UI_Data/Item_UI.tscn") as PackedScene).Instantiate() as Item_UI;
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

    public string GetItemName()
    {
        return IsEmpty() ? "" : _item.GetName();
    }

    public bool IsEmpty()
    {
        return _item == null;
    }

    public bool WhiteListed(String itemName)
    {
        if (_whiteList == null)
            return true;

        if (itemName.Contains(_whiteList) == false)
            return false;
        return true;
    }
}