using Godot;
using System;

public static class Tools
{
    public static Rigid_Body FindRigidBody(Node Source)
    {
        var children = Source.GetChildren();
        foreach (Node child in children)
        {
            if (child is Rigid_Body)
            {
                return (Rigid_Body)child;
            }
            else
            {
                return FindRigidBody(child);
            }
        }

        return null;
    }

    public static Rigid_Body FindRigidBodyFromRoot(Node Source)
    {
        return FindRigidBody(Source.Owner == null ? Source : Source.Owner);
    }

    public static Node GetRoot(Node Source)
    {
        if (Source.Owner == null)
            return Source;
        return GetRoot(Source.Owner);
    }

    public static Node GetRoot<T>(Node Source)
    {
        if (Source == null)
            return null;

        if (Source.Owner == null || Source is T)
            return Source;
        return GetRoot<T>(Source.Owner);
    }

    public static float DegToRad(float degrees)
    {
        return degrees * Mathf.Pi / 180;
    }
}