using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class HotBar : Control
{
    private int _selected;
    private int _size;

    private Array<Node> _slots;

    public override void _Ready()
    {
        var grid = GetChild(0);

        _slots = grid.GetChildren();

        _selected = 0;
        _size = 4;

        (_slots[_selected] as Slot).Highlight();
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