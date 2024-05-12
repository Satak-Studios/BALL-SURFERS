using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int currentIndex;
    public string fixedKey = "TutorialOf";
    public GameObject _holder;

    void Update()
    {
        if (currentIndex == 2 && PlayerPrefs.HasKey(fixedKey + "1") && !PlayerPrefs.HasKey(fixedKey + currentIndex))
        {
            _holder.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.HasKey(fixedKey + currentIndex.ToString()))
            {
                _holder.SetActive(false);
            }
            else
            {
                _holder.SetActive(true);
            }
        }
    }

    public void Done()
    {
        _holder.SetActive(false);
        PlayerPrefs.SetString(fixedKey + currentIndex.ToString(), "Done!!!!");
    }
}
