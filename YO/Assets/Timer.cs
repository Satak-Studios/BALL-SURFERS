using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//using BallSurfersOnlineMovement;

public class Timer : MonoBehaviour
{
    public bool Level01 = false;
    bool Time_ = false;
    float currentTime = 0f;
    //public float startingTime = 10f;
    [SerializeField] Text countDownText;
    public GameObject tOBJ;

    //public PlayerSpawner PS;
    //public GameObject RECAM;

    //public GameObject Player;
    //public GameObject FakePlayer;

    //public Transform SpawnPoint;

    // Start is called before the first frame update
    public void Start()
    {
        currentTime = PlayerPrefs.GetFloat("cTimer", 0);
        //SpeedRun On or Off
        if (PlayerPrefs.GetInt("sdrn") == 1 && PhotonNetwork.InRoom == false)
        {
            //PlayerPrefs.DeleteKey("cTimer");
            PlayerPrefs.SetFloat("cTimer", currentTime);
            tOBJ.SetActive(true);
            StartTimer();
        }
        if (PlayerPrefs.GetInt("sdrn") == 0 && PhotonNetwork.InRoom == false)
        {
            tOBJ.SetActive(false);
            StopTimer();
            PlayerPrefs.SetFloat("cTimer", 0);
        }

        Debug.Log("sdrn = " + PlayerPrefs.GetInt("sdrn"));
        /*if (Level01 == true)
        {
            PlayerPrefs.DeleteKey("cTimer");
        }*/
    }

    // Update is called once per frame
    void Update()
    { 
        if (Time_ == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //countDownText.text = currentTime.ToString("0");
        countDownText.text = time.ToString(@"mm\:ss\:fff");

        //SpeedRun On or Off
        if(PlayerPrefs.GetInt("sdrn") == 1 && PhotonNetwork.InRoom == false)
        {
            //PlayerPrefs.DeleteKey("cTimer");
            PlayerPrefs.SetFloat("cTimer", currentTime);
            tOBJ.SetActive(true);
            StartTimer();
        }
        if (PlayerPrefs.GetInt("sdrn") == 0 && PhotonNetwork.InRoom == false)
        {
            tOBJ.SetActive(false);
            StopTimer();
        }
    }

    public void StartTimer()
    {
        Time_ = true;
    }

    public void StopTimer()
    {
        Time_ = false;
        PlayerPrefs.SetFloat("cTimer", 0);
        /*if (currentTime < PlayerPrefs.GetFloat("hiSN", 0))
        {
            PlayerPrefs.SetFloat("hiSN", currentTime);
            Debug.Log("New SpeedRun =  " + currentTime);
        }
        */
    }
}
