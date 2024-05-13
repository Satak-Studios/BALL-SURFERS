using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int currentIndex;
    public string fixedKey = "TutorialOf";
    public GameObject _holder;

    void Update()
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

    public void Done()
    {
        _holder.SetActive(false);
        PlayerPrefs.SetString(fixedKey + currentIndex.ToString(), "Done!!!!");
    }
}
