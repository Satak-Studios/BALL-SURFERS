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

    public Text LevelsUnlocked;

    public Text _Status;

    public Player playerr;

    public Text HighScore;
    public int HighScoreFloat;

    public Player Player => m_player;
    private Player m_player;

    ExitGames.Client.Photon.Hashtable playerScore = new ExitGames.Client.Photon.Hashtable();


    private void Start()
    {
        CalcXP(PhotonNetwork.LocalPlayer);
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore", 1);
        SatakExtensions.SetScore(PhotonNetwork.LocalPlayer, HighScoreFloat);
        int levelCompleted = PlayerPrefs.GetInt("levelsUnlocked", 1);
        SatakExtensions.SetLevel(PhotonNetwork.LocalPlayer, levelCompleted);
    }
    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        playerr = _player;
        m_player = _player;

        HighScore.text = SatakExtensions.GetScore(_player).ToString();
        _Status.text = SatakExtensions.GetStatus(_player);
        LevelsUnlocked.text = SatakExtensions.GetLevel(_player).ToString();
    }

    //Depriciated
    public void ApplyLocalChanges()
    {

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
            playerScore["playerScore"] = (int)player.CustomProperties["playerScore"];
        }
        else
        {
            playerScore["playerScore"] = 0;
        }
    }

    void CalcXP(Player _player)
    {
        int achievements = PlayerPrefs.GetInt("totalAch");
        int levels = PlayerPrefs.GetInt("levelsUnlocked", 1);
        int hScore = (int)PlayerPrefs.GetFloat("hiScore", 1);
        int XP = achievements * levels * hScore / 2;
        string Status = "Error";
        if (XP >= 1 && XP < 500)
        {
            Status = "NewBie";
        }

        if (XP >= 500)
        {
            Status = "Causual";
        }

        if (XP >= 1100)
        {
            Status = "Intermediate";
        }

        if (XP >= 10000)
        {
            Status = "Advanced";
        }

        if (XP >= 15000)
        {
            Status = "Expert";
        }

        if (XP >= 25000)
        {
            Status = "Master";
        }

        if (XP >= 75000)
        {
            Status = "GrandMaster";
        }

        if (XP >= 225000)
        {
            Status = "Legend";
        }
        SatakExtensions.SetStatus(PhotonNetwork.LocalPlayer, Status);
    }
}
