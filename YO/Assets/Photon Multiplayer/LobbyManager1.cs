using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager1 : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public RoomItem roomItemPrfab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    List<PlayerItem1> playerItemsList = new List<PlayerItem1>();
    public PlayerItem1 playerItemPrefab;
    //public PlayerItem1 playerItem_prefab_1_retry_;
    public Transform playerItemParent;

    public GameObject StartButton;
    public GameObject BackButton;
    public bool isPlaying = false;
    public Transform Playing;
    public GameObject PlayingPrefab;

    public GameObject HScorePanel;
    public GameObject LoadingCanvas;

    ExitGames.Client.Photon.Hashtable gameStarted = new ExitGames.Client.Photon.Hashtable();

    //Errors
    public GameObject errorObj;
    public Text errorText;
    public Text errorCode;

    //Chat
    public GameObject chatObj;
    public Transform chatPos;

    // Start is called before the first frame update
    private void Start()
    {
        //isPlaying = false;
        PhotonNetwork.JoinLobby();

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
        //Chat
        Instantiate(chatObj, chatPos);

        //Normal
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

        Destroy(chatObj);
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
            //PlayerItem1 newPlayerItem1 = Instantiate(playerItem_prefab_1_retry_, playerItemParent);
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
        if (isPlaying == true)
        {
            PhotonNetwork.Instantiate(PlayingPrefab.name, Playing.position, Quaternion.identity);
        }

        if (PhotonNetwork.IsMasterClient)// && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
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
            //lobbyPanel.SetActive(true);
            BackButton.SetActive(true);
        }
        //ExitGames.Client.Photon.Hashtable gameStarted = new ExitGames.Client.Photon.Hashtable();
       /* if ((int)gameStarted["Started"] == 1)
        {
            LoadingCanvas.SetActive(true);
            PhotonNetwork.Instantiate(PlayingPrefab.name, Playing.position, Quaternion.identity);
        }*/
    }

    public void OnClickStartButton()
    {
        PhotonNetwork.LoadLevel("Game");
        //isPlaying = true;
        //ExitGames.Client.Photon.Hashtable gameStarted = new ExitGames.Client.Photon.Hashtable();
        gameStarted["Started"] = 1;
        print(gameStarted["Started"]);
        PhotonNetwork.SetPlayerCustomProperties(gameStarted);
    }

    public void LeaderBoard()
    {
        //SceneManager.LoadScene("Scoreboard");
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

    public void ThrowError(string returnCode, string message)
    {
        errorObj.SetActive(true);
        errorCode.text = returnCode.ToString();
        errorText.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ThrowError(returnCode.ToString(), message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ThrowError(returnCode.ToString(), message);
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        ThrowError("0x0000", errorInfo.ToString());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ThrowError("0x4231", "Disconnected, Make sure you have proper internet connection.");
    }
    #endregion
}

