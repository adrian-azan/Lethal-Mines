using Godot;
using Godot.Collections;

using System.Linq;

public partial class Inventory : Control
{
    private Array<Slot> _backpack;
    private HotBar _hotBar;

    private Dictionary<string, Item> _equipment;

    public override void _Ready()
    {
        _backpack = Variant.From(GetNode("GridContainer").GetChildren()).AsGodotArray<Slot>();
        _hotBar = GetParent().GetNode("HotBar") as HotBar;
        _equipment = new Dictionary<string, Item>();
    }

    public bool AddItem(string newItem)
    {
        var parts = newItem.Split();
        string itemName = parts[parts.Length - 1];

        var physicalItemPath = newItem.Replace("UI_Data", "Objects");

        foreach (var slot in _backpack)
        {
            if (slot.IsEmpty() || (itemName.Contains(slot._item._name) && slot._item._stackable))
            {
                slot.AddItem(newItem);

                if (!_equipment.ContainsKey(newItem))
                {
                    var physicalItem = (ResourceLoader.Load(physicalItemPath) as PackedScene).Instantiate() as Item;
                    _equipment.Add(newItem, physicalItem);
                }

                return true;
            }
        }

        return false;
    }

    public void Use(Player player)
    {
        var selectedItem = _hotBar.Use();

        if (_equipment.ContainsKey(selectedItem))
        {
            var physicalItem = _equipment[selectedItem];
            physicalItem.Use(player);
        }
    }
}