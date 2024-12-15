using Godot;
using System;

public partial class ViewPortIcon : SubViewportContainer
{
    public override void _Ready()
    {
        /* Ok this might look odd but I swear there's a reason
         * Since Items in inventory are 3D rendered through a viewport
         * they NEED to exist somewhere in the game. So I spawn them in, under the map
         * and randomly in a 10,000x10,000 grid. Idk if this affects performance, will
         * need to keep in mind as I add more items. OOO, just thought now, maybe I'll have all icons
         * render the same object through some sort of manager *shrug*
         */
        RandomNumberGenerator rng = new RandomNumberGenerator();
        var physicalItemPos = GetNode<Camera3D>("SubViewport/Camera3D");
        if (physicalItemPos != null)
            physicalItemPos.GlobalPosition = new Vector3(rng.RandiRange(-500, 500), -30, rng.RandiRange(-500, 500));
    }
}