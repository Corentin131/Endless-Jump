using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D rigidbody2D; 
    public WheelJoint2D wheel;

    void Start()
    {
    }
    void Update()
    {
        rigidbody2D.velocity = new Vector2(3700*Time.deltaTime,rigidbody2D.velocity.y);
    }
}

