using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SpawnObjectRandom();
    }
    void SpawnObjectRandom()
    {
        Vector3 randomPos = Random.insideUnitCircle * Radius;
        Instantiate(ItemPrefab, randomPos, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
