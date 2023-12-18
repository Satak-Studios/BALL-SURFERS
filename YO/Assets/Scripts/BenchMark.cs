using System;
using UnityEngine;
using UnityEngine.UI;
using Satak.Utilities;

public class BenchMark : MonoBehaviour
{
    public Text players_txt;
    public int spawnedPlayer = 0;
    public GameObject playerPrefab; // Prefab to instantiate
    public GameObject[] spawnedPlayers;

    public void IncreasePlayer()
    {
        spawnedPlayer++;

        // Instantiate the playerPrefab
        GameObject newPlayer = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);

        // Ensure there is enough space in the array
        if (spawnedPlayers == null || spawnedPlayers.Length <= spawnedPlayer - 1)
        {
            // Resize the array with additional space
            Array.Resize(ref spawnedPlayers, spawnedPlayer);
        }

        // Add the new player to the array
        spawnedPlayers[spawnedPlayer - 1] = newPlayer;
    }

    public void DestroyAllPlayers()
    {
        for (int i = 0; i < spawnedPlayer; i++)
        {
            Destroy(spawnedPlayers[i]);
        }

        // Reset the array
        spawnedPlayers = new GameObject[0];
        spawnedPlayer = 0;
    }

    void Update()
    {
        players_txt.text = "Player: " + (spawnedPlayer + 4).ToString();

        if (FindObjectOfType<FPSCounter>().fps <= 2)
        {
            DestroyAllPlayers();
        }

        if (spawnedPlayer <= 0)
        {
            spawnedPlayer = 0;
        }
    }
}
