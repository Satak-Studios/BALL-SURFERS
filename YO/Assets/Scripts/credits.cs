using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelManager()
    {
        SceneManager.LoadScene("levelmanager");
    }

    public void Appear()
    {
        FindObjectOfType<Restart>()._disapper = false;
        FindObjectOfType<playermovement>().gameStarted = true;
    }
}