using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;
    public bool BenchMark = false;
    public GameObject compuBench;

    private void Start()
    {
        if (BenchMark)
        {
            compuBench.SetActive(false);
        }
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
            else
            {
                gameManager.CompleteLevel();
            }
        }
	}

    public void BackAnim()
    {
        compuBench.SetActive(true);
        FindObjectOfType<BenchMark>().CompleteBench();
    }
    
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
