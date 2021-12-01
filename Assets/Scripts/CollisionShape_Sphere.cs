using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    this collision shape Sphere only works if the Sphere mesh is
    scaled same in each Axis
*/
public class CollisionShape_Sphere : CollisionShape_Base
{
    public float radius = 1.0f;

    protected override void Start()
    {
        this.radius = this.transform.localScale.x / 2.0f;
        base.Start();
    }
    
    public override PhysicsColliderShape GetColliderShape()
    {
        return PhysicsColliderShape.Sphere;
    }
    
}
