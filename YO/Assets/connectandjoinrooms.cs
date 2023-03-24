using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

namespace PhotonBye
{

    public class connectandjoinrooms : MonoBehaviourPun
    {
        [SerializeField] private Text nameText;
        private void Start()
        {
            if (photonView.IsMine) { return; }



            SetName();
        }

        private void SetName() => nameText.text = photonView.Owner.NickName;

    }
}