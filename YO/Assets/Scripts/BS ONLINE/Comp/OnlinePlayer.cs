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

    //Animator
    Animator explosion;

    //Magic
    public bool Magic = false;

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
            if (Magic == false)
            {
                Movement();
            }
            else{
                transform.position = new Vector3(0, 0, -256);
                Freeze();
            }
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
            KatamOnCollision();
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
                KatamOnCollision();
            }
        }
    }
    public void Update()
    {
        if (rb.position.y < -1f)
        {
            if (PV.IsMine)
            {
                KatamOnCollision();
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

        compManager = FindObjectOfType<CompManager>();
    }

    public void KatamOnCollision()
    {   
        if (PV.IsMine)
        {
            if (compManager._GodMod == true)
            {
                
            }else
            {
                PhotonNetwork.Destroy(Char);
                compManager.EndGame();
            }
        }
    }
}
