using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    /*public GameObject hi;    

    public Text name;*/
    public Text version;

    public void Start()
    {
        string _version = Application.version;
        if (PlayerPrefs.HasKey("sk"))
        {
            version.text = "." + _version.Substring(1) + " (Sathvik Edition)";
        }
        else
        {
            version.text = "." + _version.Substring(1);
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

    public void Stats()
    {
        SceneManager.LoadScene("Stats");
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    /*public void Update()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
           name.text = PlayerPrefs.GetString("PlayerName");
        else
           hi.SetActive(true);
    }*/
}
