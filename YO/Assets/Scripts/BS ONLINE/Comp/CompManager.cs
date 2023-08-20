using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Satak.Utilities;

public class CompManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private Transform[] spawnPoints;
    [SerializeField]private GameObject playerPrefab;

    public int currentPos;
    public int playersReady;

    //UI
    public Text posText;

    //OtherTimerRelated
    public float startTime = 60f;
    public float currentTime;
    private Coroutine timeCoroutine;
    private bool timerStarted;
    public Text timerText;
    public GameObject Stopper;
    public bool gameStarted = false;

    //Player Online
    public OnlinePlayer myPlayer;
    public bool gameHasEnded = false;
    public GameObject ResMenu;

    //Time Taken
    public Timer _timer_;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            currentTime = startTime;
            timerStarted = true;
        }
        int _seconds = Mathf.RoundToInt(startTime);
        Debug.Log("Sec = " + _seconds);
        SatakExtensions.SetCountDownTime(myPlayer.PV.Owner, _seconds);
    }

    // Update is called once per frame
    public void Update()
    {
        myPlayer = FindObjectOfType<OnlinePlayer>();
        
        if (PhotonNetwork.IsMasterClient)
        {
            if (timerStarted == true)
            {
                currentTime = currentTime - Time.deltaTime;
                SatakExtensions.SetCountDownTime(myPlayer.PV.Owner, Mathf.RoundToInt(currentTime));   
                if (currentTime <= 0)
                {
                    timerStarted = false;
                    gameStarted = true;
                }else
                {
                    gameStarted = false;
                }            
            }
            timerText.text = Mathf.RoundToInt(currentTime).ToString();
            //Debug.Log("You are MasterOfThisRoom!");
        }
        else
        {
            int actTime = SatakExtensions.GetCountDownTime(myPlayer.PV.Owner);
            timerText.text = actTime.ToString();
            if (actTime <= 0f)
            {
                gameStarted = true;
            }
            else
            {
                gameStarted = false;
            }
        }
        CalcPos();
        //Debug.Log(SatakExtensions.GetCountDownTime(myPlayer.PV.Owner));

        if (gameStarted)
        {
            Destroy(Stopper);
            myPlayer.UnFreeze();
        }
        else
        {
            Stopper.SetActive(true);
            myPlayer.Freeze();
        }
    }

    public void SpawnPlayer()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randIndex].position, Quaternion.identity);
        myPlayer.Freeze();
    }

    public void CalcPos()
    {
        posText.text = "Position: " + currentPos.ToString() + "/" + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public void EndGame()
    {
        if (myPlayer.PV.IsMine == true)
        {
            if (gameHasEnded == false)
            {
                gameHasEnded = true;
                Debug.Log("GAME OVER");

                Time.timeScale = 1f;

                if (gameHasEnded == true)
                {
                    Debug.Log("katam");
                    ResMenu.SetActive(true);
                }
            }
        }
    }
}
