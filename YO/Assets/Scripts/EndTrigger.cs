using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;
    public bool BenchMark = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter ()
	{
        if (BenchMark)
        {
            Back();
        }
        else
        {
            gameManager.CompleteLevel();
        }
	}

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
