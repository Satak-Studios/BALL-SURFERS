using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace PhotonTutu
{
    public class player_spawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;

        private void Start() => PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
}