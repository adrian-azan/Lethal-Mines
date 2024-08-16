using Godot;
using System;
using System.Linq;

public partial class PlayerMouse : Node2D
{
    public Item_UI _item;

    public override void _Ready()
    {
        _item = GetNode<Item_UI>("Item-UI");
    }

    public override void _Process(double delta)
    {
        if (_item != null)
        {
            Position = GetGlobalMousePosition();
            _item.Position = Vector2.Zero;
        }
    }

    public void Swap(Slot incomingSlot)
    {
        //Reparent
        var incomingItem = incomingSlot._item;
        incomingItem?.Reparent(this);
        _item?.Reparent(incomingSlot);

        //change addresses
        var temp = incomingSlot._item;
        incomingSlot._item = _item;
        _item = temp;
    }
}