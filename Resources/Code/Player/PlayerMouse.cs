using Godot;
using System;
using System.Linq;

public partial class PlayerMouse : Node2D
{
    public Item_UI _item;

    public override void _Ready()
    {
        _item = GetNodeOrNull<Item_UI>("Item-UI");
    }

    public override void _Process(double delta)
    {
        if (_item != null)
        {
            Position = GetGlobalMousePosition();
            _item.Position = Vector2.Zero;
        }
    }

    public String GetItemName()
    {
        return _item.GetName();
    }

    public void Swap(Slot incomingSlot)
    {
        /*
         * Both Mouse and Selected Slot have an item
         * AND
         * Items are identical and stackable
         *      Item will be added from mouse to selected slot
         */
        if (_item != null && incomingSlot._item != null && incomingSlot._item.GetName() == _item.GetName() && _item._stackable)
        {
            incomingSlot.AddItem(_item.GetName(), _item.Amount());
            _item.QueueFree();
            _item = null;
            return;
        }

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