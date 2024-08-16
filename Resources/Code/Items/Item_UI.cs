using Godot;
using System;
using System.Data.Common;

public partial class Item_UI : Node2D
{
    public bool _stackable;
    public Sprite2D _sprite;
    public RichTextLabel _amount;

    public string _name;
    public string _description;

    public override void _Ready()
    {
        _name = "NA";
        _description = "NA";

        _stackable = false;
        _sprite = GetNode<Sprite2D>("Icon");
        _amount = GetNode<RichTextLabel>("Amount");
    }

    public override void _Process(double delta)
    {
        if (_stackable)
        {
            _amount.Show();
        }
    }

    public static Item_UI operator ++(Item_UI item)
    {
        int amount = item._amount.GetMeta("amount").AsInt32();
        item._amount.SetMeta("amount", amount + 1);

        return item;
    }

    public void Use(Player player)
    {
    }
}