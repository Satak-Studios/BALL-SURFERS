using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

public class CustomPlayer : MonoBehaviourPunCallbacks
{
    public Camera PlayerCam;
    public GameObject Char;

    public Rigidbody rb;

    private CustomManager customManager;
    public CustomPlayer movement;

    //Code From PlayerMovement Script
    public float forwardForce;
    public float sidewaysForce;
    public bool isgoingleft;
    public bool isgoingright;

    //Photon View    
    public PhotonView PV;

    //Animator
    public Animator cam;

    //Magic
    public bool Magic = false;

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            Destroy(movement);
        }

        cam = PlayerCam.GetComponent<Animator>();
        LoadRoomProps();
    }

    public void LoadRoomProps()
    {
        forwardForce = PlayerPrefs.GetInt("fSpeedCustom", 300);
        sidewaysForce = PlayerPrefs.GetInt("sSpeedCustom", 35);
    }

    public void Freeze(Vector3 pos)
    {
        movement.enabled = false;
        movement.transform.position = pos;
    }

    public void UnFreeze()
    {
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
            else
            {
                Vector3 pos = new Vector3(0, 50, -25006);
                Freeze(pos);
            }
            Start();
        }
    }

    #region Movement
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
            Left();
        }

        if (isgoingright)
        {
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
    #endregion

    //Collition Script
    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (PV.IsMine)
        {
            if (customManager != null && !customManager.crazyMode && collisionInfo.collider.CompareTag("Obsticle"))
            {
                movement.enabled = false;
                KatamOnCollision();
                //PhotonNetwork.Destroy(Char);
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

        customManager = FindObjectOfType<CustomManager>();
    }

    public void KatamOnCollision()
    {
        if (PV.IsMine)
        {
            if (customManager._GodMod == true)
            {

            }
            else
            {
                //PhotonNetwork.Destroy(Char);
                customManager.EndGame();
            }
        }
    }
}
