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

                if (gameHasEnded == true)
                {
                    psoo.Hearts -= 1;
                    Debug.Log("katam");
                    ResMenu.SetActive(true);
                    pso.katam();
                    Debug.Log("Current Hearts = " + psoo.Hearts);
                }
            }
        }
    }
}
