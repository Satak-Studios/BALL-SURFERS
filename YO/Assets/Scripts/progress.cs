using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Chat.UtilityScripts;
using Utilities.General;
using Utilities;

public class progress : MonoBehaviour
{
    //public levelmanager lm;
    //public GameObject ErrorLoad;
    public Text hScore;
    public Text cLevel;
    public Text PlayerName;
    public InputField nameField;
    public Text xpText;
    public Text achText;

    public string[] Names;

    public Text Status;

    public bool rotate180 = true;

    public GameObject ImpMark;
    public GameObject ImpMarkFake;
    public SaveSystem saveSystem;

    void Start()
    {
        if (PlayerPrefs.GetString("PlayerName") == "")
        {
            SetPlayerName();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int hScoreInt = (int)PlayerPrefs.GetFloat("hiScore", 1);
        if (hScore != null)
        {
            hScore.text = "HighScore: " + hScoreInt.ToString();
        }
        if (cLevel != null)
        {
            cLevel.text = "Levels: " + PlayerPrefs.GetInt("levelsUnlocked").ToString();
        }
        if (PlayerName != null)
        {
            PlayerName.text = PlayerPrefs.GetString("PlayerName").ToString();
        }
        if (achText != null)
        {
            achText.text = "Achievements:" + PlayerPrefs.GetInt("totalAch").ToString();
        }
        if (Status != null)
        {
            CalcXP();
        }

        if (ImpMark != null)
        {
            if (PlayerPrefs.GetInt("ImpMark") >= 1)
            {
                ImpMark.SetActive(true);
                ImpMarkFake.SetActive(true);
            }
            else
            {
                ImpMark.SetActive(false);
                ImpMarkFake.SetActive(false);
            }
        }
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        PlayerPrefs.SetFloat("hiScore", 1);
        FindObjectOfType<Achiever>().DeleteAllAchievements();
        FindObjectOfType<Achiever>().AchievementUnlocked(1);
        ResetCharacterCustomizaions();
        FindObjectOfType<PlaytimeCalculator>().totalPlaytime = 0f;
        SetPlayerName();
    }

    public void ResetCharacterCustomizaions()
    {
        PlayerPrefs.SetInt("eyes", 0);
        PlayerPrefs.SetInt("mouth", 0);
        PlayerPrefs.SetInt("eyeColor", 0);
        PlayerPrefs.SetInt("bodyColor", 0);
        PlayerPrefs.Save();
    }

    //Random PlayerName
    public void SetPlayerName()
    {
        //string[] nouns = { "Gamer", "Explorer", "Adventurer", "Hero", "Champion", "Pioneer", "Detective", "Scholar", "Artist", "Musician", "Scientist", "Engineer", "Captain", "Pirate", "Wizard", "Warrior", "Athlete", "Leader", "Dreamer", "Traveler", "Nomad", "Guardian", "Hunter", "Knight", "Jester", "Acrobat", "Magician", "Guardian", "Gladiator", "Spy", "Sailor", "Astronaut", "Pirate", "Viking", "Explorer", "Samurai", "Ninja", "Archer", "Scribe", "Sage", "Gladiator" };
        string[] nouns = FindObjectOfType<Achiever>().playerNameSuffix;
        string[] names = FindObjectOfType<Achiever>().playerNames;

        int randNoun = Random.Range(0, nouns.Length);
        int rand = Random.Range(0, 10000);
        //int randName = Random.Range(0, names.Length);
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

    void CalcXP()
    {
        int achievements = PlayerPrefs.GetInt("totalAch");
        int levels = PlayerPrefs.GetInt("levelsUnlocked", 1);
        int hScore = (int)PlayerPrefs.GetFloat("hiScore", 1);
        int XP = achievements*levels*hScore/2;
        if (XP <= 1)
        {
            Status.text = "Status: " + "NewBie";
        }

        if (XP >= 500)
        {
            Status.text = "Status: " + "Causual";
            if (FindObjectOfType<Achiever>().achIndex[9] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(9);
			}
        }

        if (XP >= 1100)
        {
            Status.text = "Status: " + "Intermediate";
            if (FindObjectOfType<Achiever>().achIndex[10] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(10);
			}
        }

        if (XP >= 10000)
        {
            Status.text = "Status: " + "Advanced";
            if (FindObjectOfType<Achiever>().achIndex[11] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(11);
			}
        }

        if (XP >= 15000)
        {
            Status.text = "Status: " + "Expert";
            if (FindObjectOfType<Achiever>().achIndex[12] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(12);
			}
        }

        if (XP >= 25000)
        {
            Status.text = "Status: " + "Master";
            if (FindObjectOfType<Achiever>().achIndex[13] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(13);
			}
        }

        if (XP >= 75000)
        {
            Status.text = "Status: " + "GrandMaster";
            if (FindObjectOfType<Achiever>().achIndex[14] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(14);
			}
        }

        if (XP >= 225000)
        {
            Status.text = "Status: " + "Legend";
            if (FindObjectOfType<Achiever>().achIndex[15] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(15);
			}
        }
        if (xpText != null)
        {
            xpText.text = "Xp: " + XP;
            PlayerPrefs.SetInt("xp", XP);
        }
    }  

    public void CH()
    {
        SceneManager.LoadScene("CH");
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void RemoveImp()
    {
        PlayerPrefs.SetInt("ImpMark", 0);
    }

    public void LoadGameFile()
    {
        SaveData data = saveSystem.LoadGame();
        nameField.text = data._userName;
        PlayerPrefs.SetString("PlayerName", data._userName);
        PlayerPrefs.SetInt("levelsUnlocked", data._level);
        PlayerPrefs.SetFloat("hiScore", data._hScore);
        FindObjectOfType<Achiever>().DeleteAllAchievements();
        for (int i = 1; i <= 25; i++)
        {
            FindObjectOfType<Achiever>().LoadAllAchievements(i, data.ach[i-1]);
        }
        PlayerPrefs.SetInt("eyes", data.selectedEyes);
        PlayerPrefs.SetInt("eyeColor", data.selectedEyeColor);
        PlayerPrefs.SetInt("bodyColor", data.selectedBodyColor);
        PlayerPrefs.SetInt("mouth", data.selectedMouth);
        PlayerPrefs.SetInt("band", data.band);
        PlayerPrefs.SetFloat("TotalPlaytime", data.timePlayed);
        FindObjectOfType<PlaytimeCalculator>().totalPlaytime = data.timePlayed;
    }
}
