using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Satak.Utilities;

public class BenchMark : MonoBehaviour
{
   //public GameObject StartScreen;
    public GameObject[] oldBench;
    public GameObject[] newBench;
    public GameObject completeBench;
    public bool _regular = true;
    public GameObject _obsticle;
    public Text players_txt;
    public int spawnedPlayer = 0;
    public GameObject mainPlayer;
    public GameObject sidePlayer;
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

    private void Start()
    {
        //StartScreen.SetActive(true);
        //Time.timeScale = 0f;
        Regular();
        completeBench.SetActive(false);
        minFPS = float.MaxValue;
    }

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
            if (accumFPS >= totalFrames + currentFPS && minFPS > 0)
            {
                if (minFPS > 0)
                {
                    //minFPS = Mathf.Min(minFPS, (2 * (accumFPS / totalFrames) - maxFPS));
                    if (currentFPS < minFPS)
                    {

                        minFPS = Mathf.Min(currentFPS);
                    }
                }
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

        if (!_regular)
        {
            Instantiate(_obsticle,
                new Vector3(sidePlayer.transform.position.x, 10f, sidePlayer.transform.position.z + 15),
                Quaternion.identity);
        }
    }

    public void CompleteBench()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            players_txt.gameObject.SetActive(false);
        }
        completeBench.SetActive(true);
    }

    public void StressTest()
    {
        for (int i = 0; i < oldBench.Length; i++)
        {
            oldBench[i].SetActive(false);
        }

        for (int i = 0; i < newBench.Length; i++)
        {
            newBench[i].SetActive(true);
        }
        _regular = false;
        sidePlayer.SetActive(true);
        sidePlayer.GetComponent<BSSAI>().ShouldIMove = true;
    }

    public void Regular()
    {
        for (int i = 0; i < oldBench.Length; i++)
        {
            oldBench[i].SetActive(true);
        }
        
        for (int i = 0; i < newBench.Length; i++)
        {
            newBench[i].SetActive(false);
        }
        _regular = true;
        sidePlayer.SetActive(false);
        sidePlayer.GetComponent<BSSAI>().ShouldIMove = false;
    }
}
