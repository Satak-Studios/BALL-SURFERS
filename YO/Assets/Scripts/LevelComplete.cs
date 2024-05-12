using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {
	public GameObject _a;
	public void LoadNextLesvel  ()
	{
		SceneManager.LoadScene("levelmanager");
	}

	public void AHHHHHH()
    {
		_a.SetActive(true);
    }

}
