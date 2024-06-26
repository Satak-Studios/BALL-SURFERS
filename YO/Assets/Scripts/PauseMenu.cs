﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public bool test = false;
    public GameObject pauseMenuUI = null;
    private GameObject option = null;

    public GameObject Score = null;

    public static bool isOptions = false;

    void Start()
    {
        GameIsPaused = false;
        if(Score == null)
        {
            Score = FindObjectOfType<Score>().gameObject;
        }
        if (option == null)
        {
            option = FindObjectOfType<settingmenu>()._panel;
        }
        option = FindObjectOfType<settingmenu>()._panel;
    }

    // Update is called once per frame
    void Update()
    {
        //option = FindObjectOfType<settingmenu>().gameObject;
        if (GameIsPaused == true)
            {
            blad();
            }
            else
            {
            Red();
            }
        test = GameIsPaused; 
    }

    public void blad()
    {
        if (isOptions == false)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Score.SetActive(false);
        }

        if (isOptions == true)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Score.SetActive(false);
        }
        FindObjectOfType<Restart>()._disapper = true;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu 1");
        GameIsPaused = false;
    }

    public void Red()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Score.SetActive(true);
        pauseMenuUI.SetActive(false);
        isOptions = false;
        if (FindObjectOfType<Restart>() != null)
        {
            FindObjectOfType<Restart>()._disapper = false;
        }
        else
        {
            return;
        }
    }
    void UnLoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu 1");
    }

    public void LoadingMenu()
    {
        SceneManager.LoadScene("Menu 1");
        PlayerPrefs.SetInt("sdrn", 0);
    }

   public void rest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("sdrn", 0);
    }
    public void options()

    {
        option.SetActive(true);
        //pauseMenuUI.SetActive(false);
        isOptions = true;
        GameIsPaused = true;
        FindObjectOfType<Restart>()._disapper = true;
    }
    public void back()
    {
        option.SetActive(false);
        //pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        isOptions = false;
        FindObjectOfType<Restart>()._disapper = false;

    }

    public void Pause()
    {
        GameIsPaused = true;
    }

    public void Resume()
    {
        GameIsPaused = false;
    }

    public void Quite()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Red();
        PlayerPrefs.SetInt("sdrn", 0);
    }
}


