using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Satak.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Utilities.Debug;

public class LobbyManager1 : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public GameObject roomFPanel;
    public Text roomName;

    public RoomItem roomItemPrfab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    List<PlayerItem1> playerItemsList = new List<PlayerItem1>();
    public PlayerItem1 playerItemPrefab;
    public Transform playerItemParent;

    public GameObject StartButton;
    public GameObject BackButton;

    public GameObject HScorePanel;
    public GameObject LoadingCanvas;

    //Syncing
    public ScoreOnline scoreOnline;

    // Start is called before the first frame update
    private void Start()
    {
        //isPlaying = false;
        PhotonNetwork.JoinLobby();
        if (PhotonNetwork.InRoom)
        {
            LoadRoomProps();
        }
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() {MaxPlayers = 8, BroadcastPropsChangeToAll = true});
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerListInRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrfab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
            Debug.Log("Refreshing RoomList");
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerListInRoom()
        {
            foreach (PlayerItem1 item in playerItemsList)
            {
                Destroy(item.gameObject);
            }
            playerItemsList.Clear();
            

            if (PhotonNetwork.CurrentRoom == null)
            {
                return;
            }

        foreach (KeyValuePair<int, Player> player_ in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem1 newPlayerItem1 = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem1.SetPlayerInfo(player_.Value);

            if (player_.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem1.ApplyLocalChanges();
            }
            playerItemsList.Add(newPlayerItem1);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerListInRoom();
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        UpdatePlayerListInRoom();
    }

    private void Update()
    {
         if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }

        if (PhotonNetwork.InRoom == true)
        {
            roomPanel.SetActive(true);
            lobbyPanel.SetActive(false);
            roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
            BackButton.SetActive(false);
            UpdatePlayerListInRoom();
        }
        else
        {
            roomPanel.SetActive(false);
            BackButton.SetActive(true);
        }
    }

    public void OnClickStartButton()
    {
        switch (scoreOnline.gameMode)
        {
            case 1:
                LoadLevel("Game");
                break;
            case 2:
                LoadLevel("Comp");
                break;
            case 3:
                switch (scoreOnline.mapType)
                {
                    case 1: LoadLevel("Custom"); break;
                    case 2: LoadLevel("CrazyOnline"); break;
                    case 3: LoadLevel("Custom_Easy"); break;
                    case 4: LoadLevel("Custom_Hard"); break;
                    default:
                        LoadLevel("Custom"); break;
                }
                break;
        }
    }

    private void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("GameMode"))
        {
            scoreOnline.gameMode = (int)propertiesThatChanged["GameMode"];
        }

        if (propertiesThatChanged.ContainsKey("MapType"))
        {
            scoreOnline.mapType = (int)propertiesThatChanged["MapType"];
        }

        if (propertiesThatChanged.ContainsKey("Lives"))
        {
            scoreOnline.life = (int)propertiesThatChanged["Lives"];
        }

        if (propertiesThatChanged.ContainsKey("fSpeedComp"))
        {
            scoreOnline.fSpeed = (int)propertiesThatChanged["fSpeedComp"];
        }
        
        if (propertiesThatChanged.ContainsKey("fSpeedCustom"))
        {
            scoreOnline.fSpeedCustom = (int)propertiesThatChanged["fSpeedCustom"];
        }

        if (propertiesThatChanged.ContainsKey("sSpeedComp"))
        {
            scoreOnline.sSpeed = (int)propertiesThatChanged["sSpeedComp"];
        }
        
        if (propertiesThatChanged.ContainsKey("sSpeedCustom"))
        {
            scoreOnline.sSpeedCustom = (int)propertiesThatChanged["sSpeedCustom"];
        }

        SaveRoomProps();
    }
    public void SaveRoomProps()
    {
        PlayerPrefs.SetInt("fSpeedComp", scoreOnline.fSpeed);
        PlayerPrefs.SetInt("sSpeedComp", scoreOnline.sSpeed);
        PlayerPrefs.SetInt("fSpeedCustom", scoreOnline.fSpeedCustom);
        PlayerPrefs.SetInt("sSpeedCustom", scoreOnline.sSpeedCustom);
        PlayerPrefs.SetInt("MapType", scoreOnline.mapType);
        PlayerPrefs.SetInt("Lives", scoreOnline.life);
        PlayerPrefs.SetInt("GameMode", scoreOnline.gameMode);

        PlayerPrefs.Save();
    }

    private void LoadRoomProps()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("GameMode"))
            {
                scoreOnline.gameMode = (int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("MapType"))
            {
                scoreOnline.mapType = (int)PhotonNetwork.CurrentRoom.CustomProperties["MapType"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Lives"))
            {
                scoreOnline.life = (int)PhotonNetwork.CurrentRoom.CustomProperties["Lives"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("fSpeedComp"))
            {
                scoreOnline.fSpeed = (int)PhotonNetwork.CurrentRoom.CustomProperties["fSpeedComp"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("sSpeedComp"))
            {
                scoreOnline.sSpeed = (int)PhotonNetwork.CurrentRoom.CustomProperties["sSpeedComp"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("fSpeedCustom"))
            {
                scoreOnline.fSpeedCustom = (int)PhotonNetwork.CurrentRoom.CustomProperties["fSpeedCustom"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("sSpeedCustom"))
            {
                scoreOnline.sSpeedCustom = (int)PhotonNetwork.CurrentRoom.CustomProperties["sSpeedCustom"];
            }
        }
    }


    public void LeaderBoard()
    {
        HScorePanel.SetActive(true);
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }

    public void mMenu()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu 1");
    }

    #region Displaying Errors

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        FindObjectOfType<ErrorThrower>().ThrowError(returnCode.ToString(), message, "Error");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        FindObjectOfType<ErrorThrower>().ThrowError(returnCode.ToString(), message, "Error");
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        FindObjectOfType<ErrorThrower>().ThrowError("0x0000", errorInfo.ToString(), "Error");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        FindObjectOfType<ErrorThrower>().ThrowError("0x4231", "Disconnected, Make sure you have proper internet connection.", "Error");
    }
    #endregion
}

