using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

//namespace BallSurfersOnlineMovement
//{
    public class PlayerOnline : MonoBehaviourPunCallbacks
    {
    public Camera PlayerCam;
    public GameObject PlayerKatamExplosion;

      public RestartOnline RSO;
      public GameObject Char;

        //PhotonView PV;
        //public Text playerName;
        //public GameObject playerNameObj;

        public Rigidbody rb;

        private Restart rs;
        public PlayerOnline movement;

        //Code From PlayerMovement Script
        public float forwardForce;
        public float sidewaysForce;
        public bool isgoingleft;
        public bool isgoingright;

        //Photon View
        public PhotonView PV;
        public QuitScreen qs;

        private void Start()
        {
        PlayerKatamExplosion.SetActive(false);
        if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            RSO.enabled = false;
            Destroy(movement);
        }
        else
        {
            RSO.enabled = true;
            Debug.Log("PV is Mine");
        }
            //playerName.text = PhotonNetwork.NickName;
            rs = FindObjectOfType<Restart>();
    }

        // Update is called once per frame
        public void FixedUpdate()
        {
        // Add a forward force
        /*rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
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
        }*/
        if (PV.IsMine)
        {
            Movement();
            //playerNameObj.SetActive(false);
            //playerName.text = PhotonNetwork.NickName;
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
            FindObjectOfType<GameManager>().EndGame();
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

        public void ControlsOff()
        {

        }
        //Collition Script
        public void OnCollisionEnter(Collision collisionInfo)
        {
        if (PV.IsMine)
        {
            if (collisionInfo.collider.tag == "Obsticle")
            {
                movement.enabled = false;
                // RSO.EndGaming(PhotonNetwork.LocalPlayer);
                RSO.EndGaming();
                rs.Restartmenu.SetActive(true);
                katam();
                //rs.sco.SaveHScore();
                //rs.EndGames();
                // FindObjectOfType<Restart>().EndGames();
            }
        }
        }

    //Restart Script
    /*  public void EndGaming()
      {
          if (rs.gameHasEnded == false)
          {
              rs.gameHasEnded = true;
              Debug.Log("GAME OVER");
              rs.controls.SetActive(true);

              Time.timeScale = 0.5f;
              new WaitForSeconds(5f);
              rs.Restartmenu.SetActive(true);
              new WaitForSeconds(1);


              if (rs.gameHasEnded == true)
              {
                  Debug.Log("Displayed");
                  rs.Restartmenu.SetActive(true);
                  //Char.SetActive(false);
              }
          }
      }*/
    public void Update()
    {
        if (rb.position.y < -1f)
        {
            if (PV.IsMine)
            {
                //RSO.EndGaming(PhotonNetwork.LocalPlayer);
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
        }
        /*if (PhotonNetwork.LocalPlayer.GetHearts() == 0)
        {
            katam();
        }
        /*if (!PV.IsMine)
        {
            Destroy(PlayerCam);
            RSO.enabled = false;
            rs.sco.isMine = false;
        }
        if (PV.IsMine)
        {
            //RSO.enabled = true;
           // rs.sco.isMine = true;
            //Debug.Log("PV is Mine");fghg
        }*/
    }


        //[PunRPC]
         public void katam()
         {   
           if (PV.IsMine)
           {
            //PhotonNetwork.Instantiate(PlayerKatamExplosion.name, gameObject.transform.position, Quaternion.identity);
            //Char.SetActive(false);
            //PhotonNetwork.Destroy(PV);
            PhotonNetwork.Destroy(Char);
            //rs.sco.SaveHScore();
            //PlayerKatamExplosion.SetActive(true);
            //PlayerKatamExplosion.transform.position = gameObject.transform.position;
        }
         }

    }
//}
