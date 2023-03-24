using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public bool test = false;
    public GameObject pauseMenuUI = null;
    //public GameObject optionPrefab;
    public GameObject option = null;

    public GameObject Score = null;
    //public bool isPrefab = false;

    public static bool isOptions = false;

    bool _instantiated = false;

    void Start()
    {
        //Red();
        GameIsPaused = false;
        Score = FindObjectOfType<Score>().gameObject;
        option = FindObjectOfType<settingmenu>().gameObject;
        /*if(option == null){
           option = optionPrefab;
           _instantiated = true;
        }*/
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
        //if (/*isPrefab == false &&*/ Input.GetKey(KeyCode.Escape))
        /*{
            if (GameIsPaused)
            {
                Red();
                Debug.Log("Game Resumed");
            }
            else
            {
                _instantiated = true;
                blad();
                Debug.Log("Game Paused");
            }
            //Debug.Log("Game Paused");
            //blad();
        }

        if (GameIsPaused)
        {
            Red();
        }
        else
        {
            blad();
        }
        /*  if(GameIsPaused == true && Input.GetKey(KeyCode.Escape))
          {
              Red();
              print("You Are Resuming");
          }*/


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
            Debug.LogError("Back Yo");
        }
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
        Time.timeScale = 1f;
        GameIsPaused = false;
        Score.SetActive(true);
        pauseMenuUI.SetActive(false);
        isOptions = false;
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
            PlayerPrefs.SetInt("sdrn", 0);
        }
    }
   public void rest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("sdrn", 0);
    }
    public void options()
    {
        {
            option.SetActive(true);
            //pauseMenuUI.SetActive(false);
            isOptions = true;
            GameIsPaused = true;
        }
    }
    public void back()
    {
        option.SetActive(false);
        //pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        isOptions = false;
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


