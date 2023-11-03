using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Satak.Utilities;

public class Timer : MonoBehaviour
{
    public bool Time_ = false;
    float currentTime = 0f;
    [SerializeField] Text countDownText;

    private static Timer instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Time_ == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        countDownText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartTimer()
    {
        Time_ = true;
    }

    public void StopTimer()
    {
        Time_ = false;
        SatakExtensions.SetTime(PhotonNetwork.LocalPlayer, currentTime);
    }
}
