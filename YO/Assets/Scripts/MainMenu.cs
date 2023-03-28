using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Text version;
    public GameObject Credit;

    public void satak()
    {
        SceneManager.LoadScene("levelmanager");
    }

    public void SHOP()
    {
        SceneManager.LoadScene("SHOP");
    }
    
    public void Start()
    {
        if (PlayerPrefs.HasKey("sk"))
        {
            version.text = "v" + Application.version + " (Sathvik Edition)";
        }
        else
        {
            version.text = "v" + Application.version;
        }
        PlayerPrefs.DeleteKey("cyber");
    }

    public void StartGame(string index)
    {
        SceneManager.LoadScene(index);
    }

    public void Credits()
    {
        Credit.SetActive(true);
    }
}
