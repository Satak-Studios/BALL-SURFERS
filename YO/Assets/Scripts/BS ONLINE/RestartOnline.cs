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
                rs.controls.SetActive(false);

                Time.timeScale = 1f;

                if (gameHasEnded == true)
                {
                    psoo.Hearts -= 1;
                    ResMenu.SetActive(true);
                    pso.katam();
                }
            }
        }
    }
}
