using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    LobbyManager1 manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager1>();  
        Debug.Log("LobbyManager Found!");
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}

