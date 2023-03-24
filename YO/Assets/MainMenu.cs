using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Text version;
    public GameObject LeaderButtom;
    public int value = 0;

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

    private void Update()
    {
        if (PhotonNetwork.InRoom == true)
        {
            LeaderButtom.SetActive(false);
            //Debug.Log("Room == true");
            PlayerPrefs.SetString("cyber", "cyber");
        }
    }
    public void Check()
    {
        //LeaderButtom.SetActive(true);
       /* if (value == 1)
        {
            LeaderButtom.SetActive(false);
            //Debug.Log("Room == true");
        }
        if (value == 0)
        {
            LeaderButtom.SetActive(true);
        }*/
       if (PlayerPrefs.HasKey("cyber"))
       {
            LeaderButtom.SetActive(false);
        }
        else
        {
            LeaderButtom.SetActive(true);
        }
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
