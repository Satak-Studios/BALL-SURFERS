using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField username;
    public Text buttonText;


    public void OnClick()
    {
        if (username.text.Length >= 1)
        {
            PhotonNetwork.NickName = username.text;
            buttonText.text = "Connecting....";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
        Debug.Log("Connected To Master");
    }
   // public override void OnJoinedLobby()
    //{
   //     SceneManager.LoadScene("Find Room");
   //     Debug.Log("Joined The Lobby");
   //4 }
    
        
    
}
