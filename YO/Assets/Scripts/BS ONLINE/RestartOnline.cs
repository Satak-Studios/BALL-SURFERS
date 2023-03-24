using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

public class RestartOnline : MonoBehaviour
{
    public PhotonView Char;
    private Restart rs;
    public PlayerOnline pso;
    public PlayerSpawner psoo;
    private GameObject ResMenu;
    private GameManager gm;
    private player _SCO;

    public bool gameHasEnded = false;
    //public Player _player;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ResMenu = FindObjectOfType<Restart>().Restartmenu;
        rs = FindObjectOfType<Restart>();
        psoo = FindObjectOfType<PlayerSpawner>();
        _SCO = FindObjectOfType<player>();
    }
    //Restart Script
    //public void EndGaming(Player _player)
    public void EndGaming()
    {
        if (Char.IsMine)
        {
            if (gameHasEnded == false)
            {
                gameHasEnded = true;
                Debug.Log("GAME OVER");
                rs.controls.SetActive(true);

                Time.timeScale = 1f;
                //new WaitForSeconds(5f);
                //rs.Restartmenu.SetActive(true);
                //new WaitForSeconds(1);
                //rs.sco.SaveHScore();

                if (gameHasEnded == true)
                {
                    //_player.AddHearts(-1);
                    //PhotonNetwork.LocalPlayer.AddHearts(-1);
                    psoo.Hearts -= 1;
                    Debug.Log("katam");
                    //rs.Restartmenu.SetActive(true);
                    ResMenu.SetActive(true);
                    //pso.sco.SaveHScore();
                    //Debug.Log("Setting Highscore");
                    //PhotonNetwork.Destroy(Char);
                    pso.katam();
                    Debug.Log("Current Hearts = " + psoo.Hearts);
                    //Debug.Log("Current HighScore is " + rs.sco.HighScoreFloat);
                    //rs.sco.ResetPlayer();
                    _SCO.stopwatchActive = false;
                    _SCO.currentTime = 0;
                    //gm.Check();
                }
            }
        }
    }
}
