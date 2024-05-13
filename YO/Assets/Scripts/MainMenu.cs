using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Text version;
    public GameObject Imp;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("v2.0"))
        {
            Play2Footage();
        }
        else
        {
            string achievementKey = "Achievement_1";
            if (PlayerPrefs.GetInt(achievementKey) == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(1);
            }
        }
    }

    public void Start()
    {
        string _version = Application.version;
        if (PlayerPrefs.HasKey("sk"))
        {
            version.text =  _version.Substring(1) + " (Developer Edition)"; //Version = 2.X.XX
        }
        else
        {
            version.text = _version.Substring(1);//Version = 2.X.XX
        }
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("levelmanager");
    }

    public void Multiplayer()
    {
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("intro"))
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            SceneManager.LoadScene("Trailer");
        }
    }

    public void Stats()
    {
        SceneManager.LoadScene("Stats");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play2Footage()
    {
        //Debug.Log("Play Footage!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("Welcome");
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ImpMark") >= 1)
        {
            Imp.SetActive(true);
        }else
        {
            Imp.SetActive(false);
        }
    }
}
