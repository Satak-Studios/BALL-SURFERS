using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class progress : MonoBehaviour
{
    public levelmanager lm;
    //public GameObject ErrorLoad;
    public Text hScore;
    public Text cLevel;
    public Text PlayerName;

    public GameObject resetObj;

    public string[] Names;

    // Update is called once per frame
    void Update()
    {
        //GameObject obj = ErrorLoad;
        hScore.text = PlayerPrefs.GetFloat("hiScore").ToString();
        cLevel.text = PlayerPrefs.GetInt("levelsUnlocked").ToString();
        PlayerName.text = PlayerPrefs.GetString("PlayerName").ToString();
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        //Setting Levels Unlocked to 1
        lm.levelsUnlocked = 1;//PlayerPrefs.SetInt("levelsUnlocked", 1);
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
}
