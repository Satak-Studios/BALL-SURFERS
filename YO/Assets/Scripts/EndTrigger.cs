using UnityEngine;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter ()
	{
		gameManager.CompleteLevel();
	}

}
