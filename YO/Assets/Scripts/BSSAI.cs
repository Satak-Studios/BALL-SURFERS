using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BSSAI : MonoBehaviour
{
    public bool ShouldIMove;
    public GameObject Point;

    public bool isNotRegular = false;

    // Update is called once per frame
    void Update()
    {
        if (ShouldIMove == true)
        {
            GetComponent<NavMeshAgent>().SetDestination(Point.transform.position);
        }

        if (gameObject.transform.position.y <= -1)
        {
            Destroy(gameObject);
            FindObjectOfType<BenchMark>().spawnedPlayer--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isNotRegular && other.CompareTag("StressBench"))
        {
            Destroy(other.gameObject, 0f);
        }
    }
}
