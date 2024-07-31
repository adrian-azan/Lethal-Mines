using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Linq;

public partial class HotBar : Control
{
    private int _selected;
    private int _size;

    private Array<Slot> _slots;

    public override void _Ready()
    {
        var grid = GetNode<GridContainer>("GridContainer");
        _slots = Variant.From(grid.GetChildren()).AsGodotArray<Slot>();

        _selected = 0;
        _size = _slots.Count;

        _slots[_selected].Highlight();
        _slots[_selected].AddItem(new Pickaxe());
    }

    public void Use(Player player)
    {
        if (_slots[_selected]._item == null)
            return;

        _slots[_selected]._item.Use(player);
    }

    public static HotBar operator ++(HotBar target)
    {
        (target._slots[target._selected] as Slot).DeHighlight();

        target._selected += 1;

        if (target._selected >= target._size)
        {
            target._selected = 0;
        }

        (target._slots[target._selected] as Slot).Highlight();

        return target;
    }

    public static HotBar operator --(HotBar target)
    {
        (target._slots[target._selected] as Slot).DeHighlight();

        target._selected -= 1;

        if (target._selected < 0)
        {
            target._selected = target._size - 1; ;
        }

        (target._slots[target._selected] as Slot).Highlight();

        return target;
    }
}