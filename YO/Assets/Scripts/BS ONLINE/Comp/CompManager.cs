using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Satak.Utilities;

public class CompManager : MonoBehaviour
{
    [SerializeField]private Transform[] spawnPoints;
    [SerializeField]private GameObject playerPrefab;

    public int currentPos;

    //Timer
    bool gameStarted = false;
    public GameObject scoreText;
    public GameObject Stopper;

    //UI
    public Text posText;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            CountdownTimer countdownTimer = GetComponent<CountdownTimer>();
            countdownTimer.isTimerRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcPos();
        OnGameStart();
    }

    public void SpawnPlayer()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randIndex].position, Quaternion.identity);
    }

    public void CalcPos()
    {
        posText.text = "Position: " + currentPos.ToString() + "/" + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public void OnGameStart(){
        CountdownTimer countdownTimer = GetComponent<CountdownTimer>();
        gameStarted = countdownTimer.isTimerRunning;
        ChangePlayerSettings();
    }

    void ChangePlayerSettings(){
        PlayerOnline playerOnline = FindObjectOfType<PlayerOnline>();
        playerOnline.enabled = gameStarted;
        scoreText.SetActive(gameStarted);
        Stopper.SetActive(!gameStarted);
    }
}
