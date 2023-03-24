using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

	public void LoadNextLesvel  ()
	{
		SceneManager.LoadScene("levelmanager");
	}

}
