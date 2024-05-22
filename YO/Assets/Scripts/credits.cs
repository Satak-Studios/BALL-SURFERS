using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public void LoadLevelManagerBecauseCreditsAreOver()
    {
        SceneManager.LoadScene("levelmanager");
        PlayerPrefs.SetString("credits", "credits");
    }

    public void Done()
    {
        gameObject.SetActive(false);
    }
}
