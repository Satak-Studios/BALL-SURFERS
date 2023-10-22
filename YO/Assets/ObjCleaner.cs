using System.Collections.Generic;
using UnityEngine;

public class ObjCleaner : MonoBehaviour
{
    List<GameObject> objectsInTrigger = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        objectsInTrigger.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);
    }

    public void DestroyObjectsInTrigger()
    {
        foreach (GameObject obj in objectsInTrigger)
        {
            Debug.Log("Destroying: " + obj.name);
            Destroy(obj);
        }

        objectsInTrigger.Clear(); // Clear the list after destroying the objects
    }
}
