using UnityEngine;

public class Device : MonoBehaviour
{
    public bool isInvisible = false;
    public string m_DeviceType;
    public GameObject controls;

    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            m_DeviceType = "Desktop";
            controls.SetActive(false);
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (!isInvisible)
            {
                controls.SetActive(true);
            }
            else
            {
                controls.SetActive(false);
            }
            m_DeviceType = "HandHeld";
        }
    }
}

