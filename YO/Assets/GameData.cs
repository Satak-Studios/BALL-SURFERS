using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public int score;
    public string _name;
    public float Level;
    

    public GameData(int scoreInt, string nameStr, float timePlayedF)
    {
        score = scoreInt;
        _name = nameStr;
        Level = timePlayedF;
    }
}
