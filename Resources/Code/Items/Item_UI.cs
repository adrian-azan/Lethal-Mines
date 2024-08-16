using Godot;
using System;
using System.Data.Common;

public partial class Item_UI : Node2D
{
    public bool _stackable;
    public Sprite2D _sprite;
    public RichTextLabel _amount;

    [Export]
    public string _name;

    [Export]
    public string _description;

    public override void _Ready()
    {
        _stackable = GetMeta("stackable").As<bool>();
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

        item._amount.Text = item._amount.GetMeta("amount").AsString();

        return item;
    }

    public void Use(Player player)
    {
    }
}