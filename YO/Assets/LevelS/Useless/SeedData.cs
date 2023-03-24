using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeedData// : MonoBehaviour
{
    public float[] position;

    public SeedData(RandObjSaver cols)
    {
        position = new float[3];

        position[0] = cols.transform.position.x;
        position[1] = cols.transform.position.y;
        position[2] = cols.transform.position.z;
    }
}
