using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Name : MonoBehaviour
{
    public Text _text;
    // Update is called once per frame
    void Update()
    {
        _text.text = PhotonNetwork.NickName;   
    }
}
