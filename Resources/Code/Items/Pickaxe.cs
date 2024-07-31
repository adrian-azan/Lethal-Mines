using Godot;
using System;

public partial class Pickaxe : Item
{
    public Pickaxe()
    {
        _name = "Pickaxe";
        _description = "Whack dirt";

        _icon = new Sprite2D();
        _icon.Texture = ResourceLoader.Load("res://Resources/Art/UI/pickaxe.png") as Texture2D;
        _packedScene = ResourceLoader.Load<PackedScene>("res://Resources/Scenes/Items/Pickaxe.tscn");
    }

    public override void Use(Player player)
    {
        var frontRayCast = player.RayCast();

        if (frontRayCast.IsColliding())
        {
            var other = Tools.GetRoot<Block>(frontRayCast.GetCollider() as Node3D) as Block;

            if (other is Block)
            {
                other.TakeDamage(50);
            }
        }
    }
}