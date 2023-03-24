using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private PlayerOnline plo;
 
    // Start is called before the first frame update
    void Start()
    {
        //plo = FindObjectOfType<PlayerOnline>();
    }

    // Update is called once per frame
    void Update()
    {
        plo = FindObjectOfType<PlayerOnline>();
    }

    //Left
    public void lefT()
    {
        plo.Left();
    }

    //Right
    public void righT()
    {
        plo.Right();
    }

    //Left Pointer Up
    public void pointerLeftup()
    {
        plo.pointerUpleft();
    }

    //Left Pointer Down
    public void pointerLeftdown()
    {
        plo.pointerDownleft();
    }

    //Right pointer Up
    public void pointerRightup()
    {
        plo.pointerUpright();
    }

    //Right Pointer Down
    public void pointerRightdown()
    {
        plo.pointerDownright();
    }
}
