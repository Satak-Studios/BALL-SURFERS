using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuOnline : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject option;

    public GameObject Score;

    // Update is called once per frame
    void Update()
    {
        if (!GameIsPaused)
        {
            Red();
        }
        else
        {
            blad();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 1f;
            GameIsPaused = true;
            Score.SetActive(false);
            Debug.Log("Game Paused");
        }


        void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu 1");
        }

        void QuitGame()
        {
            Application.Quit();
        }
        void Options()
        {
            SceneManager.LoadScene("options");
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
public  void blad()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
        Score.SetActive(false);
    }
    public void LoadMenu()
    {
        {
            SceneManager.LoadScene("Menu 1");
            GameIsPaused = false;
        }
    }

    public void Red()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Score.SetActive(true);
    }
    void UnLoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu 1");
    }

    void Quit()
    {
        Application.Quit();
    }

    public void LoadingMenu()
    {
        {
            SceneManager.LoadScene("Menu 1");
        }
    }
   public void rest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void options()
    {
        {
            option.SetActive(true);
            pauseMenuUI.SetActive(false);
            GameIsPaused = true;
        }
    }
    public void back()
    {
        option.SetActive(false);
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

public void Quite()
    {
        Application.Quit();
    }

    public void ScoreBoard()
    {
        GameIsPaused = true;
    }
}


