using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyPhysicsObj : MonoBehaviour
{

    Vector3 m_position = new Vector3(0,0,0);
    public Vector3 m_velocity = new Vector3(0,0,0);
    Vector3 m_force = new Vector3(0,0,0);
    Vector3 m_gravity = new Vector3(0,0,0);

    public bool useGravity = true;
    public bool isLock = false;

    public float m_mass = 1.0f;
    public float m_gravityScale  = 0.1f;

    public MyPhysicsWorld myPhysicsWorld;


    public void UpdatingObj()
    {
        if(useGravity)
            this.m_force += this.m_mass * this.m_gravity * this.m_gravityScale;

        this.m_velocity += this.m_force / this.m_mass * Time.deltaTime;
        this.m_position += this.m_velocity * Time.deltaTime;
        this.transform.position = this.m_position;

        this.m_force.Set(0,0,0);
    }

    void Start()
    {
        // add obj to an obj list in PhysicWorld
        myPhysicsWorld = FindObjectOfType<MyPhysicsWorld>();
        myPhysicsWorld.listPhysicObjs.Add(this);

        m_mass = 1.0f;
        m_gravityScale  = 0.1f;

        m_position = transform.position;
        m_gravity = myPhysicsWorld.m_gravity;
    }

    void Update()
    {
        
    }
}
