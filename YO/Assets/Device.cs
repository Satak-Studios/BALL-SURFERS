//Attach this script to a GameObject
//This script checks what device type the Application is running on and outputs this to the console

using UnityEngine;

public class Device : MonoBehaviour
{
    public bool isnotLevel1;

    public bool isLevel1;

    public bool isHavingKey;
    //This is the Text for the Label at the top of the screen
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
            Player.SetActive(true);
        }

        //Output the device type to the console window
        //Debug.Log("Device type : " + m_DeviceType);

        //Check if the device running this is a console
        if (SystemInfo.deviceType == DeviceType.Console)
        {
            //Change the text of the label
            m_DeviceType = "Console";
        }

        //Check if the device running this is a desktop
        if (SystemInfo.deviceType == DeviceType.Desktop)// && PlayerPrefs.HasKey("key") == false)
        {
            if (isLevel1 == true)
            {
                if (PlayerPrefs.HasKey("tutuu") == false)
                {
                    /* dialogpc.SetActive(true);
                     dialogmobile.SetActive(false);
                     m_DeviceType = "Desktop";
                     controls.SetActive(false);
                    */
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
                    //m_DeviceType = "Desktop";
                    controls.SetActive(false);
                    Player.SetActive(true);
                    isHavingKey = true;
                }
            }

                if (isLevel1 == false)
                {
                    dialogpc.SetActive(false);
                    dialogmobile.SetActive(false);
                    //m_DeviceType = "Desktop";
                    controls.SetActive(false);
                    Player.SetActive(true);
                    // Debug.Log("Destop");
                }
            
        }
        //Check if the device running this is a handheld
        if (SystemInfo.deviceType == DeviceType.Handheld)// && PlayerPrefs.HasKey("key") == false)
        {
            /*dialogpc.SetActive(false);
            dialogmobile.SetActive(true);
            m_DeviceType = "Handheld";
            controls.SetActive(true);*/
            //Debug.Log("Mobile");
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
                    //m_DeviceType = "HandHeld";
                    controls.SetActive(true);
                    Player.SetActive(true);
                    isHavingKey = true;
                }
            }
            if (isLevel1 == false)
            {
                dialogpc.SetActive(false);
                dialogmobile.SetActive(false);
                //m_DeviceType = "Desktop";
                controls.SetActive(true);
                Player.SetActive(true);
                // Debug.Log("Destop");
            }
        }

        //Check if the device running this is unknown
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

