using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Satak.Utilities;


public class CompTimer : MonoBehaviour
{
    public bool Time_ = false;
    float currentTime = 0f;
    [SerializeField] Text compTimeText;

    // Update is called once per frame
    void Update()
    {
        if (Time_ == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        compTimeText.text = "Time Taken: " + time.ToString(@"mm\:ss\:fff");
    }

    public void StartTimer()
    {
        Time_ = true;
    }

    public void StopTimer()
    {
        Time_ = false;
        compTimeText.gameObject.SetActive(false);
        SatakExtensions.SetTime(PhotonNetwork.LocalPlayer, currentTime);
    }
    
    public void SaveTime(int newTime)
    {
        Time_ = false;
        compTimeText.gameObject.SetActive(false);
        SatakExtensions.SetTime(PhotonNetwork.LocalPlayer, newTime);
    }
}
