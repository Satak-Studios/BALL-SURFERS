using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public CompManager compManager;
    public int index = -1;

    void OnTriggerEntered(){
        index += 1;

        switch (index)
        {
            case 0: compManager.currentPos = 1;break;
            case 1: compManager.currentPos = 2;break;
            case 2: compManager.currentPos = 3;break;
            case 3: compManager.currentPos = 4;break;
            default: compManager.currentPos = 0;break;
        }
    }
}
