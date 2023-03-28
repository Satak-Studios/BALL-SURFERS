using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oktutorial : MonoBehaviour
{
    public GameObject ok;
    public GameObject ko;


    
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
        print("hi");
    }
    public void button()
    {
        Time.timeScale = 1f;
        ok.SetActive(false);
        ko.SetActive(false);
        GameObject.Destroy(ok);
        GameObject.Destroy(ko);

    }
}
