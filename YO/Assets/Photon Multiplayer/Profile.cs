using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Profile : MonoBehaviour
{
    public GameObject[] objects;
    public Transform PLayer;
    bool isOnline;

    public void Start()
    {
        int rand = Random.Range(0, objects.Length);
        if (PhotonNetwork.InRoom== false)
        {
            //Instantiate(objects[rand], transform.position, Quaternion.identity);
            Debug.Log("Offline mode enabled");
            isOnline = false;
        }

        if (PhotonNetwork.InRoom == true)
        {
            //if(PhotonNetwork.IsMasterClient == true)
            //PhotonNetwork.Instantiate(objects[rand].name, transform.position, Quaternion.identity);
            Debug.Log("Online mode enabled");
            isOnline = true;
        }
    }

    public void OnTriggerEntered()
    {
         
    }
}
