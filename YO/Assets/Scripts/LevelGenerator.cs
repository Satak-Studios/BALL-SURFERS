using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] objects;
    public string currentLevel;

    public void Start()
    {
        int rand = Random.Range(0, objects.Length-1);
        if (PhotonNetwork.InRoom == false)
        {
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }

        if (PhotonNetwork.InRoom == true)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                if (objects[rand] != null)
                {
                    PhotonNetwork.Instantiate(objects[rand].name, transform.position, Quaternion.identity);
                }
                else
                {
                    int randAgain = Random.Range(0, objects.Length - 1);
                    PhotonNetwork.Instantiate(objects[randAgain].name, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentLevel == "infinite" && other.tag == "Player")
        {
            Restart rs = FindObjectOfType<Restart>();
            rs.RestartGame();
        }
    }
}
