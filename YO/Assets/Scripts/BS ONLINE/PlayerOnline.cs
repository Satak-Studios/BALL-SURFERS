using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

public class PlayerOnline : MonoBehaviourPunCallbacks
{
    public Camera PlayerCam;

    public RestartOnline RSO = null;
    public GameObject Char;

    public Rigidbody rb;

    private Restart rs;
    public PlayerOnline movement;

    //Code From PlayerMovement Script
    private float forwardForce = 300f;
    private float sidewaysForce = 35f;
    public bool isgoingleft;
    public bool isgoingright;

    //Photon View
    public PhotonView PV;
    public GameObject Controls;
    public bool _disappear;

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            RSO.enabled = false;
            Destroy(movement);
        }
        else
        {
            RSO.enabled = true;
        }
        rs = FindObjectOfType<Restart>();
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
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.A))
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

    public void ControlsOff()
    {

    }
    //Collition Script
    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (PV.IsMine)
        {
            if (collisionInfo.collider.CompareTag("Obsticle"))
            {
                movement.enabled = false;
                RSO.EndGaming();
                rs.Restartmenu.SetActive(true);
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
                RSO.EndGaming();
                rs.Restartmenu.SetActive(true);
                katam();
            }
        }

        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            RSO.enabled = false;
            Destroy(movement);
        }
        else
        {
            RSO.enabled = true;
            ChangeUI();
        }
    }


    //[PunRPC]
    public void katam()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(Char);
        }
    }

    public void ChangeUI()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (_disappear)
            {
                Controls.SetActive(false);
            }
            else
            {
                Controls.SetActive(true);
            }
        }
        else
        {
            Controls.SetActive(false);
        }
    }
}
