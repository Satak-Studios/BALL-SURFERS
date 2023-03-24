using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    //public GameObject SpecCam;
    public Restart rs;

    // public PhotonView FakePV;
    public PhotonView PV;
    public Timer timer;

    public GameObject[] playerPrefabs;
    public GameObject FakeplayerPrefabs;
    public Transform[] spawnPoints;
    int randPlayer;

    public GameObject RECAM;
    public GameObject Controlls;
    public GameObject Score;
    public GameObject REMENU;

    public ScoreOnline sco;

    //public int timesKatam = 0;
    public GameObject Hearts1;
    public GameObject Hearts2;
    public GameObject Hearts3;
    //public Player _player;


    public GameObject NOINTERNET;
    //public GameObject Hearts5;
    public GameObject GameOver;
    public GameObject AddNoHearts;

    public player SCO;
    public CheckPointTrigger cpt;
    public PlayerOnline pso;

    //==MAIN==
    public int Hearts = 3;

    public GameObject HeartsReward;
    public GameObject noAD;

    //public Transform _spawnPoint;
    //public Vector3 playerPos;

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

   public void SpawnCheckPoint()
   {
        int rand = Random.Range(0, spawnPoints.Length);
        Transform Point = spawnPoints[rand];
        GameObject playertoSpawn = playerPrefabs[randPlayer];
        PhotonNetwork.Instantiate(playertoSpawn.name, Point.position, Quaternion.identity);
        Debug.Log("Spawned Player = " + playertoSpawn.name);
        cpt.LoadFromPreviousCheckpoint();
        //SCO.StartTimer();
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
         ExitGames.Client.Photon.Hashtable gameStarted = new ExitGames.Client.Photon.Hashtable();
         if ((int)gameStarted["Started"] == 1)
         {
             gameStarted["Started"] = 0;
            // _player.SetHearts(3);
             PhotonNetwork.SetPlayerCustomProperties(gameStarted);
         }
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

      /* if (!PlayerPrefs.HasKey("Hearts"))
        {
            _player.SetHearts(3);
            Debug.Log("You are a noob, hah; hah;");
        }
        int rand = Random.Range(0, spawnPoints.Length);
        Transform Point = spawnPoints[rand];
        //Vector3 Playerpos = new Vector3(Posx, Posy, Posz);
        //GameObject playerToSpawn = playerPrefabs;
        FakeObj.SetActive(true);
        //spawnPoints[rand] = FakeObj.transform;
            
        //PhotonNetwork.Instantiate(FakeObj.name, Point.position, Quaternion.identity);
        */
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
                //sco.IncreaseScore();
            }

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                HeartsReward.SetActive(false);
                noAD.SetActive(true);
            }
            else
            {
                HeartsReward.SetActive(true);
                noAD.SetActive(false);
            }
        }
    }

    public void Destroy()
    {
        //PhotonNetwork.Destroy(FakePV);
    }

    ///*
    public void Retry()
    {
        /* RECAM.SetActive(true);
         Controlls.SetActive(false);
         Score.SetActive(false);
         //hScore.SetActive(false);
         REMENU.SetActive(false);
         timer.Start();*/
        /*PhotonNetwork.Instantiate(RECAM.name, gameObject.transform.position, Quaternion.identity);
        if (!PV.IsMine)
        {
            PhotonNetwork.Destroy(RECAM);
        }*/
        //sco.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();*/
        if (Hearts == 2)
        {
            SpawnOrg();
        }

        if (Hearts == 1)
        {
            SpawnOrg();
        }

        if (Hearts == 0)
        {
            REMENU.SetActive(false);
            Hearts1.SetActive(false);
            Hearts2.SetActive(false);
            Hearts3.SetActive(false);
            AddNoHearts.SetActive(false);
            GameOver.SetActive(true);
        }
        REMENU.SetActive(false);
        SCO.cScore = 0;
    }
    //*/


    public void Wait()
    {
        /* if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
         {
             //RetryObj.SetActive(false);
             Lobby();
         }

         if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
         {
             Spectacte();
         }*/
        //SceneManager.LoadScene("Lobby 1");
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            NOINTERNET.SetActive(true);
            //playerrr.SetHearts(3);
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
