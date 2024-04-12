using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CamManager : MonoBehaviour
{
    public GameObject[] cams;
    public int activeCam = 0;

    public void SwitchCamera()
    {
        activeCam += 1;
        if (activeCam >= cams.Length)
        {
            cams[cams.Length - 1].SetActive(false);
            activeCam = 0;
        }
        cams[activeCam].SetActive(true);
        if (activeCam != 0)
        {
            int pre = activeCam - 1;
            cams[pre].SetActive(false);
        }
        
    }
}
