using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public GameObject[] Heart;

    public GameObject Score;
    public GameObject REMENU;

    public GameObject NOINTERNET;
    public GameObject GameOver;
    public PlayerOnline pso;

    //==MAIN==
    public int Hearts = 3;


    // Start is called before the first frame update
    public void SpawnOrg()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Transform Point = spawnPoints[rand];
        PhotonNetwork.Instantiate(playerPrefab.name, Point.position, Quaternion.identity);
    }


    public void Start()
    {
        SpawnOrg();
        GameOver.SetActive(false);
        if (Hearts == 0)
        {
            Hearts = 3;
        }

        if (!PV.IsMine)
        {
            Destroy(pso);
        }

        if (FindObjectOfType<Achiever>().achIndex[2] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(2);
        }
    }

    public void Update()
    {
        switch (Hearts)
        {
            case 0:
                Heart[0].SetActive(false);
                Heart[1].SetActive(false);
                Heart[2].SetActive(false);
                GameOver.SetActive(true);
                break;

            case 1:
                Heart[0].SetActive(true);
                Heart[1].SetActive(false);
                Heart[2].SetActive(false);
                break;

            case 2:
                Heart[0].SetActive(true);
                Heart[1].SetActive(true);
                Heart[2].SetActive(false);
                break;

            case 3:
                Heart[0].SetActive(true);
                Heart[1].SetActive(true);
                Heart[2].SetActive(true);
                break;
        }

        pso = FindObjectOfType<PlayerOnline>();
        if (PV == null)
        {
            return;
        }
        if (pso != null)
        {
            PV = pso.PV;
        }
    }

    public void Retry()
    {
       if (!(Hearts == 0))
         {
             SpawnOrg();
         }
       else
         {
             GameOver.SetActive(true);
         }
    }

     public void Wait()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            NOINTERNET.SetActive(true);
        }
        else
        {
            string _message = "\n has left the game.";
            FindObjectOfType<Notifier>().SendInfo(_message);
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

     public void GameOverOff()
     {
         GameOver.SetActive(false);
     }

     public void addHeart()
     {
        Hearts ++;
     }
}