using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Satak.Utilities;

public class CompManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    public int currentPos;
    private int NumberOfKatams;

    //UI
    public Text koText;
    public GameObject progressObj;
    public Text progressText;
    public Slider progressSlider;

    //OtherTimerRelated
    float startTime = 3f;
    float currentTime;
    public bool timerStarted = true;
    public Text timerText;
    public GameObject Stopper;
    public bool gameStarted = false;

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
    public GameObject Controls;
    public int PlayerPosition = 100;
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

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        currentTime = startTime;
        timerStarted = true;
    }

    // Update is called once per frame
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

        #region extra

        timerText.text = Mathf.RoundToInt(currentTime).ToString();
        koText.text = "Retries : " + NumberOfKatams.ToString();
        if (!(myPlayer == null))
        {
            progress = myPlayer.gameObject.transform.position.z / 925 * 100;
        }
        progressText.text = progress.ToString("F2") + "%";
        progressSlider.value = progress;
        screenshotFolder = Path.Combine(Application.persistentDataPath, "Screenshots");
        Directory.CreateDirectory(screenshotFolder);

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

        if (progress >= 100)
        {
            CompleteCompAnim();
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
        myPlayer.Freeze(_pos);
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
                Time.timeScale = 1f;
                PhotonNetwork.Destroy(myPlayer.Char);

                ReSpawnPlayer();
                NumberOfKatams++;
            }
        }
    }

    public void CompleteCompAnim()
    {
        if (myPlayer.PV.IsMine && progress >= 100)
        {
            GodMode();
            gameStarted = false;
            timerStarted = false;
            compTimer.StopTimer();
            koText.gameObject.SetActive(false);
            progressText.gameObject.SetActive(false);  
            myPlayer.cam.SetBool("compu", true);
            progressObj.SetActive(false);
        }
    }
    public void CompleteComp()
    {
        Controls.SetActive(false);
        CompPanel.SetActive(true);
        FeedBackText.text = PlayerPosition switch
        {
            1 => "Winner!",
            2 => "Close, but Cigar",
            3 => "Wistful!",
            4 => "Better efforts, Next time!",
            5 => "You Noob",
            _ => "Winner!"
        };

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
        SceneManager.LoadScene("Lobby 1");
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
    }

    public void LeaveGame(int x)
    {
        leaveGameObj[0].gameObject.SetActive(false);
        leaveGameObj[1].gameObject.SetActive(false);
        leaveGameObj[x].gameObject.SetActive(true);
    }
}
