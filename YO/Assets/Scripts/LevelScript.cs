using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public void PassLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 6)
        {
            int cLevel = PlayerPrefs.GetInt("levelsUnlocked");
            PlayerPrefs.SetInt("levelsUnlocked", cLevel + 1);
        }else
        {
            if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            }
        }
        Debug.Log("LEVEL" + PlayerPrefs.GetInt("levelsUnlocked") + "UNLOCKED");
        SceneManager.LoadScene("levelmanager");
    }

}
