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
            Ore possibleOre = Tools.GetRoot<Ore>(frontRayCast.GetCollider() as Node3D) as Ore;

            if (possibleOre is Ore)
            {
                possibleOre.TakeDamage(50);
                return;
            }

            Block possibleBlock = Tools.GetRoot<Block>(frontRayCast.GetCollider() as Node3D) as Block;

            if (possibleBlock is Block)
            {
                possibleBlock.TakeDamage(50);
                return;
            }
        }
    }
}