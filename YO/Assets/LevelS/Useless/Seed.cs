using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seed : MonoBehaviour
{
    public string GameSeed = "Default";
    public int currentSeed = 0;


    private void Awake()
    {
        currentSeed = GetHashCode();
        Random.InitState(currentSeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
