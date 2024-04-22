using UnityEngine;
using Photon.Pun;

public class Device : MonoBehaviour
{
    public bool isInvsible = false;
    public bool pauseLevel = false;
    public string unlockKey;

    public bool isHavingKey;
    public string m_DeviceType;
    
    public GameObject dialogpc;
    public GameObject dialogmobile;
    public GameObject controls;

    public GameObject Player;

    private void Start()
    {
        if (PlayerPrefs.HasKey(unlockKey))
        {
            isHavingKey = true;
        }
        else
        {
            isHavingKey = false;
        }

        if (!pauseLevel)
        {
            dialogmobile.SetActive(false);
            dialogpc.SetActive(false);
            if (!PhotonNetwork.InRoom)
            {
                Player = FindObjectOfType<playermovement>().gameObject;
                if (!(Player == null))
                {
                    Player.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (pauseLevel)
            {
                if (!isHavingKey)
                {
                    dialogpc.SetActive(true);
                    Player.SetActive(false);
                }
                else
                {
                    dialogpc.SetActive(false);
                    Player.SetActive(true);
                }
            }
            else
            {
                dialogpc.SetActive(false);
                Player.SetActive(true);
            }
            m_DeviceType = "Desktop";
            dialogmobile.SetActive(false);
            controls.SetActive(false);
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (pauseLevel)
            {
                if (!isHavingKey)
                {
                    dialogmobile.SetActive(true);
                    Player.SetActive(false);
                }
                else
                {
                    dialogmobile.SetActive(false);
                    Player.SetActive(true);
                }
            }
            else
            {
                dialogmobile.SetActive(false);
                Player.SetActive(true);
            }

            if (!isInvsible)
            {
                controls.SetActive(true);
            }
            else
            {
                controls.SetActive(false);
            }
            dialogpc.SetActive(false);
            m_DeviceType = "HandHeld";
        }
    }
    public void OnClickOk()
    {
        PlayerPrefs.SetString(unlockKey, unlockKey);
        isHavingKey = true;
    }

}

