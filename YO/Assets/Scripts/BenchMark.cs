using System;
using UnityEngine;
using UnityEngine.UI;
using Satak.Utilities;

public class BenchMark : MonoBehaviour
{
    public Text players_txt;
    public int spawnedPlayer = 0;
    public GameObject mainPlayer;
    public GameObject[] playerPrefab;
    public GameObject[] spawnedPlayers;
    public GameObject[] buttons;

    public float accumFPS = 0f;
    public int totalFrames = 0;
    private float minFPS = float.MaxValue;
    private float maxFPS = 0f;

    public Text MinFPS;
    public Text MaxFPS;
    public Text AvgFPS;

    public void IncreasePlayer()
    {
        spawnedPlayer++;
        int rand = UnityEngine.Random.Range(0, playerPrefab.Length);
        GameObject newPlayer = Instantiate(playerPrefab[rand], mainPlayer.transform.position, mainPlayer.transform.rotation);
        if (spawnedPlayers == null || spawnedPlayers.Length <= spawnedPlayer - 1)
        {
            Array.Resize(ref spawnedPlayers, spawnedPlayer);
        }
        spawnedPlayers[spawnedPlayer - 1] = newPlayer;
    }

    public void DestroyAllPlayers()
    {
        for (int i = 0; i < spawnedPlayer; i++)
        {
            Destroy(spawnedPlayers[i]);
        }

        spawnedPlayers = new GameObject[0];
        spawnedPlayer = 0;
    }

    void Update()
    {
        players_txt.text = "Player: " + (spawnedPlayer + 4).ToString();

        float currentFPS = FindObjectOfType<FPSCounter>().fps;
        if (currentFPS > 0)
        {
            accumFPS += currentFPS;
            totalFrames++;
            maxFPS = Mathf.Max(maxFPS, currentFPS);
            if (accumFPS >= totalFrames + currentFPS)
            {
                minFPS = Mathf.Min(minFPS, (2 * (accumFPS / totalFrames) - maxFPS));
            }
        }
        MinFPS.text = "Min : " + minFPS.ToString("0") + " FPS";
        MaxFPS.text = "Max : " + maxFPS.ToString("0") + " FPS";
        AvgFPS.text = "Avg : " + (accumFPS / totalFrames).ToString("0") + " FPS";

        if (FindObjectOfType<FPSCounter>().fps <= 2)
        {
            DestroyAllPlayers();
        }

        if (spawnedPlayer <= 0)
        {
            spawnedPlayer = 0;
        }
    }

    public void CompleteBench()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            players_txt.gameObject.SetActive(false);
        }
    }
}
