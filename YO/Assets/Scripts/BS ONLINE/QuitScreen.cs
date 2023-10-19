using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class QuitScreen : MonoBehaviour
{
    public bool WherAmI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteCompReal()
    {
        if (!WherAmI)
        {
            FindObjectOfType<CompManager>().CompleteComp();
        }
    }
}
