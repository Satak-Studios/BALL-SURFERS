using UnityEngine;
using Photon.Pun;

public class Device : MonoBehaviour
{
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
    }

    void Update()
    {
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

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (pauseLevel)
            {
                if (!isHavingKey)
                {
                    dialogpc.SetActive(true);
                    dialogmobile.SetActive(false);
                    m_DeviceType = "Desktop";
                    controls.SetActive(false);
                    Player.SetActive(false);
                }
                else
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    controls.SetActive(false);
                    Player = FindObjectOfType<playermovement>().gameObject;
                    if (!(Player == null))
                    {
                        Player.SetActive(true);
                    }
                    m_DeviceType = "Desktop";
                    FindObjectOfType<Restart>()._disapper = false;
                }
            }
            else
            {
                dialogpc.SetActive(false);
                dialogmobile.SetActive(false);
                controls.SetActive(false);
            }
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (pauseLevel)
            {
                if (!isHavingKey)
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(true);
                    m_DeviceType = "HandHeld";
                    controls.SetActive(false);
                    Player.SetActive(false);
                }
                else
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    controls.SetActive(true);
                    m_DeviceType = "HandHeld";
                    Player.SetActive(true);
                }
            }
            else
            {
                dialogpc.SetActive(false);
                dialogmobile.SetActive(false);
                controls.SetActive(true);
                m_DeviceType = "HandHeld";
                Player = FindObjectOfType<playermovement>().gameObject;
                if (!(Player == null))
                {
                    Player.SetActive(true);
                }
            }
        }
    }
    public void OnClickOk()
    {
        PlayerPrefs.SetString(unlockKey, unlockKey);
        isHavingKey = true;
    }

}

