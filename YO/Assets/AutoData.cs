using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


[System.Serializable]
public class AutoData : MonoBehaviour
{
    public string _name;
    public int Level;
    public float [] position;
    public Transform pm;
    public GameManager game;


    public AutoData ()
    {
        _name = PhotonNetwork.NickName;
        position = new float[3];
        position[0] = pm.transform.position.x;
        position[1] = pm.transform.position.y;
        position[2] = pm.transform.position.z;
    }
}
