using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Chat.UtilityScripts;

public class progress : MonoBehaviour
{
    //public levelmanager lm;
    //public GameObject ErrorLoad;
    public Text hScore;
    public Text cLevel;
    public Text PlayerName;

    public GameObject resetObj;

    public string[] Names;

    public Text Status;

    // Update is called once per frame
    void Update()
    {
        //GameObject obj = ErrorLoad;
        int hScoreInt = (int)PlayerPrefs.GetFloat("hiScore");
        hScore.text = "HighScore: " + hScoreInt.ToString();
        cLevel.text = "Levels: " + PlayerPrefs.GetInt("levelsUnlocked").ToString();
        PlayerName.text = PlayerPrefs.GetString("PlayerName").ToString();
        CalcXP();
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        //Setting Levels Unlocked to 1
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        SetPlayerName();
   }
   
    public void SetPlayerName()
     {     
        //Random PlayerName
        int rand = Random.Range(0, 10000);
        int randName = Random.Range(0, Names.Length);
        string player_name = Names[randName] + rand.ToString("0000");
        Debug.Log("Your Name is " + randName + rand);
        PlayerPrefs.SetString("PlayerName", player_name);
    }

    void CalcXP()
    {
        int levels = PlayerPrefs.GetInt("levelsUnlocked");
        int hScore = (int)PlayerPrefs.GetFloat("hiScore");
        int XP = levels*hScore/2;
        if (XP <= 1)
        {
            Status.text = "Status: " + "NewBie";
        }

        if (XP >= 500)
        {
            Status.text = "Status: " + "Causual";
        }

        if (XP >= 5000)
        {
            Status.text = "Status: " + "Intermediate";
        }

        if (XP >= 10000)
        {
            Status.text = "Status: " + "Advanced";
        }

        if (XP >= 15000)
        {
            Status.text = "Status: " + "Expert";
        }

        if (XP >= 25000)
        {
            Status.text = "Status: " + "Master";
        }

        if (XP >= 75000)
        {
            Status.text = "Status: " + "GrandMaster";
        }

        if (XP >+ 225000)
        {
            Status.text = "Status: " + "Legend";
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
}
