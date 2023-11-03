
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Multiplayer : MonoBehaviourPunCallbacks
{
    public Text status;
    public string Lobby;
    public Transform pointer;

    public void onStart()
    {
            PhotonNetwork.ConnectUsingSettings();
            status.text = "Connecting....";
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.AutomaticallySyncScene = true;

            Touch touch = Input.GetTouch(0);
            pointer.position = touch.position;
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(Lobby);
    }

    public void CreateMenu()
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

    public void NoName()
    {
        SceneManager.LoadScene("connecttoserver");
    }

}
