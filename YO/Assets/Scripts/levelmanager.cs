using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour
{
    public GameObject Credits;
    public int levelsUnlocked;
    public Button[] buttons;
    public GameObject Nxetten;

    int slPanel = 0;
    public GameObject[] LevelPanels;

    public GameObject SaveWarn;
    public GameObject LoadWarn;
    public GameObject LoadError;

    // Start is called before the first frame update
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void SavePlayerCheck()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        if (File.Exists(path) == false)
        {
            SavePlayer();
        }
        else
        {
            SaveWarn.SetActive(true);
        }
    }

    public void SavePlayer()
    {
        ResumeSystem.SaveFile(this);
    }

    public void LoadPlayerCheck()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        if (File.Exists(path) == true)
        {
            LoadWarn.SetActive(true);
        }
        else
        {
            LoadError.SetActive(true);
        }
    }
    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        if (File.Exists(path) == true)
        {
            ResumeData data = ResumeSystem.LoadFile();

            levelsUnlocked = data.Level;
            //float HighScore = data.HighScore;
            PlayerPrefs.SetFloat("hiScore", data.HighScore);
            PlayerPrefs.SetString("PlayerName", data.PlayerName);
        }
        else
        {
            LoadWarn.SetActive(true);
        }
    }

    private void Update()
    {
        PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
        Start();
        if (levelsUnlocked == 10)
        {
            if (PlayerPrefs.HasKey("credits") == false)
            {
                Credits.SetActive(true);
                PlayerPrefs.SetString("credits", "credits");
                Nxetten.SetActive(true);
                Debug.Log("Showing Credits");
            }

            if (PlayerPrefs.HasKey("credits") == true)
            {
                //Debug.Log("Doing Nothing");
                Nxetten.SetActive(true);
            }
        }
        else
        {
            Nxetten.SetActive(false);
        }
    }

    public void NextPanel()
    {
        
        //int allLevelPanel = LevelPanels.Length;
        LevelPanels[slPanel].SetActive(false);
        slPanel += 1;
        //slPanel = (slPanel + 1) & LevelPanels.Length;
        LevelPanels[slPanel].SetActive(true);
        Debug.Log("Your Panel is " + slPanel);
    }

    public void PreviousPanel()
    {
        //int allLevelPanel = LevelPanels.Length;
        LevelPanels[slPanel].SetActive(false);
        slPanel --;
        if (slPanel < 0)
        {
            slPanel += LevelPanels.Length;
        }
        LevelPanels[slPanel].SetActive(true);
    }
}
