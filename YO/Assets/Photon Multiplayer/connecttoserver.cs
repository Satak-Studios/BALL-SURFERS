using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class connecttoserver : MonoBehaviourPunCallbacks
{
    public InputField username;
    public Text buttonText;
    public string SceneToLoadOnLobby;
    public bool isCreater = false;

    private const string PlayerPrefsNameKey = "PlayerName";
    public string[] Names;

    #region Connect-To-Region

    //DropdownBox For Regions
    public Dropdown value;

    //Regions
    public string region;
    public string Region_1;
    public string Region_2;
    public string Region_3;
    public string Region_4;
    public string Region_5;
    #endregion

    public void OnClick()
    {
        if (username.text.Length >= 1)
        {
            PlayerPrefs.SetString(PlayerPrefsNameKey, username.text);
            PhotonNetwork.NickName = username.text;
            buttonText.text = "Connecting....";
            Network();
            //PhotonNetwork.AutomaticallySyncScene = true;
            //PhotonNetwork.ConnectToRegion(region);
            PhotonNetwork.ConnectUsingSettings();
            //RegionChanger();
        }
    }

    public GameObject NameError;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNameKey)){
        username.text = PlayerPrefs.GetString(PlayerPrefsNameKey);
        username.characterLimit = 15;
        }

        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            SetPlayerName();
        }
    }

    public void Connect()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        SceneManager.LoadScene("Start");
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(SceneToLoadOnLobby);
        Debug.Log("Connected To Master");
    }
 
     public GameObject noInternet;

    // Start is called before the first frame update
    void Network()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Network Available");
            noInternet.SetActive(false);
        }
        else
        {
            Debug.Log("No Network Available");
            noInternet.SetActive(true);
        }
    }

    public void Retry()
    {
        OnClick();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu 1");
    }

    private void Update()
    {
        /*Verify That it is me
        if (isCreater == true)
        {
            PlayerPrefs.SetString("sk", "Sathvik");
            //Debug.LogWarning("You Are Creater of this Game, Mr.Sathvik");
        }
        if (isCreater == false)
        {
            PlayerPrefs.DeleteKey("sk");
            //Debug.LogWarning("Hey You Are Just A Player");
        }
        bool nRestriction;
        if (PlayerPrefs.HasKey("sk"))
        {
            nRestriction = false;
        }
        else
        {
            nRestriction = true;
        }

        if (nRestriction == true)
        {
            /* if (username.text == "Sathvik")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "Satak")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "sathvik")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "satak")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "SathviK")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "sathviK")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "sataK")
             {
                 NameError.SetActive(true);
             }

             if (username.text == "SataK")
             {
                 NameError.SetActive(true);
             }*/
            /*
            string pName = PlayerPrefs.GetString("PlayerName");
            pName.ToLower();
            pName.Trim();
            if (pName == "sathvik")
            {
                NameError.SetActive(true);
            }

            if (pName == "satak")
            {
                NameError.SetActive(true);
            }*/

            /*
            string usernameString = username.text;
            usernameString.ToLower();
            usernameString.Trim();
            Debug.LogWarning(usernameString); 
            if (usernameString == "sathvik")
            {
               NameError.SetActive(true);
            }

            if (usernameString == "satak")
            {
                NameError.SetActive(true);
            }
        }
        else
        {
            NameError.SetActive(false);
        }
        }*/
    }

    public void nTry()
    {
        username.text = "";
    }

    public void RegionChanger()
    {
        if (value.value == 0)
        {
            PhotonNetwork.ConnectToRegion(Region_1);
            //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = Region_1;
            //PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        if (value.value == 1)
        {
            PhotonNetwork.ConnectToRegion(Region_2);
            //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = Region_2;
            //PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        if (value.value == 2)
        {
           PhotonNetwork.ConnectToRegion(Region_3);
            //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = Region_3;
            //PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        if (value.value == 3)
        {
            PhotonNetwork.ConnectToRegion(Region_4);
            //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = Region_4;
            //PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        if (value.value == 4)
        {
            PhotonNetwork.ConnectToRegion(Region_5);
            //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = Region_5;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    public void SetPlayerName()
    {
        string[] nouns = { "Gamer", "Explorer", "Adventurer", "Hero", "Champion", "Pioneer", "Detective", "Scholar", "Artist", "Musician", "Scientist", "Engineer", "Captain", "Pirate", "Wizard", "Warrior", "Athlete", "Leader", "Dreamer", "Traveler", "Nomad", "Guardian", "Hunter", "Knight", "Jester", "Acrobat", "Magician", "Guardian", "Gladiator", "Spy", "Sailor", "Astronaut", "Pirate", "Viking", "Explorer", "Samurai", "Ninja", "Archer", "Scribe", "Sage", "Gladiator" };
        int randNoun = Random.Range(0, nouns.Length);
        int rand = Random.Range(0, 10000);
        int randName = Random.Range(0, Names.Length);
        string player_name = Names[randName] + nouns[randNoun] + rand.ToString("0000");
        
        if (player_name.Length > 15)
        {
            string playerName = player_name.Substring(0, 15);
            username.text = playerName;
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            username.text = player_name;
            PlayerPrefs.SetString("PlayerName", player_name);
        }
    }
}


