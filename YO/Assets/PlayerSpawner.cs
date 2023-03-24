using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public Restart rs;
    public PhotonView PV;
    public Timer timer;

    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    int randPlayer;

    public GameObject RECAM;
    public GameObject Controlls;
    public GameObject Score;
    public GameObject REMENU;

    public ScoreOnline sco;

    public GameObject Hearts1;
    public GameObject Hearts2;
    public GameObject Hearts3;


    public GameObject NOINTERNET;
    public GameObject GameOver;
    public GameObject AddNoHearts;

    public player SCO;
    public PlayerOnline pso;

    //==MAIN==
    public int Hearts = 3;


    // Start is called before the first frame update
    public void SpawnOrg()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        //int randPlayer = Random.Range(0, playerPrefabs.Length);
        Transform Point = spawnPoints[rand];
        GameObject playertoSpawn = playerPrefabs[randPlayer];
        PhotonNetwork.Instantiate(playertoSpawn.name, Point.position, Quaternion.identity);
        Debug.Log("Spawned Player = " + playertoSpawn.name);
        SCO.StartTimer();
        //Vector3 Playerpos = new Vector3(Posx, Posy, Posz);
        //GameObject playerToSpawn = playerPrefabs;
        //Old
        //PhotonNetwork.Instantiate(playerPrefabs.name, Point.position, Quaternion.identity);
    }


    public void Start()
    {
        /* Player _player = PhotonNetwork.LocalPlayer;
         //PV = FindObjectOfType<QuitScreen>().GetComponent<PhotonView>();
         if (_player.GetHearts() >= 0)
         {
             SpawnOrg();
         }
         randPlayer = Random.Range(0, playerPrefabs.Length);
         _player.SetHearts(3);
         GameOver.SetActive(false);
         if (_player.GetHearts() == 0)
         {
             _player.SetHearts(3);
         }

         if (!PV.IsMine)
         {
             Destroy(pso);
         }

         if (!PlayerPrefs.HasKey("Hearts"))
         {
              _player.SetHearts(3);
              Debug.Log("You are a noob, hah; hah;");
         }
         */

        randPlayer = Random.Range(0, playerPrefabs.Length);
        GameOver.SetActive(false);
        if (Hearts == 0)
        {
            Hearts = 3;
        }

        if (!PV.IsMine)
        {
            Destroy(pso);
        }
       SpawnOrg();
    }

    public void Update()
    {
        PV = FindObjectOfType<QuitScreen>().GetComponent<PhotonView>();
        pso = FindObjectOfType<PlayerOnline>();
        if (PV == null)
        {
            return;
        }
        if (pso == null)
        {
            return;
        }

        if (PV.IsMine)
        {
            if (Hearts == -3)
            {
                AddNoHearts.SetActive(true);
                Hearts = 0;
            }
            if (Hearts == -2)
            {
                AddNoHearts.SetActive(true);
                Hearts = 0;
            }
            if (Hearts == -1)
            {
                AddNoHearts.SetActive(true);
                Hearts = 0;
            }

            if (Hearts == 0)
            {
                // GameOver.SetActive(false);
                Hearts1.SetActive(false);
                Hearts2.SetActive(false);
                Hearts3.SetActive(false);
                AddNoHearts.SetActive(false);
                GameOver.SetActive(true);
            }

            if (Hearts == 1)
            {
                Hearts1.SetActive(false);
                Hearts2.SetActive(false);
                Hearts3.SetActive(true);
                GameOver.SetActive(false);
            }

            if (Hearts == 2)
            {
                Hearts1.SetActive(false);
                Hearts2.SetActive(true);
                Hearts3.SetActive(true);
                GameOver.SetActive(false);
            }

            if (Hearts == 3)
            {
                Hearts1.SetActive(true);
                Hearts2.SetActive(true);
                Hearts3.SetActive(true);
                GameOver.SetActive(false);
            }
        }
    }

    public void Destroy()
    {
        //PhotonNetwork.Destroy(FakePV);
    }

      public void Wait()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            NOINTERNET.SetActive(true);
        }
        else
        {
            NOINTERNET.SetActive(false);
            SceneManager.LoadScene("Lobby 1");
            Hearts = 3;
            PlayerPrefs.SetString("Hearts", "You are a noob");
        }
    }

    public void Lobby()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby 1");
    }

    public void msterok()
    {
        SceneManager.LoadScene("Lobby 1");
        Hearts = 3;
    }

     public void GameOverOff(){
         GameOver.SetActive(false);
     }
}