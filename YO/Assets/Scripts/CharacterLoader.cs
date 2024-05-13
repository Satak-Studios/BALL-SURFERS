using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLoader : MonoBehaviour
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
    public bool lockRotation = true;
    public bool _okay = false;

    public void Start()
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

        if (SceneManager.GetActiveScene().name == "CH")
        {
            if (PlayerPrefs.GetInt("Achievement_2") == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(2);
            }
        }

        if (selectedMouth < 1)
        {
            BodyColorObj.SetActive(true);
            eyes[selectedMouth].SetActive(true);
        }
        else
        {
            BodyColorObj.SetActive(false);
            eyes[selectedEyes].SetActive(false);
            hats[selectedHat].SetActive(false);
        }

        if (selectedMouth > 2 && _okay)
        {
            lockRotation = false;
        }
        else if (_okay && selectedMouth < 3)
        {
            lockRotation = true;
        }
        else
        {
            lockRotation = false;
        }
    }
}
