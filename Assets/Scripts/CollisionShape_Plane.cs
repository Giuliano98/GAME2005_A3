using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X = 0,
    Y,
    Z
}

public class CollisionShape_Plane : CollisionShape_Base
{
    public Axis alignment = Axis.Y;

    public override PhysicsColliderShape GetColliderShape()
    {
        return PhysicsColliderShape.Plane;
    }

    public Vector3 GetNormal()
    {
        switch (alignment)
        {
            case (Axis.X):
            {
                return transform.right;
            }
            case (Axis.Y):
            {
                return transform.up;
            }
            case (Axis.Z):
            {
                return transform.forward;
            }
            default:
            {
                throw new System.Exception("invalid plane!");
            }
        }
    }

}
