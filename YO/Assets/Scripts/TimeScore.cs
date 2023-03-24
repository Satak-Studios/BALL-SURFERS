using System;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

public class TimerScore  : MonoBehaviour
{
    private float playertp;
    public Text scoreText;
    public Text PING;
    //public Text HighScore;
    //public Text hscore;
    public int HighScoreFloat;
    public float Score;

    int cScore;
    float currentTime;
    bool stopwatchActive = false;
    public float multiplayer = 1;
    //public RestartOnline RSO;
    //public Player _player;
    private PhotonView PV;
    public bool isMine;

    private void Start()
    {
        if (isMine == true)
        {
            currentTime = 0;
        }
            PV = FindObjectOfType<PlayerOnline>().PV;
    }

    // Update is called once per frame
    public void Update()
    {
        if (PV.IsMine)
        {
            if (stopwatchActive == true)
            {
                currentTime = currentTime + Time.deltaTime;
            }
            cScore = Mathf.RoundToInt(currentTime * multiplayer);
            TimeSpan time = TimeSpan.FromSeconds(currentTime);

            Player _player = PhotonNetwork.LocalPlayer;
            playertp = currentTime;
            _player.SetRealScore(playertp);
            float number = _player.GetRealScore();
            scoreText.text = number.ToString("0");
            PING.text = "PING : " + PhotonNetwork.GetPing();

            if (Score > PlayerPrefs.GetFloat("hiScore", 0))
            {
                PlayerPrefs.SetFloat("hiScore", Score);
                Debug.Log("HighScore =  " + HighScoreFloat);
            }
            SaveHScore();
        }
    }

    public void ResetPlayer()
    {
        PlayerPrefs.DeleteKey("hiScore");
        PhotonNetwork.LocalPlayer.SetScore(0);
        Debug.Log("HighScore Deleted");
    }
    
    public void SaveHScore()
    {
        //PlayerPrefs.SetFloat("hiScore", playertp.position.z);
        Score = playertp;
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
        PhotonNetwork.LocalPlayer.SetScore(HighScoreFloat);
        //Debug.Log("Score saved in ScoreBoard");
        //Debug.Log("hScore = " + HighScoreFloat);
    }
}
