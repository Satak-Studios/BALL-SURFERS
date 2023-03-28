using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using Utilities;
using Photon.Pun.UtilityScripts;
using System.IO;

public class Intro : MonoBehaviour
{
    /*
     public float videoTime;
     public int MenuSceneIndex;

     // Start is called before the first frame update
     void Start()
     {
        if(isIntro == true)
         Invoke("waitforintro", videoTime);
     }

     void waitforintro()
     {
        if (isIntro == true)
        {
            SceneManager.LoadScene(MenuSceneIndex);
            Debug.Log("Loading Menu");
        }
     }*/
    

    public GameObject EndCanvas;
    public bool isIntro;

    private void OnTriggerEnter(Collider other)
    {
        EndCanvas.SetActive(true);
        AddBadge(PhotonNetwork.LocalPlayer);
    }

    public void AddBadge(Player player)
    {
        //player.SetBadge("Game Completed");
        int Badgee = int.Parse("");
        //ScoreExtensions.SetScore(player, Badgee);
    }

    public void OnClickOkEnding()
    {
        SceneManager.LoadScene("Menu 1");
    }
}
