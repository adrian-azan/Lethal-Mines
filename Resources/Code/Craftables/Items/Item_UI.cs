using Godot;
using System;
using System.Data.Common;

public partial class Item_UI : Node2D
{
    public Sprite2D _sprite;
    public RichTextLabel _amount;

    [Export]
    public string _name;

    [Export]
    public string _description;

    [Export]
    public bool _stackable;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Icon");
        _amount = GetNode<RichTextLabel>("Amount");
        _amount.SetMeta("amount", 1);

        if (!_stackable)
            _amount.Visible = false;

        _name = GetName();
    }

    public override void _Process(double delta)
    {
        if (_stackable)
        {
            _amount.Show();
        }
        else
            _amount.Hide();
    }

    public void Clear()
    {
        _sprite.Texture = null;
        _amount.SetMeta("amount", 1);
        _stackable = false;
        _name = "";
    }

    public static Item_UI operator ++(Item_UI item)
    {
        int amount = item._amount.GetMeta("amount").AsInt32();
        item._amount.SetMeta("amount", amount + 1);

        item._amount.Text = item._amount.GetMeta("amount").AsString();

        return item;
    }

    public static Item_UI operator --(Item_UI item)
    {
        int amount = item._amount.GetMeta("amount").AsInt32();
        item._amount.SetMeta("amount", amount - 1);

        item._amount.Text = item._amount.GetMeta("amount").AsString();

        return item;
    }

    public static Item_UI operator +(Item_UI item, int quantity)
    {
        int amount = item._amount.GetMeta("amount").AsInt32();
        item._amount.SetMeta("amount", amount + quantity);

        item._amount.Text = item._amount.GetMeta("amount").AsString();

        return item;
    }

    //TODO: Possibly override the comparison operator
    //

    public int Amount()
    {
        return _amount.GetMeta("amount").AsInt32();
    }

    public void Use(Player player)
    {
    }

    public string GetName()
    {
        return _name == null ? "Name Not Implemented" : Name;
    }
}