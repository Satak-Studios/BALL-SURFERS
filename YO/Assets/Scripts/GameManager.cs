using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject completeLevelUI;
	public Restart rs;
	public int level;
	public LevelScript ls;

	public void Start()
    {
		//level = PlayerPrefs.GetInt("levelsUnlocked");
	}

	public void CompleteLevel()
	{
		completeLevelUI.SetActive(true);
		PlayerPrefs.SetInt("sdrn", 0);
		rs.cl = true;
	}

    public void Pass()
    {
		PlayerPrefs.SetInt("sdrn", 0);
		ls.Pass();
		SceneManager.LoadScene("levelmanager");
	}
    public void EndGame()
    {
		rs.EndGames();
    }
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
