using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;
    public int randPlayer;

    public Timer SCO;

    // Start is called before the first frame update
    void Start()
    {
        randPlayer = Random.Range(0, playerPrefabs.Length);

        int rand = Random.Range(0, spawnPoints.Length);
        //int randPlayer = Random.Range(0, playerPrefabs.Length);
        Transform Point = spawnPoints[rand];
        GameObject playertoSpawn = playerPrefabs[randPlayer];
        PhotonNetwork.Instantiate(playertoSpawn.name, Point.position, Quaternion.identity);
        Debug.Log("Spawned Player = " + playertoSpawn.name);
        SCO.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
