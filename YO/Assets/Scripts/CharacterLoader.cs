using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public Material BodyColor;
    public Material EyeColor;

    public GameObject[] eyes;
    public GameObject[] mouth;

    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedEyeColor = 0;

    // Start is called before the first frame update
    void Start()
    {
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedMouth = PlayerPrefs.GetInt("mouth");       
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedEyeColor = PlayerPrefs.GetInt("eyeColor");

        eyes[selectedEyes].SetActive(true);
        mouth[selectedMouth].SetActive(true);
        BodyColor.color = selectedBodyColor switch
        {
            0 => Color.black,
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => BodyColor.color,
        };

        EyeColor.color = selectedEyeColor switch
        {
            0 => Color.black,
            1 => Color.white,
            _ => EyeColor.color,
        };
    }
}