using Godot;
using System;

public partial class WorldGrid : GridMap
{
    public Vector3 GetPosFromLocal(Vector3 localPosition)
    {
        var mapPos = LocalToMap(localPosition);

        var pos = ToGlobal(MapToLocal(mapPos));

        return pos;
    }
}