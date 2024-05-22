using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Satak.Utilities;
using Photon.Realtime;

public class CompManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    public int currentPos;
    public int NumberOfKatams;
    public int retries;
    public int tempStore;

    //UI
    public Text koText;
    public GameObject progressObj;
    public Text progressText;
    //public Slider progressSlider;

    //OtherTimerRelated
    float startTime = 3f;
    float currentTime = 3f;
    public bool timerStarted = true;
    public Text timerText;
    public GameObject Stopper;
    public bool gameStarted = false;
    bool changedFPS = false;

    //Player Online
    public OnlinePlayer myPlayer;
    public bool gameHasEnded = false;

    //Time Taken
    CompTimer compTimer => GetComponent<CompTimer>();

    //Score
    public float progress = 1;

    //CompPanel
    public string screenshotFolder;

    //EndScreen
    public int PlayerPosition = 1;
    public Text FeedBackText;
    public GameObject CompPanel;
    public bool _GodMod = false;

    //Extra
    int asasa = 0;
    public Text PING;
    public bool leaveGameWarn = false;
    public Text leaveGameWarnObj;
    public Text[] leaveGameObj;
    Vector3 _pos;

    void Start()
    {
        SpawnPlayer();
        currentTime = startTime;
        timerStarted = true;
        myPlayer = FindObjectOfType<OnlinePlayer>();
        if (myPlayer == null)
        {
            return;
        }
    }

    public void Update()
    {
        myPlayer = FindObjectOfType<OnlinePlayer>();

        //Ping
        PING.text = "PING : " + PhotonNetwork.GetPing();

        if (FindObjectOfType<Achiever>().achIndex[2] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(2);
        }

        if (timerStarted)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                timerStarted = false;
                gameStarted = true;
            }
            else
            {
                ClockRunning();
            }
        }
        switch(PlayerPosition)
        {
            case 0:
                if (!compTimer.Time_)
                {
                    FeedBackText.text = "Next Time!";
                }
                break;
            case 1:
                FeedBackText.text = "Winner!";
                break;
            case 2:
                    FeedBackText.text = "The Veiled Challenger";
                break;
            case 3:
                FeedBackText.text = "Close, but Cigar!";
                break;
            case 4:
                    FeedBackText.text = "Better efforts, Next time!";
                break;
            default:
                FeedBackText.text = "";
                break;
        };


        #region extra
        timerText.text = Mathf.RoundToInt(currentTime).ToString();
        koText.text = "Retries : " + retries.ToString();
        if (!(myPlayer == null))
        {
            progress = myPlayer.gameObject.transform.position.z / 925 * 1000;
        }
        progressText.text = progress.ToString("0");// + "%";
        //progressSlider.value = progress / 925 * 1000;
        screenshotFolder = Path.Combine(Application.persistentDataPath, "Screenshots");
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }

        if (gameStarted)
        {
            Stopper.SetActive(false);
            if ((myPlayer == null) == false)
            {
                myPlayer.UnFreeze();
                asasa++;
                if (asasa == 1)
                {
                    progressObj.SetActive(true);
                }
                compTimer.StartTimer();
            }
        }

        if (!gameStarted && !timerStarted)
        {
            Stopper.SetActive(false);
        }

        if (progress >= 1000)
        {
            CompleteCompAnim();
            if (PlayerPrefs.GetInt("fps") == 1)
            {
                PlayerPrefs.SetInt("fps", 0);
                changedFPS = true;
            }
        }

        PlayerPosition = SatakExtensions.GetPlayerPosition(myPlayer.PV.Owner);

        //Device
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            LeaveGame(0);
        }
        else
        {
            LeaveGame(1);
        }

        //Extra
        if (Input.GetKey(KeyCode.Q))
        {
            leaveGameWarnObj.gameObject.SetActive(true);
            leaveGameWarn = true;
        }

        if (Input.GetKey(KeyCode.Y) && leaveGameWarn == true)
        {
            PhotonNetwork.DestroyAll(true);
            SceneManager.LoadScene("Lobby 1");
        }

        if (Input.GetKey(KeyCode.N) && leaveGameWarn == true)
        {
            leaveGameWarn = false;
            leaveGameWarnObj.gameObject.SetActive(false);
        }

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            leaveGameWarnObj.text = "You want to leave?All Players will also leave(Press) \n Y / N";
        }
        else
        {
            leaveGameWarnObj.text = "You want to leave?(Press) \n Y / N";
        }
        #endregion
    }

    void ClockRunning()
    {
        Stopper.SetActive(true);
        progressObj.SetActive(false);
        myPlayer.Freeze(_pos);
    }

    public void SpawnPlayer()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        _pos = spawnPoints[randIndex].position;
        PhotonNetwork.Instantiate(playerPrefab.name, _pos, Quaternion.identity);
        if (myPlayer != null)
        {
            myPlayer.Freeze(_pos);
        }
    }

    public void ReSpawnPlayer()
    {
        if (myPlayer != null)
        {
            myPlayer = null;
        }

        int randIndex = Random.Range(0, spawnPoints.Length);
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randIndex].position, Quaternion.identity);
        myPlayer = newPlayer.GetComponent<OnlinePlayer>();
        if (myPlayer != null)
        {
            myPlayer.UnFreeze();
        }
        NumberOfKatams++;
    }

    public void EndGaming()
    {
        if (myPlayer.PV.IsMine && !_GodMod)
        {
            retries++;
            gameHasEnded = true;
            Time.timeScale = 1f;
            PhotonNetwork.Destroy(myPlayer.Char);
            ReSpawnPlayer();         
        }
    }

    public void CompleteCompAnim()
    {
        if (myPlayer.PV.IsMine && progress >= 1000)
        {
            GodMode();
            gameStarted = false;
            timerStarted = false;
            compTimer.StopTimer();
            koText.gameObject.SetActive(false);
            progressText.gameObject.SetActive(false);  
            //myPlayer.cam.SetBool("compu", true);
            Camera.main.GetComponent<Animator>().SetBool("compu", true);
            string _message = "\n has completed the race!";
            FindObjectOfType<Notifier>().SendInfo(_message);
            progressObj.SetActive(false);
        }
    }
    public void CompleteComp()
    {
        myPlayer._disappear = true;
        CompPanel.SetActive(true);

        switch (PlayerPosition)
        {
            case 1:
                if (PlayerPrefs.GetInt("Achievement_20") == 0)
                {
                    FindObjectOfType<Achiever>().AchievementUnlocked(20);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("Achievement_21") == 0)
                {
                    FindObjectOfType<Achiever>().AchievementUnlocked(21);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("Achievement_22") == 0)
                {
                    FindObjectOfType<Achiever>().AchievementUnlocked(22);
                }
                break;
            default:
                break;
        }
        myPlayer.Magic = true;
    }

    public void GodMode()
    {
        _GodMod = true;
    }

    public void GodModeOff()
    {
        _GodMod = false;
    }

    public void Continue()
    {
        if (changedFPS)
        {
            PlayerPrefs.SetInt("fps", 1);
            changedFPS = false;
        }
        SceneManager.LoadScene("Lobby 1");
        string _message = "\n has left the game.";
        FindObjectOfType<Notifier>().SendInfo(_message);
        SatakExtensions.SetPlayerPosition(myPlayer.PV.Owner, 0);
        SatakExtensions.SetTime(myPlayer.PV.Owner, 0);
        LevelGenerator[] obsticles = FindObjectsOfType<LevelGenerator>();
        for (int i = 0; i < obsticles.Length; i++)
        {
            if (obsticles[i].GetComponent<PhotonView>())
            {
                PhotonNetwork.Destroy(obsticles[i].gameObject);
            }
        }
    }

    public void ScreenShot()
    {
        string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".png";
        string screenshotPath = Path.Combine(screenshotFolder, screenshotName);

        ScreenCapture.CaptureScreenshot(screenshotPath);
        FindObjectOfType<Achiever>().Notify("Saved!", "ScreenShot successfully saved!");
    }

    public void LeaveGameFake()
    {
        leaveGameObj[0].text = "Confirm?";
        
        if (tempStore == 1)
        {
            PhotonNetwork.DestroyAll(true);
            SceneManager.LoadScene("Lobby 1");
            string _message = "\n has left the game.";
            FindObjectOfType<Notifier>().SendInfo(_message);
        }
        tempStore++;
    }

    public void LeaveGame(int x)
    {
        leaveGameObj[0].gameObject.SetActive(false);
        leaveGameObj[1].gameObject.SetActive(false);
        leaveGameObj[x].gameObject.SetActive(true);
    }

    #region Errors
    public override void OnDisconnected(DisconnectCause cause)
    {
        string _message = "\n was Disconnected due to " + cause + ".";
        FindObjectOfType<Notifier>().SendInfo(_message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string _message = "\n has joined the game.";
        FindObjectOfType<Notifier>().SendInfo(_message);
    }
    #endregion
}
