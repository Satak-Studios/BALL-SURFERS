using System;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class SaveData
{
    private string _userName = "PlayerName01010";
    private int _level = 1;
    private int _hScore = 0;
    private int[] ach;
    private int selectedEyes = 0;
    private int selectedMouth = 0;
    private int selectedBodyColor = 0;
    private int selectedEyeColor = 0;
    private int band = 0;

    [FirestoreProperty]
    public string UserName
    {
        get
        {
            _userName = PlayerPrefs.GetString("PlayerName");
            return _userName;
        }
        set
        {
            _userName = value;
            PlayerPrefs.SetString("PlayerName", value);
        }
    }

    [FirestoreProperty]
    public int Level
    {
        get
        {
            _level = PlayerPrefs.GetInt("levelsUnlocked", 1);
            return _level;
        }
        set
        {
            _level = value;
            PlayerPrefs.SetInt("levelsUnlocked", value);
        }
    }

    [FirestoreProperty]
    public int HighScore
    {
        get
        {
            _hScore = PlayerPrefs.GetInt("hiScore", 1);
            return _hScore;
        }
        set
        {
            _hScore = value;
            PlayerPrefs.SetInt("hiScore", value);
        }
    }

    [FirestoreProperty]
    public int[] Achievements
    {
        get
        {
            int[] achievements = new int[25];

            for (int i = 1; i <= 25; i++)
            {
                string achievementKey = "Achievement_" + i.ToString();
                achievements[i-1] = PlayerPrefs.GetInt(achievementKey);
            }

            return achievements;
        }
        set
        {
            for (int i = 1; i <= 25; i++)
            {
                string achievementKey = "Achievement_" + i.ToString();
                PlayerPrefs.SetInt(achievementKey, value[i-1]);
            }
        }
    }

    [FirestoreProperty]
    public int Eye
    {
        get
        {
            selectedEyes = PlayerPrefs.GetInt("eyes");
            return selectedEyes;
        }
        set
        {
            selectedEyes = value;
            PlayerPrefs.SetInt("eyes", value);
        }
    }

    [FirestoreProperty]
    public int EyeColor
    {
        get
        {
            selectedEyeColor = PlayerPrefs.GetInt("eyeColor");
            return selectedEyeColor;
        }
        set
        {
            selectedEyeColor = value;
            PlayerPrefs.SetInt("eyeColor", value);
        }
    }

    [FirestoreProperty]
    public int BodyColor
    {
        get
        {
            selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
            return selectedBodyColor;
        }
        set
        {
            selectedBodyColor = value;
            PlayerPrefs.SetInt("bodyColor", value);
        }
    }

    [FirestoreProperty]
    public int Mouth
    {
        get
        {
            selectedMouth = PlayerPrefs.GetInt("mouth");
            return selectedMouth;
        }
        set
        {
            selectedMouth = value;
            PlayerPrefs.SetInt("mouth", value);
        }
    }

    [FirestoreProperty]
    public int Bandage
    {
        get
        {
            band = PlayerPrefs.GetInt("band");
            return selectedEyes;
        }
        set
        {
            band = value;
            PlayerPrefs.SetInt("band", value);
        }
    }
}
