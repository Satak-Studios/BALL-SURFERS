using System;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using Photon.Pun.UtilityScripts;

public class player : MonoBehaviour
{
   // private float playertp;
    public Text scoreText;
    public Text timeText;
    public Text stoptimeText;
    public Text PING;
    //public Text HighScore;
    //public Text hscore;
    public int HighScoreFloat;
    public float Score;

    public int cScore;
    public float currentTime;
    public bool stopwatchActive = false;
    public float multiplayer = 5;
    //public RestartOnline RSO;
    //public Player _player;
    //public PhotonView PV;
    public bool isMine;

    Player Player => m_player;
    Player m_player;

    private void Start()
    {
        currentTime = 0;
        StartTimer();
        ScoreExtensions.SetScore(Player, HighScoreFloat);
    }

    // Update is called once per frame
    public void Update()
    {
        //PV = FindObjectOfType<PlayerOnline>().PV;
        //if (PV == null)
        //{
         //   return;
        //}
       // {
            if (stopwatchActive == true)
            {
                currentTime = currentTime + Time.deltaTime;
            }
            cScore = Mathf.RoundToInt(currentTime * multiplayer);
            scoreText.text = cScore.ToString();
            //scoreText.text = currentTime.ToString();
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            stoptimeText.text = time.ToString(@"mm\:ss\:fff");

            //ShowScore(PhotonNetwork.LocalPlayer);
            ///Apply Score
            ///Apply Time
            //scoreText.text = cScore.ToString("0");
            //stoptimeText.text = time.ToString(@"mm\:ss\:fff");

            //Ping
            PING.text = "PING : " + PhotonNetwork.GetPing();

            //HighScore
            if (Score > PlayerPrefs.GetFloat("hiScore", 0))
            {
                PlayerPrefs.SetFloat("hiScore", Score);
                Debug.Log("HighScore =  " + HighScoreFloat);
            }
            SaveHScore();
       // }
       // else
       // {
        //    StopTimer();
 
        //    Destroy(gameObject);
       // }
    }

    void ShowScore(Player _player)
    {
        _player.SetRealScore(cScore);
        scoreText.text = _player.GetRealScore().ToString();
    }

    public void StartTimer()
    {
        stopwatchActive = true;
    }

    public void StopTimer()
    {
        stopwatchActive = false;
    }

    public void ResetPlayer()
    {
        PlayerPrefs.DeleteKey("hiScore");
        //PhotonNetwork.LocalPlayer.SetScore(0);
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, 0);
        Debug.Log("HighScore Deleted");
    }

    public void SaveHScore()
    {
        //PlayerPrefs.SetFloat("hiScore", playertp.position.z);
        Score = cScore;
        //Debug.Log("Score is " + Score);
        IncreaseScore();
    }
    /*
     public void CheckH()
     {
         if (playertp.position.z > /*PlayerPrefs.GetFloat("HighScore") HighScoreFloat)
         {
             //PlayerPrefs.SetFloat("HighScore", playertp.position.z);
             // HighScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
             // HighScore.text = HighScoreFloat.ToString("0");
             //Debug.Log("Setting Highscore");
             SaveHScore();
         }
     }*/

    public void IncreaseScore()
    {
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        //PhotonNetwork.LocalPlayer.SetScore(HighScoreFloat);
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
        //Debug.Log("Score saved in ScoreBoard");
        //Debug.Log("hScore = " + HighScoreFloat);
    }

}
