using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hindi : MonoBehaviour
{
    public bool isMenu;
   // public bool isLevel01;
    public bool isLevelS;
    public int level;

   // public GameObject gamo;
    public Text go;
    public Text retry;
    public Text mainmenu;




    public void LangHindi()
    {
         
        // if (scene.name == "Level01")
        //  {
        //     Debug.Log("Level01");
        // }
        //
        // if (scene.name == "Level02")
        // {
        //     Debug.Log("You Are In 2");
        // 
        //}

        //LEVEL 02
        if (isLevelS == true)
        {
            go.text = "खेल खत्म";
            retry.text = "फिर से प्रयास करें";
            mainmenu.text = "मेन्यू";
            Debug.Log("Game Over In Hindi");
        }
    }

    public void LangEnglish()
    {    
        if (isLevelS == true)
        {
            // gamo = GameObject.Find("Restart Canvas/AllButtons/game over");
            go.text = "GAME OVER";
            retry.text = "RESTART";
            mainmenu.text = "MAIN MENU";
            Debug.Log("Game Over In ENGLISH");
        }
    }

    public void LangTelugu()
    {
        if (isLevelS)
        {
            // gamo = GameObject.Find("Restart Canvas/AllButtons/game over");
            go.text = "ఆట సమాప్తం";
            retry.text = "మళ్ళీ ప్రారంభించండి";
            mainmenu.text = "ప్రధాన మెనూ";
            Debug.Log("Game Over In Telugu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        level = PlayerPrefs.GetInt("levelsUnlocked");
    }
}
