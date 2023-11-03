using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] objects;
    //public Transform PLayer;

    public void Start()
    {
        int rand = Random.Range(0, objects.Length);
        if (PhotonNetwork.InRoom== false)
        {
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }

        if (PhotonNetwork.InRoom == true)
        {
            if(PhotonNetwork.IsMasterClient == true)
            PhotonNetwork.Instantiate(objects[rand].name, transform.position, Quaternion.identity);
        }
    }
}
