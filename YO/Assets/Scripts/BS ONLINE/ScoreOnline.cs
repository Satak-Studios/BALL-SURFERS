 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Satak.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreOnline  : MonoBehaviourPunCallbacks
{ 
    public GameObject cRoomPanel;
    public GameObject fRoomPanel;
    public GameObject mainPanel;
    public InputField roomIF;
    //public InputField pName;
    public Text GM_txt;

    public InputField NameField;
    public string[] names;
    public LobbyManager1 lManager;

    //Room Options
    public Dropdown GM;
    public int gameMode;
    public GameObject Comp;
    public GameObject Custom;
    public Dropdown Players;
    public byte players;
    public Dropdown Lives;
    public int life;

    //Debugging
    public int gameMode_test;
    public bool testing = false;

    public void Start()
    {
        if (testing)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Host()
    {
        cRoomPanel.SetActive(true);
        fRoomPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public void Public()
    {
        cRoomPanel.SetActive(false);
        fRoomPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void OnClickCreate()
    {
        if (roomIF.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomIF.text, new RoomOptions() {
                MaxPlayers = players, 
                BroadcastPropsChangeToAll = true 
            });
            SatakExtensions.SetGM(PhotonNetwork.LocalPlayer, gameMode);
            SatakExtensions.SetLives(PhotonNetwork.LocalPlayer, life);
            cRoomPanel.SetActive(false);
        }
        else
        {
            FindObjectOfType<ErrorThrower>().ThrowError("0x1234", "Make sure that you have entered Room Name", "Error");
            Host();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (testing)
        {
            PhotonNetwork.JoinLobby();
            Invoke("CreateRoomTest", 2f);
        }
    }

    void CreateRoomTest()
    {
        PhotonNetwork.CreateRoom("TestingTheRoom", new RoomOptions() {
            MaxPlayers = 4, 
            BroadcastPropsChangeToAll = true 
        });
        SatakExtensions.SetGM(PhotonNetwork.LocalPlayer, gameMode_test);
        SatakExtensions.SetLives(PhotonNetwork.LocalPlayer, life);
        cRoomPanel.SetActive(false);
    }

    private void Update()
    {
        //pName.text = PhotonNetwork.NickName;
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 1)
        {
            GM_txt.text = "GameMode: Default";
        }
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 2)
        {
            GM_txt.text = "GameMode: Competition";
        }
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 3)
        {
            GM_txt.text = "GameMode: Custom";
        }

        #region GM
        if (GM.value == 0)
        {
            gameMode = 1;
            Comp.SetActive(false);
            Custom.SetActive(false);
            Players.interactable = true;
        }
        if (GM.value == 1)
        {
            gameMode = 2;
            Comp.SetActive(true);
            Custom.SetActive(false);
            Players.interactable = false;
            players = 4;
        }
        if (GM.value == 2)
        {
            gameMode = 3;
            Comp.SetActive(true);
            Custom.SetActive(true);
            Players.interactable = true;
        }
        #endregion

        #region Players
        if (Players.value == 0)
        {
            players = 4;
        }

        if (Players.value == 1)
        {
            players = 3;
        }
        #endregion

        #region lives
        if (Lives.value == 0)
        {
            life = 1;
        }

        if (Lives.value == 1)
        {
            life = 2;
        }

        if (Lives.value == 2)
        {
            life = 3;
        }
#endregion

        if (PhotonNetwork.InRoom == true)
        {
            lManager.roomPanel.SetActive(true);
        }
        else
        {
            lManager.roomPanel.SetActive(false);
        }
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", NameField.text);
        PhotonNetwork.NickName = NameField.text;
    }

    public void BackToHome()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
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
    #endregion
}

namespace Satak.Utilities
{
    public class SatakOnline : MonoBehaviour
    {
        public const string GM_Prop = "gMode";
        public const string LifeProp = "Lives";
        public const string LevelProp = "Level";
        public const string highScore = "hScore";
        public const string badge = "badge";
        public const string PlayerPosition = "PlayerPosition";
        public const string Time = "Time";
        public const string CD_Time = "CD_Time";
        public const string GameStatus = "GameStatus";
        public const string Retries_Comp = "Retries";
    }

    public static class SatakExtensions
    {
        //GameMode
        #region GameMode
        public static void SetGM(this Player player, int newGM)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.GM_Prop] = newGM;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetGM(this Player player)
        {
            object score;
            if (player.CustomProperties.TryGetValue(SatakOnline.GM_Prop, out score))
            {
                return (int)score;
            }

            return 0;
        }
        #endregion

        //Levels Completed
        #region Levels Completed
        public static void SetLevel(this Player player, int newLevel)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.LevelProp] = newLevel;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetLevel(this Player player)
        {
            object Lives;
            if (player.CustomProperties.TryGetValue(SatakOnline.LevelProp, out Lives))
            {
                return (int)Lives;
            }

            return 0;
        }
        #endregion

        //Lives
        #region Lives
        public static void SetLives(this Player player, int newLife)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.LifeProp] = newLife;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetLives(this Player player)
        {
            object Lives;
            if (player.CustomProperties.TryGetValue(SatakOnline.LifeProp, out Lives))
            {
                return (int)Lives;
            }

            return 0;
        }
        #endregion

        //Score
        #region Score
        public static void SetScore(this Player player, int amount)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.highScore] = amount;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetScore(this Player player)
        {
            object Score;
            if (player.CustomProperties.TryGetValue(SatakOnline.highScore, out Score))
            {
                return (int)Score;
            }

            return 0;
        }
        #endregion

        //Badge
        #region Badge
        public static void SetBadge(this Player player, string WhatBadge)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.badge] = WhatBadge;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static string GetBadge(this Player player)
        {
            object Badge;
            if (player.CustomProperties.TryGetValue(SatakOnline.badge, out Badge))
            {
                return Badge.ToString();
            }

            return "";
        }

        #endregion

        //PlayerPosition
        #region PlayerPosition

        public static void SetPlayerPosition(this Player player, int WhatPlayerPosition)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.PlayerPosition] = WhatPlayerPosition;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static void AddPlayerPosition(this Player player, int AmountToAdd)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.PlayerPosition] = GetPlayerPosition(player) + AmountToAdd;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetPlayerPosition(this Player player)
        {
            object PlayerPosition;
            if (player.CustomProperties.TryGetValue(SatakOnline.PlayerPosition, out PlayerPosition))
            {
                return (int)PlayerPosition;
            }

            return 0;
        }

        #endregion

        //Time
        #region Time
        public static void SetTime(this Player player, float time)
        {
            Hashtable _time = new Hashtable();  // using PUN's implementation of Hashtable
            _time[SatakOnline.Time] = time;

            player.SetCustomProperties(_time);  // this locally sets the score and will sync it in-game asap.
        }

        public static float GetTime(this Player player)
        {
            object Time;
            if (player.CustomProperties.TryGetValue(SatakOnline.Time, out Time))
            {
                return (float)Time;
            }

            return 0;
        }
        #endregion

        //CountDownTime
        #region CountDownTime
        public static void SetCountDownTime(this Player player, int cdTime)
        {
            Hashtable cd_time_ = new Hashtable();  // using PUN's implementation of Hashtable
            cd_time_[SatakOnline.CD_Time] = cdTime;

            player.SetCustomProperties(cd_time_);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetCountDownTime(this Player player)
        {
            object CD_Time;
            if (player.CustomProperties.TryGetValue(SatakOnline.CD_Time, out CD_Time))
            {
                return (int)CD_Time;
            }

            return 0;
        }
        #endregion

        //GameStatus
        #region GameStatus

        public static void SetGameStatus(this Player player, int _gameStaus)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.GameStatus] = _gameStaus;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetGameStatus(this Player player)
        {
            object GameStatus;
            if (player.CustomProperties.TryGetValue(SatakOnline.GameStatus, out GameStatus))
            {
                return (int)GameStatus;
            }

            return 0;
        }

        #endregion

        //Retries Comp
        #region Retries_Comp

        public static void SetCompRetries(this Player player, int _compRetries)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.Retries_Comp] = _compRetries;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetCompRetries(this Player player)
        {
            object CompRetries;
            if (player.CustomProperties.TryGetValue(SatakOnline.Retries_Comp, out CompRetries))
            {
                return (int)CompRetries;
            }

            return 0;
        }

        #endregion
    }
}
