using Godot;
using Godot.Collections;

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
    }

    public override void _Process(double delta)
    {
    }

    public string Use()
    {
        if (_slots[_selected]._item == null)
            return "";

        return _slots[_selected]._item.SceneFilePath;
    }

    public void Drop()
    {
        //if (_slots[_selected]._item == null)
        //    return;

        //var drop = GD.Load<PackedScene>(Paths.Scenes.DROP).Instantiate() as Drop;
        //GetTree().Root.AddChild(drop);

        //drop.SetObject(_slots[_selected]._item);

        //var player = Tools.GetRoot<Player>(this) as Player;
        //var pos = player.GetPosition();
        //pos.Y = .5f;

        //drop.Position = pos;
        //drop.Rotation = player.GetRotation();

        //_slots[_selected]._item = null;
    }

    public static HotBar operator ++(HotBar target)
    {
        (target._slots[target._selected] as Slot).DeHighlight();

        if ((target._slots[target._selected] as Slot)._item != null)
            (target._slots[target._selected] as Slot)._item.Visible = false;

        target._selected += 1;

        if (target._selected >= target._size)
        {
            target._selected = 0;
        }

        (target._slots[target._selected] as Slot).Highlight();
        if ((target._slots[target._selected] as Slot)._item != null)
            (target._slots[target._selected] as Slot)._item.Visible = true;

        return target;
    }

    public static HotBar operator --(HotBar target)
    {
        (target._slots[target._selected] as Slot).DeHighlight();

        if ((target._slots[target._selected] as Slot)._item != null)
            (target._slots[target._selected] as Slot)._item.Visible = false;

        target._selected -= 1;

        if (target._selected < 0)
        {
            target._selected = target._size - 1; ;
        }

        (target._slots[target._selected] as Slot).Highlight();

        if ((target._slots[target._selected] as Slot)._item != null)
            (target._slots[target._selected] as Slot)._item.Visible = true;

        return target;
    }
}