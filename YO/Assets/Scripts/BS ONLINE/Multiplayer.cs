﻿
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Multiplayer : MonoBehaviourPunCallbacks
{
    public Text status;
    public string Lobby;
    public Transform pointer;

    public void onStart()
    {
        PhotonNetwork.ConnectUsingSettings();
        status.text = "Connecting....";
        PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        PhotonNetwork.AutomaticallySyncScene = true;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Touch touch = Input.GetTouch(0);
            pointer.position = touch.position;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(Lobby);
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        status.text = errorInfo.Info;
    }
}
