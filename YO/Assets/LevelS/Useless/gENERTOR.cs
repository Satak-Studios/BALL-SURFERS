using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gENERTOR : MonoBehaviour
{
    public GameObject[] Obsticles;
    public GameObject[] spawn;

    public int Objectcount = 4;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < Objectcount; i++)
        {
            GameObject selected = Obsticles[Random.Range(0, Obsticles.Length)];

            GameObject ground = Instantiate(selected, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
