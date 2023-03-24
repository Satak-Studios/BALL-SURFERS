
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
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            PhotonNetwork.ConnectUsingSettings();
            status.text = "Connecting....";
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            SceneManager.LoadScene("connecttoserver");
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on current position of the touch each frame
            pointer.position = touch.position;
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(Lobby);
        Debug.Log("Connected To Master");
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
