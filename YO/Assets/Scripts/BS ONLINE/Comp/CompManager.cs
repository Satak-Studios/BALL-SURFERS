using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Satak.Utilities;
using Photon.Chat.UtilityScripts;

public class CompManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private Transform[] spawnPoints;
    [SerializeField]private GameObject playerPrefab;

    public int currentPos;
    private int NumberOfKatams;

    //UI
    public Text koText;
    public GameObject progressObj; 
    public Text progressText;

    //OtherTimerRelated
    public float startTime = 60f;
    private float currentTime = 7f;
    public bool timerStarted;
    public Text timerText;
    public GameObject Stopper;
    public bool gameStarted = false;

    //Player Online
    public OnlinePlayer myPlayer;
    public bool gameHasEnded = false;

    //Time Taken
    public float progress = 1;

    //EndScreen
    Animator CompleteCompAnimator;
    public GameObject Controls;
    public int PlayerPosition = 100;
    public Text FeedBackText;
    public GameObject CompPanel;
    public bool _GodMod = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        SatakExtensions.SetCountDownTime(myPlayer.PV.Owner, 7);
        gameStarted = false;
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
                    timerText.text = Mathf.RoundToInt(currentTime).ToString();
                }            
            }
            //Debug.Log("You are MasterOfThisRoom!");
        }
        if (!PhotonNetwork.IsMasterClient && (SatakExtensions.GetGameStatus(myPlayer.PV.Owner) == 1))
        {
            int actTime = SatakExtensions.GetCountDownTime(myPlayer.PV.Owner);
            //int actTime = Mathf.RoundToInt(currentTime);
            if (actTime <= 0f)
            {
                gameStarted = true;
            }
            else
            {
                gameStarted = false;
                Debug.Log("CountDown Time = " + SatakExtensions.GetCountDownTime(myPlayer.PV.Owner));
                timerText.text = actTime.ToString();
            }
        }

        koText.text = "Retries : " + NumberOfKatams.ToString();
        progress = myPlayer.gameObject.transform.position.z/925 *100;
        progressText.text = progress.ToString("F2") + "%";

        if (gameStarted)
        {
            Destroy(Stopper);
            if ((myPlayer == null) == false)
            {
                myPlayer.UnFreeze();
                progressObj.SetActive(true);
            }
        }
        else
        {
            Stopper.SetActive(true);
            progressObj.SetActive(false);
            myPlayer.Freeze();
        }

        if (!gameStarted && !timerStarted)
        {
            gameStarted = false;
            timerStarted = true;
        }

        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("Lobby 1");
            SatakExtensions.SetPlayerPosition(myPlayer.PV.Owner, 0);
        }

        if (progress == 100)
        {
            if (myPlayer.PV.IsMine)
            {
                CompleteCompAnim();
            }
        }

        PlayerPosition = SatakExtensions.GetPlayerPosition(myPlayer.PV.Owner);
    }

    public void SpawnPlayer()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randIndex].position, Quaternion.identity);
        myPlayer.Freeze();

        if (PhotonNetwork.IsMasterClient)
        {
            currentTime = startTime;
            timerStarted = true;
        }
    }

    public void ReSpawnPlayer()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randIndex].position, Quaternion.identity);
        myPlayer.UnFreeze();
    }

    public void EndGame()
    {
        if (myPlayer.PV.IsMine == true)
        {
            if (!_GodMod)
            {
                gameHasEnded = true;
                Debug.Log("GAME OVER");
                Time.timeScale = 1f;   

                ReSpawnPlayer();
                NumberOfKatams++;
                SatakExtensions.SetCompRetries(myPlayer.PV.Owner, NumberOfKatams);
            }
            else
            {
                Debug.Log("Entered God Mode!");
            }
        }
    }

    public void CompleteCompAnim()
    {
        if (myPlayer.PV.IsMine && !(FindObjectOfType<FollowPlayer>().gameObject == null))
        {
            SatakExtensions.AddPlayerPosition(myPlayer.PV.Owner, 1);
            
            GodMode();
            CompleteCompAnimator = FindObjectOfType<FollowPlayer>().GetComponent<Animator>();
            CompleteCompAnimator.SetBool("compu", true);
        }
    }
    public void CompleteComp()
    {
        Controls.SetActive(false);
        CompPanel.SetActive(true);
        FeedBackText.text = PlayerPosition switch
        {
            1 => "Winner!",
            2 => "Almost",
            3 => "Close but Cigar",
            4 => "Better Luck  Next time!",
            _ => "You Noob",
        };
        myPlayer.Magic = true;
        Debug.LogError(myPlayer.PV.Owner.NickName + " Has Achieved " + PlayerPosition.ToString() + " Postion");
    }

    public void GodMode(){
        _GodMod = true;
    }

    public void GodModeOff(){
        _GodMod = false;
    }
}
