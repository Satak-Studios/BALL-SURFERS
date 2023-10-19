using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterCustomiser : MonoBehaviour
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
        eyes[pre].SetActive(false);
        PlayerPrefs.SetInt("eyes", selectedEyes);
    }

    public void Mouth()
    {
        selectedMouth++;
        if (selectedMouth >= mouth.Length)
        {
            selectedMouth = 0;
        }
        mouth[selectedMouth].SetActive(true);
        int pre = selectedMouth -= 1;
        mouth[pre].SetActive(false);
        PlayerPrefs.SetInt("mouth", selectedMouth);
    }

    public void ChangeBodyColor(int colorIndex)
    {
        BodyColor.color = colorIndex switch
        {
            0 => Color.black,
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => BodyColor.color,
        };
        selectedBodyColor = colorIndex;
        PlayerPrefs.SetInt("bodyColor", colorIndex);
    }

    public void ChangeEyeColor(int colorIndex)
    {
        EyeColor.color = colorIndex switch
        {
            0 => Color.black,
            1 => Color.white,
            _ => BodyColor.color,
        };
        selectedEyeColor = colorIndex;
        PlayerPrefs.SetInt("eyeColor", colorIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene("Stats");
    }
}
