using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Intro : MonoBehaviour
{ 
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
