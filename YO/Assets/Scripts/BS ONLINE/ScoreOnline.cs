using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun.UtilityScripts;

public class ScoreOnline  : MonoBehaviourPunCallbacks
{
    #region old code
    /*private Transform playertp;
    public Text scoreText;
    public Text PING;
    public float number;
    //public Text HighScore;
    //public Text hscore;
    public int HighScoreFloat;
    public float Score;
    //public RestartOnline RSO;
    //public Player _player;
    private PhotonView PV;
    public bool isMine;

    private void Start()
    {
        //HighScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
        //HighScore.text = HighScoreFloat.ToString("0");
        PV = FindObjectOfType<PlayerOnline>().PV;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isMine == true)
        {
            playertp = FindObjectOfType<RestartOnline>().transform;
            //RSO = FindObjectOfType<RestartOnline>();
            number = playertp.position.z;
            //_player.SetRealScore(number);
            scoreText.text = number.ToString("0");
            //scoreText.text = PhotonNetwork.LocalPlayer.GetRealScore().ToString("0");
            PING.text = "PING : " + PhotonNetwork.GetPing();
        }
        //SaveHScore();

        //HighScore.text = PlayerPrefs.GetFloat("hiScore").ToString("00:00");

        if (Score > PlayerPrefs.GetFloat("hiScore", 0))
        {
            PlayerPrefs.SetFloat("hiScore", Score);
            //PlayerPrefs.SetFloat("high", Score);
            //hscore.text = PlayerPrefs.GetFloat("hiScore", number).ToString("0");
            //HighScoreFloat = Mathf.RoundToInt(PlayerPrefs.SetFloat("hiScore", number));
            //PlayerPrefs.Save();
            //IncreaseScore();
            //DecreaseScore();
            //Score = PlayerPrefs.GetFloat("hiScore", 0); 
            //IncreaseScore();
            Debug.Log("HighScore =  " + HighScoreFloat);
        }
        //HighScoreFloat = Mathf.RoundToInt(PlayerPrefs.GetFloat("hiScore", number));
        //Debug.Log(HighScoreFloat);
        // Debug.Log(Score);
        /*if (RSO.psoo.timesKatam == 1)
         {
             SaveHScore();
             //Debug.Log("Saving H SCORE");
         }

         if (RSO.psoo.timesKatam == 2)
         {
             SaveHScore();
             //Debug.Log("Saving H SCORE");
         }

         if (RSO.psoo.timesKatam == 3)
         {
             SaveHScore();
             //Debug.Log("Saving H SCORE");
         }
        SaveHScore();
    }

    public void ResetPlayer()
    {
        PlayerPrefs.DeleteKey("hiScore");
        PhotonNetwork.LocalPlayer.SetScore(0);
        Debug.Log("HighScore Deleted");
    }
    
    public void SaveHScore()
    {
        //PlayerPrefs.SetFloat("hiScore", playertp.position.z);
        Score = playertp.position.z;
        //Debug.Log("Score is " + Score);
        IncreaseScore();
    }
    /*
     public void CheckH()
     {
         if (playertp.position.z > /*PlayerPrefs.GetFloat("HighScore") HighScoreFloat)
         {
             //PlayerPrefs.SetFloat("HighScore", playertp.position.z);
             // HighScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
             // HighScore.text = HighScoreFloat.ToString("0");
             //Debug.Log("Setting Highscore");
             SaveHScore();
         }
     }

    public void IncreaseScore()
    {
        HighScoreFloat = (int)PlayerPrefs.GetFloat("hiScore");
        PhotonNetwork.LocalPlayer.SetScore(HighScoreFloat);
        //Debug.Log("Score saved in ScoreBoard");
        //Debug.Log("hScore = " + HighScoreFloat);
    }*/
    #endregion
    public GameObject cRoomPanel;
    public GameObject fRoomPanel;
    public GameObject mainPanel;
    public InputField roomIF;
    public InputField pName;
    public Text GM_txt;

    //Room Options
    public Dropdown GM;
    public int gameMode;
    public GameObject Comp;
    public GameObject Custom;
    public Dropdown Players;
    public byte players;
    public Dropdown Lives;
    public int life;

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

    public void OnClickCreate(Player kataS)
    {
        if (roomIF.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomIF.text, new RoomOptions() {
                MaxPlayers = players, 
                BroadcastPropsChangeToAll = true 
            });
            SatakExtensions.SetGM(kataS, gameMode);
            SatakExtensions.SetLives(kataS, life);
        }
    }

    private void Update()
    {
        pName.text = PhotonNetwork.NickName;
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 1)
        {
            GM_txt.text = "Default";
        }
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 2)
        {
            GM_txt.text = "Competition";
        }
        if (SatakExtensions.GetGM(PhotonNetwork.LocalPlayer) == 3)
        {
            GM_txt.text = "Custom";
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

        //Main
        if (gameMode == 1)
        {

        }
        if (gameMode == 2)
        {

        }
        if (gameMode == 2)
        {
 
        }

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
    }
}
namespace Photon.Pun.UtilityScripts
{
    /// <summary>
    /// Scoring system for PhotonPlayer
    /// </summary>
    public class SatakOnline : MonoBehaviour
    {
        public const string GM_Prop = "gMode";
        public const string Lives_Prop = "Lives";
        public const string PlayerLevelProp = "Level";
    }

    public static class SatakExtensions
    {
        //GameMode
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


        //Levels Completed
        public static void SetLives(this Player player, int newLife)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[PunPlayerScores.PlayerLevelProp] = newLife;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static int GetLives(this Player player)
        {
            object Lives;
            if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerLevelProp, out Lives))
            {
                return (int)Lives;
            }

            return 0;
        }

        #region Device
        public static void SetDevice(this Player player, string deviceName)
        {
            Hashtable device = new Hashtable();  // using PUN's implementation of Hashtable
            device[PunPlayerScores.PlayerDeviceProp] = deviceName;

            player.SetCustomProperties(device);  // this locally sets the score and will sync it in-game asap.
        }

        public static string GetDevice(this Player player)
        {
            object device;
            if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerDeviceProp, out device))
            {
                return device.ToString(); //(string)device;
            }

            return "Unknown";
        }
        #endregion
    }
}
