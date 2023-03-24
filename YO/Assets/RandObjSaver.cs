using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandObjSaver : MonoBehaviour
{
    public GameObject[] eyes;
    private int currentEye;

    public GameObject[] mouths;
    private int currentMouth;

    public GameObject[] bodies;
    private int currentBody;
    public int tmp_body = 0;

    public Color[] bColor;
    public Material bMat;

    public Color[] cColor;
    public Material cMat;
    private void Update()
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            if (i == currentEye)
            {
                eyes[i].SetActive(true);
            }
            else
            {
                eyes[i].SetActive(false);
            }
        }


        for (int i = 0; i < mouths.Length; i++)
        {
            if (i == currentEye)
            {
                mouths[i].SetActive(true);
            }
            else
            {
                mouths[i].SetActive(false);
            }
        }


        for (int i = 0; i < mouths.Length; i++)
        {
            if (i == currentEye)
            {
                mouths[i].SetActive(true);
            }
            else
            {
                mouths[i].SetActive(false);
            }
        }
    }

    public void SwitchEyes()
    {
        if (currentEye == eyes.Length - 1)
        {
            currentEye = 0;
        }
        else
        {
            currentEye++;
        }
    }
    public void SwitchMouths()
    {
        if (currentMouth == mouths.Length - 1)
        {
            currentMouth = 0;
        }
        else
        {
            currentMouth++;
        }
    }

    public void SwitchBodies(int colorIndex)
    {
        /*if (currentBody == 0)
        {
            currentBody = 1;
            bodies[tmp_body].SetActive(false);
            bodies[colorIndex].SetActive(true);
            tmp_body = colorIndex;
        }
        if (currentBody == 1)
        {
            currentBody = 0;
            bodies[colorIndex].SetActive(true);
            bodies[tmp_body].SetActive(false);
        }*/
        bMat.color = bColor[colorIndex];
    }

    public void SwitchEyeColor(int colorIndex)
    {
        cMat.color = cColor[colorIndex];
    }
    // Start is called before the first frame update
    //void Start()
    /* void Awake()
     {
         if (PlayerPrefs.HasKey("test"))
         {
             SeedData data = SeedSaveSystem.LoadFile(this);

             Vector3 Vposition;
             Vposition.x = data.position[0];
             Vposition.y = data.position[1];
             Vposition.z = data.position[2];

             transform.position = Vposition;
         }
         else
         {
             SeedSaveSystem.SaveFile(this);
             PlayerPrefs.SetString("test", "");
         }
     }
     */
}
