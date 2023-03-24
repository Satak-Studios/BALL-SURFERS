using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Utilities;


[System.Serializable]
public class ResumeData// : MonoBehaviour
{
    //string _name;
    //public string Name;
    public string PlayerName;
    public int Level;
    //int fLevel;
    public float HighScore;

    public ResumeData (levelmanager lm)
    {
        Level = lm.levelsUnlocked;
        PlayerName = PlayerPrefs.GetString("PlayerName");
        HighScore = PlayerPrefs.GetFloat("hiScore");
    }
}
