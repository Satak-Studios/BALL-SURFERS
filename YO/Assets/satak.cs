using System.Collections;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class satak : MonoBehaviourPunCallbacks
{
    
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;
    [SerializeField] private GameObject ready2;
    [SerializeField] private GameObject ready3;
    [SerializeField] private GameObject ready4;

    [SerializeField] private Button start;
    [SerializeField] private GameObject start1;
    [SerializeField] private GameObject redton;

    
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    



    private void Update()
    {
        start1.SetActive(false);
        redton.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            start1.SetActive(true);
            redton.SetActive(false);
        }
    }


    public void OnPlayerConnected()
    {
        int playerConunt = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerConunt != 2)
        {
            player2.SetActive(true);
            text2.text = photonView.Owner.NickName;
            start.interactable = (false);
        }
    }
    public void player()
    {
        int playerConunt = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerConunt != 3)
        {
            player3.SetActive(true);
            text3.text = photonView.Owner.NickName;
            start.interactable = (false);
        }
    }
    public void bla()
    {
        int playerConunt = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerConunt != 4)
        {
            player1.SetActive(true);
            text4.text = photonView.Owner.NickName;
            start.interactable = (true);
        }
    }
    public void yo()
    {
        int playerConunt = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerConunt != 1)
        {
            player1.SetActive(true);
            text3.text = photonView.Owner.NickName;
            start.interactable = (false);
            
        }
    }
}


