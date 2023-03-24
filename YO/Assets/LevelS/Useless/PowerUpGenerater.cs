using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class PowerUps
{
    // for debugging
    public string Name;

    public GameObject Prefab;
    [Range (0f, 100f)]public float Chance = 100f;

    [HideInInspector] public double _weight;
}

public class PowerUpGenerater : MonoBehaviour
{
    public PowerUps[] PowerUps;

    public Transform[] spawnPoints;
    public double accumulateWeights;
    public System.Random rand = new System.Random();

    private void Awake()
    {
        CalculateWeights();
    }

    public void Start()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        PowerUps randomPower = PowerUps[GetRandomPowerIndex()];
        //PhotonNetwork.Instantiate(randomPower.Prefab.name, spawnPoints[rand].position, Quaternion.identity);
        Instantiate(randomPower.Prefab, spawnPoints[rand].position, Quaternion.identity);
        //int rand = Random.Range(0, objects.Length);     
        //PhotonNetwork.Instantiate(objects[rand].name, transform.position, Quaternion.identity);
        Debug.Log("Spawned" + randomPower.Name + "Chance = " + randomPower.Chance);

    }

    private int GetRandomPowerIndex()
    {
        double r = rand.NextDouble() * accumulateWeights;

        for (int i = 0; i < PowerUps.Length; i++)
            if (PowerUps[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeights()
    {
        accumulateWeights = 0f;
        foreach (PowerUps _power in PowerUps)
        {
            accumulateWeights += _power.Chance;
            _power._weight = accumulateWeights;
        }
    }
}
