using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    public List<GameObject> liGoSpawn = new List<GameObject>();
    public Transform spawnpoint;

    void Start()
    {
        GameObject goToSpawn = liGoSpawn[Random.Range(0, liGoSpawn.Count)];
        Instantiate(goToSpawn, spawnpoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
