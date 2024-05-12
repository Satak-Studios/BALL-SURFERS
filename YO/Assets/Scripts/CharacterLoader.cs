using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLoader : MonoBehaviour
{
    public Material BodyColor;
    public GameObject BodyColorObj;
    public Material EyeColor;

    public GameObject[] eyes;
    public GameObject[] mouth;

    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedEyeColor = 0;

    public bool lockRotation = true;

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
            0 => Color.red,
            1 => Color.black,
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

    void Update()
    {
        if (lockRotation)
        {
            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.x = 0f;
            newRotation.y = 180f;
            newRotation.z = 0f;
            transform.rotation = Quaternion.Euler(newRotation);
        }

        if (!(selectedEyes + selectedMouth == 0) && SceneManager.GetActiveScene().buildIndex > 0)
        {
            if (PlayerPrefs.GetInt("Achievement_2") == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(2);
            }
        }

        if (selectedMouth <= 0)
        {
            BodyColorObj.SetActive(true);
            eyes[selectedMouth].SetActive(true);
        }
        else
        {
            BodyColorObj.SetActive(false);
            eyes[selectedEyes].SetActive(false);
        }
    }
}
