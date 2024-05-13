using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterCustomiser : MonoBehaviour
{
    public Material BodyColor;
    public GameObject BodyColorObj;
    public Material EyeColor;

    public GameObject[] eyes;
    public GameObject[] mouth;
    public GameObject[] hats;

    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedHat = 0;

    public GameObject[] Skins;
    public GameObject[] NotSkins;

    // Start is called before the first frame update
    void Start()
    {
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedMouth = PlayerPrefs.GetInt("mouth");       
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedHat = PlayerPrefs.GetInt("eyeColor");

        if (selectedMouth >= 1)
        {
            mouth[selectedMouth].SetActive(true);
            BodyColor.color = selectedBodyColor switch
            {
                0 => Color.red,
                1 => Color.black,
                2 => Color.green,
                3 => Color.blue,
                4 => Color.yellow,
                5 => Color.magenta,
                _ => BodyColor.color,
            };
        }
        else
        {
            eyes[selectedEyes].SetActive(true);
            mouth[selectedMouth].SetActive(true);
            BodyColor.color = selectedBodyColor switch
            {
                0 => Color.red,
                1 => Color.black,
                2 => Color.green,
                3 => Color.blue,
                4 => Color.yellow,
                5 => Color.magenta,
                _ => BodyColor.color,
            };
            hats[selectedHat].SetActive(true);
        }
    }

    public void Eyes()
    {
        selectedEyes += 1;
        if (selectedEyes >= eyes.Length)
        {
            eyes[eyes.Length - 1].SetActive(false);
            selectedEyes = 0;
        }
        eyes[selectedEyes].SetActive(true);
        int pre = selectedEyes-1;
        if (selectedEyes != 0)
        {
            eyes[pre].SetActive(false);
        }
        SaveCustomizations();
    }

    public void Mouth()
    {
        selectedMouth += 1;
        if (selectedMouth >= mouth.Length)
        {
            mouth[mouth.Length - 1].SetActive(false);
            selectedMouth = 0;
        }
        mouth[selectedMouth].SetActive(true);
        int pre = selectedMouth - 1;
        if (selectedMouth != 0)
        {
            mouth[pre].SetActive(false);
        }
        SaveCustomizations();
    }
    
    public void Hat()
    {
        selectedHat += 1;
        if (selectedHat >= hats.Length)
        {
            hats[hats.Length - 1].SetActive(false);
            selectedHat = 0;
        }
        hats[selectedHat].SetActive(true);
        int pre = selectedHat - 1;
        if (selectedHat != 0)
        {
            hats[pre].SetActive(false);
        }
        SaveCustomizations();
    }

    public void ChangeBodyColor(int colorIndex)
    {
        BodyColor.color = colorIndex switch
        {
            0 => Color.red,
            1 => Color.black,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => BodyColor.color,
        };
        selectedBodyColor = colorIndex;
        SaveCustomizations();
    }

    void SaveCustomizations()
    {
        PlayerPrefs.SetInt("eyes", selectedEyes);
        PlayerPrefs.SetInt("mouth", selectedMouth);
        PlayerPrefs.SetInt("eyeColor", selectedHat);
        PlayerPrefs.SetInt("bodyColor", selectedBodyColor);
        PlayerPrefs.Save();
    }

    public void ResetSkins()
    {
        selectedMouth = 0;
        mouth[selectedMouth].SetActive(false);
        BodyColorObj.SetActive(true);
        hats[selectedHat].SetActive(true);
        ChangeBodyColor(PlayerPrefs.GetInt("bodyColor"));
        eyes[selectedMouth].SetActive(true);
        PlayerPrefs.SetInt("mouth", selectedMouth);
    }

    public void Back()
    {
        SceneManager.LoadScene("Stats");
    }

    private void Update()
    {
        if (selectedMouth == 0)
        {
            BodyColorObj.SetActive(true);
            hats[selectedHat].SetActive(true);
            ChangeBodyColor(PlayerPrefs.GetInt("bodyColor"));
            eyes[selectedMouth].SetActive(true);
            for (int i = 0; i < NotSkins.Length; i++)
            {
                Skins[0].SetActive(false);
                NotSkins[i].SetActive(true);
            }
        }
        else if (selectedMouth > 0)
        {
            BodyColorObj.SetActive(false);
            eyes[selectedEyes].SetActive(false);
            hats[selectedHat].SetActive(false);
            for (int i = 0; i < NotSkins.Length; i++)
            {
                Skins[0].SetActive(true);
                NotSkins[i].SetActive(false);
            }
        }
        if (selectedMouth < 0)
        {
            selectedMouth = 0;
        }
    }
}
