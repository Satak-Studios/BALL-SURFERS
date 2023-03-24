using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using Photon.Pun.UtilityScripts;

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
        //PV = FindObjectOfType<PhotonView>();
        // int randColor = Random.Range(0, avatars.Length);
        //playerProperties["playerAvatar"] = avatars[randColor];
        // playerAvatar.sprite = avatars[randColor];
        // Debug.Log("Your Character Is " + avatars[randColor].name);\
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        //ScoreExtensions.SetDevice(m_player, deviceOSfamily);
        //PhotonNetwork.LocalPlayer.SetScore(HighScoreFloat);
        //Rank.text = HighScoreFloat.ToString();
        //ScoreExtensions.SetScore(Player,HighScoreFloat);
        ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
        //Debug.Log("Your HighScore is " + HighScoreFloat);
        //playerr.SetScore(HighScoreFloat);

        /*if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            //_player.SetDevice(_player, "PC");
            ScoreExtensions.SetDevice(m_player, "PC");
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            //_player.SetDevice("Mobile");
            ScoreExtensions.SetDevice(m_player, "Mobile");
        }

        if (SystemInfo.deviceType == DeviceType.Unknown)
        {
            //_player.SetDevice("Unknown");
            ScoreExtensions.SetDevice(m_player, "Unknown");
        }*/
        int levelCompleted = PlayerPrefs.GetInt("levelsUnlocked", 1);
        ScoreExtensions.SetLevel(PhotonNetwork.LocalPlayer, levelCompleted);

        //Debug.Log("Your Operating System is " + SystemInfo.operatingSystemFamily);
        //Debug.Log("Your Device is " + SystemInfo.deviceType.ToString());
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
        Rank.text = ScoreExtensions.GetScore(_player).ToString();
        //Devicename.text = ScoreExtensions.GetDevice(_player).ToString();
        Devicename.text = ScoreExtensions.GetLevel(_player).ToString();

        /*if (PV.IsMine == true)
        {
            /*if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                //_player.SetDevice(_player, "PC");
                ScoreExtensions.SetDevice(_player, "PC");
            }

            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                //_player.SetDevice("Mobile");
                ScoreExtensions.SetDevice(_player, "Mobile");
                }

            if (SystemInfo.deviceType == DeviceType.Unknown)
            {
                //_player.SetDevice("Unknown");
                ScoreExtensions.SetDevice(_player, "Unknown");
            }*/
        //}
    }
    
    public void ApplyLocalChanges()
    {
        // backgroundImage.color = highlightColor;
        //leftArrowButton.SetActive(true);
        //rightArrowButton.SetActive(true);
        pBG.color = Color.red;
    }
    /*
    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        Debug.Log("Left");
    }

    public void OnClickRighttArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        Debug.Log("Right");
    }
  */ 
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
   
    void UpdatePlayer(Player player_)
    {
        if (player_.IsLocal == true)
        {
            //int HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
            //PhotonNetwork.LocalPlayer.SetScore(HighScoreFloat);
            //int HighScore = player_.GetScore();
            /* if (PhotonNetwork.LocalPlayer.GetScore() == 0)
             {
                 Rank.text = "0";
             }
             else
             {
                 Rank.text = PhotonNetwork.LocalPlayer.GetScore().ToString("0");
             }
             //PhotonNetwork.SetPlayerCustomProperties();
             //Rank.text = PlayerPrefs.GetInt("high").ToString();
            */
            //Rank.text = player.GetScore().ToString();
        }
        //Rank.text = $"{m_player.GetScore()}";
        //Rank.text = HighScoreFloat.ToString();
        //Rank.text = PhotonNetwork.SetPlayerCustomProperties();
        //Devicename.text = ScoreExtensions.GetDevice(player_).ToString();//m_player.GetDevice().ToString(); 
    }
    
}
