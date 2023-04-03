using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public GameObject hi;    

    public Text name;
    public Text version;

    public void Start()
    {
        if (PlayerPrefs.HasKey("sk"))
        {
            version.text = "." + Application.version + " (Sathvik Edition)";
        }
        else
        {
            version.text = "." + Application.version;
        }
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("levelmanager");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Start");
    }

    public void Profile()
    {
        SceneManager.LoadScene("CH");
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Update()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
           name.text = PlayerPrefs.GetString("PlayerName");
        else
           hi.SetActive(true);
    }
}
