using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Satak.Utilities;

public class PlayerItem1 : MonoBehaviourPunCallbacks
{
    public Text playerName;

    public Text Devicename;

    public Player playerr;

    public Player Player => m_player;
    //public int Score => m_player.GetScore();

    private Player m_player;
    // public Image backgroundImage;
    // public Color highlightColor;
    // public GameObject leftArrowButton;
    //public GameObject rightArrowButton;
    public Text Rank;
    public int HighScoreFloat;

    public Image pBG;

    //public PhotonView PV;
    public string deviceType;
    public string deviceOS;
    public string deviceOSfamily;

    ExitGames.Client.Photon.Hashtable playerScore = new ExitGames.Client.Photon.Hashtable();
    //public Image playerAvatar;
    //public Sprite[] avatars;


    private void Start()
    {
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        SatakExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
        int levelCompleted = PlayerPrefs.GetInt("levelsUnlocked", 1);
        SatakExtensions.SetLevel(PhotonNetwork.LocalPlayer, levelCompleted);
    }
    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        playerr = _player;
        m_player = _player;
        deviceType = SystemInfo.deviceType.ToString();
        deviceOS = SystemInfo.operatingSystem;
        deviceOSfamily = SystemInfo.operatingSystemFamily.ToString();//UpdatePlayer(playerr);
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        Rank.text = SatakExtensions.GetScore(_player).ToString();
        Devicename.text = SatakExtensions.GetLevel(_player).ToString();
    }

    public void ApplyLocalChanges()
    {
        // backgroundImage.color = highlightColor;
        //leftArrowButton.SetActive(true);
        //rightArrowButton.SetActive(true);
        pBG.color = Color.red;
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (playerr == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerScore"))
        {
            //Rank.text = player.CustomProperties["playerScore"].ToString();
            playerScore["playerScore"] = (int)player.CustomProperties["playerScore"];
        }
        else
        {
            playerScore["playerScore"] = 0;
        }
    }    
}
