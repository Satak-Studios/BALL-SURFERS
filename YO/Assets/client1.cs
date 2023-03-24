using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Menus
{
    public class client1 : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject findOppenentPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private Text waitingStatusText = null;

        private bool isConnecting = false;

        private const string GameVersion = "1.0";
        private const int MaxPlayersPerRoom = 4;

         private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;
        
        public void FindOpponenet()
        {
            isConnecting = true;

            findOppenentPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Searching...";
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatusPanel.SetActive(false);
            findOppenentPanel.SetActive(true);

            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients are waiting for an opponent, creating a new room");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Client successfully joined a room");

            int playerConunt = PhotonNetwork.CurrentRoom.PlayerCount;

            if(playerConunt != MaxPlayersPerRoom)
            {
                waitingStatusText.text = "Waiting For Opponent";
                Debug.Log("Client is waiting for an oppenent");
            }
            else
            {
                waitingStatusText.text = "Oppenent Found";
                Debug.Log("Maching is ready to begin");
            }
        }

        public override void OnPlayerEnteredRoom(Realtime.Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                Debug.Log("Match is ready to begin");
                waitingStatusText.text = "Opponent Found";

                PhotonNetwork.LoadLevel("create");
               
            }
        }
    }

}




