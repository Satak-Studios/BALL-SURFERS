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
            if (PlayerPrefs.GetInt("levelsUnlocked") != 100)
            {
                PlayerPrefs.SetInt("levelsUnlocked", cLevel + 1);
            }
        }
        if (currentLevel < 6)
        {
            if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            }
        }

        if (currentLevel > 6)
        {
            //nothing :)
        }
        SceneManager.LoadScene("levelmanager");
    }
}
