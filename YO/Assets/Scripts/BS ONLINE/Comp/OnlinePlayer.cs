using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

    public class OnlinePlayer : MonoBehaviourPunCallbacks
    {
     public Camera PlayerCam;
    public GameObject Char;

    public Rigidbody rb;

    private CompManager compManager;
    public OnlinePlayer movement;

    //Code From PlayerMovement Script
    public float forwardForce;    
    public float sidewaysForce;    
    public bool isgoingleft;    
    public bool isgoingright;    

    //Photon View    
    public PhotonView PV;      

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            Destroy(movement);
        }
        else
        {
            Debug.Log("PV is Mine");
        }
    }

    public void Freeze(){
        movement.enabled = false;
    }

    public void UnFreeze(){
        movement.enabled = true;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Movement();
        }
    }

    public void Movement()
    {

        // Add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        //Controls for A & D
        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        //Controls for Arrow Keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (rb.position.y < -1f)
        {
            katam();
        }

        if (isgoingleft)
        {
            Debug.Log("Is Going Left");
            Left();
        }

        if (isgoingright)
        {
            Debug.Log("Is Going Right");
            Right();
        }
    }

    public void Left()
    {
        rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void Right()
    {
        rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void pointerDownleft()
    {
        isgoingleft = true;
    }
    public void pointerUpleft()
    {
        isgoingleft = false;
    }
    public void pointerDownright()
    {
        isgoingright = true;
    }

    public void pointerUpright()
    {
        isgoingright = false;
    }

    //Collition Script
    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (PV.IsMine)
        {
            if (collisionInfo.collider.tag == "Obsticle")
            {
                movement.enabled = false;
                katam();
            }
        }
    }
    public void Update()
    {
        if (rb.position.y < -1f)
        {
            if (PV.IsMine)
            {
                katam();
            }
        }

        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            Destroy(movement);
        }
        else
        {
            
        }
    }

    public void katam()
    {   
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(Char);
            compManager.EndGame();
        }
    }
}
