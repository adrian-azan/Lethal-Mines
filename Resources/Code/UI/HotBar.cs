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
        var grid = GetChild(0);

        _slots = Variant.From(grid.GetChildren()).AsGodotArray<Slot>();

        _selected = 0;
        _size = 4;

        _slots[_selected].Highlight();

        //Example of having an item in a slot. should be removed when implementing actual items
        _slots[0]._item = new Pickaxe();
        _slots[0]._item._packedScene = ResourceLoader.Load<PackedScene>("res://Resources/Scenes/Items/Pickaxe.tscn");
        var item = _slots[0]._item._packedScene.Instantiate<Pickaxe>();
        AddChild(item);
        _slots[0]._content.Texture = item._icon.Texture;
    }

    public void Use(Player player)
    {
        if (_slots[_selected]._item == null)
            return;

        (_slots[_selected]._item as Item).Use(player);
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