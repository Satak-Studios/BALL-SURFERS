using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    [SerializeField] GameObject[] gb;
    public Button left;
    public Button right;
    public int selectedCharacter = 0;
    int nextcharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int randomNumber;

    public void NextCharacter()
    {
        gb[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % gb.Length;
        gb[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        gb[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += gb.Length;
        }
        gb[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("levelmanager");
    }
}
