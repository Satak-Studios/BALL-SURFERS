 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Satak.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreOnline : MonoBehaviourPunCallbacks
{
    public GameObject cRoomPanel;
    public GameObject fRoomPanel;
    public GameObject mainPanel;
    public InputField roomIF;
    public Text GM_txt;
    public Text Map_txt;

    public Text PlayerName;
    public InputField nameField;
    public string[] names;
    public LobbyManager1 lManager;

    //Room Options
    public int gameMode;
    public byte players;

    //Room Options
    public Dropdown GM;
    public Dropdown fSpeedDD;
    public Dropdown sSpeedDD;
    public Dropdown[] Custom;
    public GameObject CompObj;
    public GameObject CustomObj;
    public int life;
    public int fSpeed;
    public int sSpeed;
    public int fSpeedCustom;
    public int sSpeedCustom;
    public int defaulFSpeed;
    public int defaultSSpeed;
    public int mapType;

    //RoomPanelDetails
    public Text[] RoomDetails;
    public string[] randRoomName = {
    "Noob'sPalace",
    "CelestialChamber",
    "LuminousLounge",
    "DimensionDen",
    "ZenZone",
    "AuroraAbode",
    "InfinityInn",
    "ElysianEdifice",
    "SpectralSanctum",
    "HarmonyHaven",
    "PhoenixParlor",
    "EtherealEmporium",
    "OdysseyOasis",
    "SolsticeSuite",
    "PrismPalace",
    "EchoEnclave",
    "ArcaneAsylum",
    "NexusNest",
    "TranquilityTower",
    "QuantumQuarters",
    "EdenExpanse",
    "MirageManor",
    "AstralApartment",
    "HavenHall",
    "RadiantRetreat",
    "VertexVilla",
    "DreamDwelling",
    "NimbusNook",
    "EssenceEscape",
    "EnchantedEnclosure",
    "UnityUtopia",
    "GalaxyGallery",
    "MysticMansion",
    "SanctuarySalon",
    "OracleOutpost",
    "SerenitySpace",
    "EtherealEstate",
    "WonderlandWard",
    "NebulaNest",
    "BlissBungalow",
    "SeraphimSuite",
    "ZodiacZephyr",
    "HarmonyHearth",
    "SolitudeSanctuary",
    "TranquilTurret",
    "CosmoCottage",
    "CelestialCell",
    "MirageMaze",
    "LuminaryLodge",
    "EmpyreanEden"
    };

    public void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            SetPlayerName();
        }
        else
        {
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
        }
        if (PhotonNetwork.InRoom)
        {
            LoadRoomProps();
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
                BroadcastPropsChangeToAll = true,
            });
            SetRoomProperties();
            cRoomPanel.SetActive(false);
        }
        else
        {
            if (PhotonNetwork.NickName.Length <= 11)
            {
                string roomName = PhotonNetwork.NickName + "'s Room";
                PhotonNetwork.CreateRoom(roomName, new RoomOptions()
                {
                    MaxPlayers = players,
                    BroadcastPropsChangeToAll = true,
                });
                SetRoomProperties();
                cRoomPanel.SetActive(false);
            }
            else
            {
                int randRoomN = Random.Range(0, randRoomName.Length - 1);
                PhotonNetwork.CreateRoom(randRoomName[randRoomN], new RoomOptions()
                {
                    MaxPlayers = players,
                    BroadcastPropsChangeToAll = true,
                });
                SetRoomProperties();
                cRoomPanel.SetActive(false);
            }
        }
    }

    #region RoomProps
    private void SetRoomProperties()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Hashtable customRoomProperties = new Hashtable();
            customRoomProperties.Add("GameMode", gameMode);
            customRoomProperties.Add("MapType", mapType);
            customRoomProperties.Add("Lives", life);   
            customRoomProperties.Add("fSpeedComp", fSpeed);   
            customRoomProperties.Add("fSpeedCustom", fSpeedCustom);   
            customRoomProperties.Add("sSpeedComp", sSpeed);   
            customRoomProperties.Add("sSpeedCustom", sSpeedCustom);   

            PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
        }
    }

    private void LoadRoomProps()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("GameMode"))
            {
                gameMode = (int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("MapType"))
            {
                mapType = (int)PhotonNetwork.CurrentRoom.CustomProperties["MapType"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Lives"))
            {
                life = (int)PhotonNetwork.CurrentRoom.CustomProperties["Lives"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("fSpeedComp"))
            {
                fSpeed = (int)PhotonNetwork.CurrentRoom.CustomProperties["fSpeedComp"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("sSpeedComp"))
            {
                sSpeed = (int)PhotonNetwork.CurrentRoom.CustomProperties["sSpeedComp"];
            }
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("fSpeedCustom"))
            {
                fSpeedCustom = (int)PhotonNetwork.CurrentRoom.CustomProperties["fSpeedCustom"];
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("sSpeedCustom"))
            {
                sSpeedCustom = (int)PhotonNetwork.CurrentRoom.CustomProperties["sSpeedCustom"];
            }
        }
    }
    #endregion

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.InRoom)
        {
            SetRoomProperties();
        }

        if (gameMode == 1)
        {
            GM_txt.text = "GameMode: Default";
            Map_txt.gameObject.SetActive(false);
            RoomDetails[0].gameObject.SetActive(false);
            RoomDetails[1].gameObject.SetActive(false);
            RoomDetails[2].gameObject.SetActive(false);
        }
        if (gameMode == 2)
        {
            GM_txt.text = "GameMode: Competition";
            Map_txt.gameObject.SetActive(false);
            RoomDetails[0].gameObject.SetActive(true);
            RoomDetails[1].gameObject.SetActive(true);
            RoomDetails[2].gameObject.SetActive(false);
            RoomDetails[0].text = "Forward Speed: x" + fSpeedCustom / defaulFSpeed;
            RoomDetails[1].text = "Side Speed: x" + sSpeedCustom / defaultSSpeed;
        }
        if (gameMode == 3)
        {
            GM_txt.text = "GameMode: Custom";
            Map_txt.gameObject.SetActive(true);
            RoomDetails[0].gameObject.SetActive(true);
            RoomDetails[1].gameObject.SetActive(true);
            RoomDetails[2].gameObject.SetActive(true);
            RoomDetails[0].text = "Forward Speed: x" + fSpeedCustom / defaulFSpeed;
            RoomDetails[1].text = "Side Speed: x" + sSpeedCustom / defaultSSpeed;
            if (life <= 3)
            {
                RoomDetails[2].text = "Lives: " + life;
            }
            else
            {
                RoomDetails[2].text = "Lives: ∞";
            }
        }

        #region MapType
        if (mapType == 1)
        {
            Map_txt.text = "MapType: Default";
        }
        if (mapType == 2)
        {
            Map_txt.text = "MapType: Crazy";
        }
        if (mapType == 3)
        {
            Map_txt.text = "MapType: Easy";
        }
        if (mapType == 4)
        {
            Map_txt.text = "MapType: Hard";
        }
#endregion

        #region GM
        if (GM.value == 0)
        {
            gameMode = 1;

            for (int i = 0; i < Custom.Length; i++)
            {
                Custom[i].gameObject.SetActive(false);
            }

            CompObj.SetActive(false);
            CustomObj.SetActive(false);
        }
        if (GM.value == 1)
        {
            gameMode = 2;
            for (int i = 0; i < Custom.Length; i++)
            {
                Custom[i].gameObject.SetActive(false);
            }

            CompObj.SetActive(true);
            CustomObj.SetActive(false);
        }
        if (GM.value == 2)
        {
            gameMode = 3;

            for (int i = 0; i < Custom.Length; i++)
            {
                Custom[i].gameObject.SetActive(true);
            }

            CompObj.SetActive(false);
            CustomObj.SetActive(true);
        }
        #endregion

        if (Custom[0].value == 3)
        {
            life = 4;
        }
        else
        {
            life = 3 - Custom[0].value;
        }

        fSpeed = (int)(defaulFSpeed * Mathf.Pow(2, fSpeedDD.value));
        sSpeed = (int)(defaultSSpeed * Mathf.Pow(2, sSpeedDD.value));

        fSpeedCustom = (int)(defaulFSpeed * Mathf.Pow(2, Custom[2].value));
        sSpeedCustom = (int)(defaultSSpeed * Mathf.Pow(2, Custom[3].value));

        mapType = Custom[1].value + 1;

        if (PhotonNetwork.InRoom == true)
        {
            lManager.roomPanel.SetActive(true);
            LoadRoomProps();
            RoomDetails[3].text = "Owner: " + PhotonNetwork.MasterClient.NickName;
        }
        else
        {
            lManager.roomPanel.SetActive(false);
        }
    }

    //Random PlayerName
    public void SetPlayerName()
    {
        string[] nouns = { "Gamer", "Explorer", "Adventurer", "Hero", "Champion", "Pioneer", "Detective", "Scholar", "Artist", "Musician", "Scientist", "Engineer", "Captain", "Pirate", "Wizard", "Warrior", "Athlete", "Leader", "Dreamer", "Traveler", "Nomad", "Guardian", "Hunter", "Knight", "Jester", "Acrobat", "Magician", "Guardian", "Gladiator", "Spy", "Sailor", "Astronaut", "Pirate", "Viking", "Explorer", "Samurai", "Ninja", "Archer", "Scribe", "Sage", "Gladiator" };
        int randNoun = Random.Range(0, nouns.Length);
        int rand = Random.Range(0, 10000);
        int randName = Random.Range(0, names.Length);
        string player_name = names[randName] + nouns[randNoun] + rand.ToString("0000");
        if (player_name.Length > 15)
        {
            string playerName = player_name.Substring(0, 15);
            PlayerPrefs.SetString("PlayerName", playerName);
            PhotonNetwork.NickName = playerName;
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", player_name);
            PhotonNetwork.NickName = player_name;
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void SavePlayerName()
    {
        if (nameField.text.Length >= 1)
        {
            PlayerPrefs.SetString("PlayerName", nameField.text);
            PhotonNetwork.NickName = nameField.text;
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            FindObjectOfType<ErrorThrower>().ThrowError("9898", "Your name cannot be empty", "Check!");
        }
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
        public const string LevelProp = "Level";
        public const string highScore = "hScore";
        public const string badge = "badge";
        public const string PlayerPosition = "PlayerPosition";
        public const string Time = "Time";
        public const string Player_Status = "PlayerStatus";
        public const string Player_Props_Eye = "PlayerEye";
        public const string Player_Props_Mouth = "PlayerMouth";
        public const string Player_Props_BodyColor = "PlayerBodyColor";
        public const string Player_Props_EyeColor = "PlayerEyeColor";
    }

    public static class SatakExtensions
    {
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

        //Time(Comp)
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

        //Status(Noob)
        #region PlayerStatus

        public static void SetStatus(this Player player, string _playerStatus)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[SatakOnline.Player_Status] = _playerStatus;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static string GetStatus(this Player player)
        {
            object PlayerStatus;
            if (player.CustomProperties.TryGetValue(SatakOnline.Player_Status, out PlayerStatus))
            {
                return PlayerStatus.ToString();
            }

            return "";
        }

        #endregion

        //PlayerProperties
        #region PlayerProperties

        public static void SetPlayerEyes(this Player player, int sEye)
        {
            Hashtable score = new Hashtable();
            score[SatakOnline.Player_Props_Eye] = sEye;
            player.SetCustomProperties(score);
        }

        public static int GetPlayerEyes(this Player player)
        {
            object PlayerEye;
            if (player.CustomProperties.TryGetValue(SatakOnline.Player_Props_Eye, out PlayerEye))
            {
                return (int)PlayerEye;
            }
            return 0;
        }

        //Mouth
        public static void SetPlayerMouth(this Player player, int sMouth)
        {
            Hashtable score = new Hashtable();
            score[SatakOnline.Player_Props_Mouth] = sMouth;
            player.SetCustomProperties(score);
        }

        public static int GetPlayerMouth(this Player player)
        {
            object PlayerMouth;           
            if (player.CustomProperties.TryGetValue(SatakOnline.Player_Props_Mouth, out PlayerMouth))
            {
                return (int)PlayerMouth;
            }        
            return 0;
        }
        
        //BodyColor
        public static void SetPlayerBodyColor(this Player player,int sBodyColor)
        {
            Hashtable score = new Hashtable();
            score[SatakOnline.Player_Props_BodyColor] = sBodyColor;
            player.SetCustomProperties(score);
        }

        public static int GetPlayerBodyColor(this Player player)
        {
            object PlayerBodyColor;         
            if (player.CustomProperties.TryGetValue(SatakOnline.Player_Props_BodyColor, out PlayerBodyColor))
            {
                return (int)PlayerBodyColor;
            }
            return 0;
        }

        //EyeColor
        public static void SetPlayerEyeColor(this Player player, int sEyeColor)
        {
            Hashtable score = new Hashtable();
            score[SatakOnline.Player_Props_EyeColor] = sEyeColor;

            player.SetCustomProperties(score);
        }

        public static int GetPlayerEyeColor(this Player player)
        {
            object PlayerEyeColor;
            if (player.CustomProperties.TryGetValue(SatakOnline.Player_Props_EyeColor, out PlayerEyeColor))
            {
                return (int)PlayerEyeColor;
            }
           
            return 0;
        }
        #endregion
    }
}
