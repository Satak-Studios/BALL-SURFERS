using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Photon.Menus
{

    public class CLOSE : MonoBehaviour
    {
        [SerializeField] private InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;
        public GameObject Menu;

        private const string PlayerPrefsNameKey = "PlayerName";


        private void Start() 
        {
            if (PlayerPrefs.HasKey(PlayerPrefsNameKey))
            {
                gameObject.SetActive(false);
                Menu.SetActive(true);
            }
            else
            {
                Menu.SetActive(false);
                gameObject.SetActive(true);
                SetInputField();
            }
        }

        private void SetInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }
            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
            nameInputField.text = defaultName;


            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }
        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }
    }
}