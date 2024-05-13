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

        int currentPanel = (levelsUnlocked - 1) / 3;
        currentPanel = Mathf.Clamp(currentPanel, 0, LevelPanels.Length - 1);

        slPanel = currentPanel;
        for (int i = 0; i < LevelPanels.Length; i++)
        {
            LevelPanels[i].SetActive(i == currentPanel);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);   
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu 1");
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

        if (levelsUnlocked >= 10)
        {
            if (PlayerPrefs.HasKey("credits") == false)
            {
                Credits.SetActive(true);
            }else
            {
                Credits.SetActive(false);
                switch (slPanel)
                {
                    case 2: Next_btn.SetActive(false); break;
                    case 33: Next_btn.SetActive(false); break;
                    default: Next_btn.SetActive(true); break;
                }
            }
        }
        else
        {
            switch (slPanel)
            {
                case 2 : Next_btn.SetActive(false);break;
                case 33: Next_btn.SetActive(false);break; 
                default: Next_btn.SetActive(true);break;
            }
        }

        if (slPanel <= 0)
        {
            Back_btn.SetActive(false);
            slPanel = 0;
        }else
        {
            Back_btn.SetActive(true);
        }
    }

   public void NextPanel()
    {
        if (slPanel < LevelPanels.Length)
        {
            LevelPanels[slPanel].SetActive(false);
            slPanel++;
            LevelPanels[slPanel].SetActive(true);
        }else if (slPanel == 33)
        {
            Next_btn.SetActive(false);
        }
    }

    public void GoToPanel(int index)
    {
        slPanel = index;
        LevelPanels[slPanel].SetActive(true);
    }

    public void PreviousPanel()
    {
        if (slPanel > 0)
        {
            LevelPanels[slPanel].SetActive(false);
            slPanel--;
            LevelPanels[slPanel].SetActive(true);
        }
    }
}
