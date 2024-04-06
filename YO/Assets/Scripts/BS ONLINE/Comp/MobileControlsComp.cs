using UnityEngine;

public class MobileControlsComp : MonoBehaviour
{
    private OnlinePlayer plo;

    // Update is called once per frame
    void Update()
    {
        if (plo != null)
        {
            plo = FindObjectOfType<OnlinePlayer>();
        }
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
