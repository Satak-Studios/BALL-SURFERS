//Attach this script to a GameObject
//This script checks what device type the Application is running on and outputs this to the console

using UnityEngine;

public class Device : MonoBehaviour
{
    public bool isnotLevel1;

    public bool isLevel1;

    public bool isHavingKey;
    public string m_DeviceType;
    public string m_DeviceOSType;
    public GameObject dialogpc;
    public GameObject dialogmobile;
    public GameObject controls;

    public GameObject Player;

    void Update()
    {
        if (isLevel1 == false)
        {
            dialogmobile.SetActive(false);
            dialogpc.SetActive(false);
            if (!(Player == null))
            {
                Player.SetActive(true);
            }
        }

        if (SystemInfo.deviceType == DeviceType.Console)
        {
            m_DeviceType = "Console";
        }

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (isLevel1 == true)
            {
                if (PlayerPrefs.HasKey("tutuu") == false)
                {
                    dialogpc.SetActive(true);
                    dialogmobile.SetActive(false);
                    m_DeviceType = "Desktop";
                    controls.SetActive(false);
                    Player.SetActive(false);
                    isHavingKey = false;
                }
                else
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    controls.SetActive(false);
                    Player.SetActive(true);
                    isHavingKey = true;
                }
            }

                if (isLevel1 == false)
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    controls.SetActive(false);
                    Player.SetActive(true);
                }
            
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (isLevel1 == true)
            {
                if (PlayerPrefs.HasKey("tutuu") == false)
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(true);
                    m_DeviceType = "HandHeld";
                    controls.SetActive(false);
                    Player.SetActive(false);
                    isHavingKey = false;
                }

                if (PlayerPrefs.HasKey("tutuu") == true)
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    controls.SetActive(true);
                    Player.SetActive(true);
                    isHavingKey = true;
                }
            }
            if (isLevel1 == false)
            {
                dialogpc.SetActive(false);
                dialogmobile.SetActive(false);
                controls.SetActive(true);
                Player.SetActive(true);
            }
        }

        if (SystemInfo.deviceType == DeviceType.Unknown)
        {
            m_DeviceType = "Unknown";
        }

        m_DeviceOSType = SystemInfo.operatingSystem;

    }
    public void OnClickOk()
    {
        PlayerPrefs.SetString("tutuu", "tutu");
    }

}

