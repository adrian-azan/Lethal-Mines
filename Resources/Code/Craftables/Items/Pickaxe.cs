using Godot;
using System;

public partial class Pickaxe : Item
{
    public Pickaxe()
    {
        _packedScene = ResourceLoader.Load<PackedScene>(Paths.Items.Objects.PICKAXE);
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