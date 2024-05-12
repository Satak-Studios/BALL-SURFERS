using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;
    public bool BenchMark = false;
    public bool _intro = false;
    public GameObject completeScreen;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (BenchMark)
            {
                BackAnim();
            }
            else if (_intro)
            {
                completeScreen.SetActive(true);
            }
            else
            {
                gameManager.CompleteLevel();
            }
        }
    }

    public void BackAnim()
    {
        FindObjectOfType<BenchMark>().CompleteBench();
    }
    
    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelManager()
    {
        SceneManager.LoadScene("levelmanager");
    }
}
