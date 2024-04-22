using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string _userName = "PlayerName01010";
    public int _level = 1;
    public int _hScore = 0;
    public int[] ach;
    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedEyeColor = 0;
    public int band = 0;
    public float timePlayed = 0;

    public SaveData()
    {
        _userName = PlayerPrefs.GetString("PlayerName");
        _level = PlayerPrefs.GetInt("levelsUnlocked", 1);
        _hScore = (int)PlayerPrefs.GetFloat("hiScore");
        int[] achievements = new int[25];
        for (int i = 1; i <= 25; i++)
        {
            string achievementKey = "Achievement_" + i.ToString();
            achievements[i-1] = PlayerPrefs.GetInt(achievementKey);
        }
        ach = achievements;
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedEyeColor = PlayerPrefs.GetInt("eyeColor");
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedMouth = PlayerPrefs.GetInt("mouth");
        band = PlayerPrefs.GetInt("band");
        timePlayed = PlayerPrefs.GetFloat("TotalPlaytime", 0f);
    }
}