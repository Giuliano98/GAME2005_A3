using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyPhysicsWorld  : MonoBehaviour
{
    public Vector3 m_gravity = new Vector3(0, -9.81f, 0);

    public List<MyPhysicsObj> listPhysicObjs = new List<MyPhysicsObj>();
    public List<CollisionShape_Base> listColShapes = new List<CollisionShape_Base>();


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        foreach (MyPhysicsObj obj in listPhysicObjs)
        {
            if(obj.isLock) continue;
            obj.UpdatingObj();
        }
        
        CheckingCollisions();
    }

    private void CheckingCollisions()
    {
        foreach (CollisionShape_Base obj1 in listColShapes)
        {
            foreach (CollisionShape_Base obj2 in listColShapes)
            {
                // if it's the same obj, go to the break this loop
                if (obj1 == obj2) break;
                // if one of the obj doesnot have a collider, check the next one
                if (!obj1.GetHasCollider() || !obj2.GetHasCollider()) continue;

                CollisionShape_Base ob1 = obj1;
                CollisionShape_Base ob2 = obj2;

                PhysicsColliderShape shape1 = ob1.GetColliderShape();
                PhysicsColliderShape shape2 = ob2.GetColliderShape();
                
                // test Sphere-Sphere collision
                if (shape1 == PhysicsColliderShape.Sphere && 
                    shape2 == PhysicsColliderShape.Sphere)
                {
                    SphereSphereCollision((CollisionShape_Sphere)obj1, (CollisionShape_Sphere)obj2);
                }

                // test Sphere-Plane & Plane-Sphere collision
                if ((shape1 == PhysicsColliderShape.Sphere && 
                     shape2 == PhysicsColliderShape.Plane) 
                     || 
                     (shape1 == PhysicsColliderShape.Plane &&
                      shape2 == PhysicsColliderShape.Sphere))
                {
                    if (shape1 == PhysicsColliderShape.Sphere)
                        SpherePlaneCollision((CollisionShape_Sphere)obj1, (CollisionShape_Plane)obj2);
                    else
                        SpherePlaneCollision((CollisionShape_Sphere)obj2, (CollisionShape_Plane)obj1);
                }
            }
        }
    }

    // function for Sphere-Sphere collision
    void SphereSphereCollision(CollisionShape_Sphere a, CollisionShape_Sphere b)
    {
        Vector3 m_dis = b.transform.position - a.transform.position;
        float m_disMag = m_dis.magnitude;
        float m_sumRad = a.radius + b.radius;
        float m_depth = m_sumRad - m_disMag;
        bool hasCollision = m_depth > 0.0f;
        
        if(!hasCollision) return;

        Vector3 ColNormalAtoB = m_dis / m_disMag;
        Vector3 minTransVecAtoB =  ColNormalAtoB * m_depth;
        float mtvScalarA = 0.5f;
        float mtvScalarB = 0.5f;

        if(a.myPhysicsObj.isLock && !b.myPhysicsObj.isLock)
        {
            mtvScalarA = 0.0f;
            mtvScalarB = 1.0f;
        }
        if(!a.myPhysicsObj.isLock && b.myPhysicsObj.isLock)
        {
            mtvScalarA = 1.0f;
            mtvScalarB = 0.0f;
        }
        if(!a.myPhysicsObj.isLock && !b.myPhysicsObj.isLock)
        { 
            mtvScalarA = 0.5f;
            mtvScalarB = 0.5f;
        }
        
        Vector3 transA = minTransVecAtoB * -mtvScalarA;
        Vector3 transB = minTransVecAtoB * mtvScalarB;

        a.transform.position += transA;
        b.transform.position += transB;
    }
    // function for Sphere-Plane & Plane-Sphere collision
    void SpherePlaneCollision(CollisionShape_Sphere a, CollisionShape_Plane b)
    {
        
        Vector3 vecPlaneToSphere = a.transform.position - b.transform.position; 
        float dot = Vector3.Dot(vecPlaneToSphere, b.GetNormal());   
        float disPlaneSphere = Mathf.Abs(dot);
        bool HasCollision = disPlaneSphere <= a.radius;

        if(!HasCollision) return;

        a.myPhysicsObj.useGravity = false;
        a.myPhysicsObj.m_velocity.y = 0.0f;

        float m_Depth = a.radius - disPlaneSphere;
        Vector3 m_minMovVecAoutP = b.GetNormal() * m_Depth;

        a.transform.position += m_minMovVecAoutP;

    }
}
