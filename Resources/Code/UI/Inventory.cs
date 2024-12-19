using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Inventory : Control
{
    private Array<Slot> _backpack;
    private HotBar _hotBar;
    private Lantern _lantern;

    private Dictionary<string, Item> _equipment;

    public override void _Ready()
    {
        _backpack = Variant.From(GetNode("GridContainer").GetChildren()).AsGodotArray<Slot>();
        _hotBar = GetParent().GetNode("HotBar") as HotBar;
        _equipment = new Dictionary<string, Item>();
        _lantern = GetNode<Lantern>("Lantern");
    }

    public bool AddItem(string newItem)
    {
        var itemName = Paths.GetNameFromScenePath(newItem);

        var physicalItemPath = Paths.Items.Objects.FindByName[itemName.ToUpper()];

        Slot potentialSlot;

        potentialSlot = FindStackableSlot(itemName);

        if (potentialSlot != null)
        {
            potentialSlot.AddItem(newItem);
            AddToEquipment(newItem, physicalItemPath);

            return true;
        }

        potentialSlot = FindEmptySlot(itemName);
        if (potentialSlot != null)
        {
            potentialSlot.AddItem(newItem);
            AddToEquipment(newItem, physicalItemPath);

            return true;
        }

        return false;
    }

    private void AddToEquipment(string newItem, string physicalItemPath)
    {
        if (!_equipment.ContainsKey(newItem) && ResourceLoader.Exists(physicalItemPath))
        {
            try
            {
                var physicalItem = (ResourceLoader.Load(physicalItemPath) as PackedScene).Instantiate() as Item;

                if (physicalItem != null)
                {
                    AddChild(physicalItem);
                    physicalItem.GlobalPosition = new Vector3(0, -40, 0);
                    _equipment.Add(newItem, physicalItem);
                }
            }
            catch (Exception ex)
            {
                GD.PushError(String.Format("Failed to add {0} to equipment", Paths.GetNameFromScenePath(newItem)));
            }
        }
    }

    private Slot FindStackableSlot(string itemName)
    {
        foreach (var slot in _backpack)
        {
            if (!slot.IsEmpty() && (itemName.Contains(slot._item.GetName()) && slot._item._stackable))
            {
                return slot;
            }
        }
        return null;
    }

    private Slot FindEmptySlot(string itemName)
    {
        foreach (var slot in _backpack)
        {
            if (slot.IsEmpty() && slot.WhiteListed(itemName))
            {
                return slot;
            }
        }
        return null;
    }

    public bool LanternFueled()
    {
        return _lantern.Fueled();
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