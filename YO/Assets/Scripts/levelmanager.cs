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
    public GameObject Next_btn;
    public GameObject Back_btn;

    public int slPanel = 0;
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

        int currentPanel = levelsUnlocked/3;
        if(currentPanel / 3 == Mathf.RoundToInt(currentPanel / 3))
        {
            slPanel = currentPanel-2;
            NextPanel();
        }else
        {
            slPanel = currentPanel-1;
            NextPanel();
        }
        LevelPanels[0].SetActive(false);
        //Debug.Log("Current Level Panel = " + currentPanel);
        //LevelPanels[currentPanel].SetActive(true);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);   
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu 1");
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
        
        //Start Begin
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
        //end

        if (levelsUnlocked == 10)
        {
            if (PlayerPrefs.HasKey("credits") == false)
            {
                Credits.SetActive(true);
                PlayerPrefs.SetString("credits", "credits");
                Next_btn.SetActive(true);
                Debug.Log("Showing Credits");
            }

            if (PlayerPrefs.HasKey("credits") == true)
            {
                //Debug.Log("Doing Nothing");
                Next_btn.SetActive(true);
            }
        }
        else if(slPanel == 2)
        {
            Next_btn.SetActive(false);
        }else
        {
            Next_btn.SetActive(true);
        }

        if (slPanel == 0)
        {
            Back_btn.SetActive(false);
        }else
        {
            Back_btn.SetActive(true);
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
