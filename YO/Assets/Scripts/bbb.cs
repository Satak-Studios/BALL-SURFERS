using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbb : MonoBehaviour
{
    public playermovement pm;
    public float forwardspeed;
    public float sidespeed;
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        forwardspeed = pm.forwardForce;
        sidespeed = pm.sidewaysForce;
    }
    
    public void Left()
    {
        rb.AddForce(-sidespeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void Right()
    {
        rb.AddForce(sidespeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
