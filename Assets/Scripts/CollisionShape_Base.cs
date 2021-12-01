using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhysicsColliderShape
{
    Sphere = 0,
    Plane,
    AABB
}

[RequireComponent(typeof(MyPhysicsObj))]
public abstract class CollisionShape_Base : MonoBehaviour
{
    private PhysicsColliderShape shape;
    private bool hasCollider = true;

    public PhysicsColliderShape colliderShape = PhysicsColliderShape.Sphere;
    public MyPhysicsObj myPhysicsObj;
     

    protected virtual void Start()
    {
        myPhysicsObj = GetComponent<MyPhysicsObj>();
        // add shape to a shapes list in physicworld
        FindObjectOfType<MyPhysicsWorld>().listColShapes.Add(this);
    }

    public bool GetHasCollider() { return hasCollider; }
    public abstract PhysicsColliderShape GetColliderShape();
    
}
