using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timemanager : MonoBehaviour
{
    public GameObject Lobbytutu;
    public GameObject Roomtutu;

    public void Update(){
        //Lobby Tutorial
        if (PlayerPrefs.HasKey("lotutu") == false)
        {
            Lobbytutu.SetActive(true);
        }

        if (PlayerPrefs.HasKey("lotutu") == true){
            Lobbytutu.SetActive(false);
        }

        //Room Tutorial
                if (PlayerPrefs.HasKey("rotutu") == false)
        {
            Roomtutu.SetActive(true);
        }

        if (PlayerPrefs.HasKey("rotutu") == true){
            Roomtutu.SetActive(false);
        }
    }

    public void OnClickLobbyOk(){
        PlayerPrefs.SetString("lotutu", "bla");
    }

    public void OnClickRoomok(){
        PlayerPrefs.SetString("rotutu", "bla");
    }
}
