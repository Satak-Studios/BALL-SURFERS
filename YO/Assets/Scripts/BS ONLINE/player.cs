using System;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using Photon.Pun.UtilityScripts;

public class player : MonoBehaviour
{
    public Text scoreText;
    public Text PING;
    public int HighScoreFloat;

    public float Score;
    public float cScore;

    private void Start()
    {
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
    }

    // Update is called once per frame
    public void Update()
    {
        if (FindObjectOfType<PlayerOnline>() != null)
        {
            cScore = FindObjectOfType<PlayerOnline>().gameObject.transform.position.z;
        }
        int _score = (int) cScore;
        scoreText.text = _score.ToString();

        //Ping
        PING.text = "PING : " + PhotonNetwork.GetPing();

        //HighScore
        if (Score > PlayerPrefs.GetFloat("hiScore", 0))
        {
            PlayerPrefs.SetFloat("hiScore", Score);
        }
        SaveHScore();
    }

    public void ResetPlayer()
    {
        PlayerPrefs.DeleteKey("hiScore");
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, 0);
    }

    public void SaveHScore()
    {
        Score = cScore;
        IncreaseScore();
    }
    
    public void IncreaseScore()
    {
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
    }

}
