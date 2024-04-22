using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject completeLevelUI;
	public Restart rs;
	public int level;
	public LevelScript ls;

	public void CompleteLevel()
	{
		completeLevelUI.SetActive(true);
		rs._disapper = true;
		PlayerPrefs.SetInt("sdrn", 0);
		rs.cl = true;
	}

    public void Pass()
    {
		PlayerPrefs.SetInt("sdrn", 0);
		ls.PassLevel();
	}
    public void EndGame()
    {
		rs.EndGames(FindObjectOfType<playermovement>().transform.position.z);
    }
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Home()
    {
		SceneManager.LoadScene(0);
    }

	void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 7)
		{
			if (FindObjectOfType<Achiever>().achIndex[7] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(7);
			}
		}

		if (SceneManager.GetActiveScene().buildIndex == 8)
		{
			if (FindObjectOfType<Achiever>().achIndex[8] == 0)
			{
				FindObjectOfType<Achiever>().AchievementUnlocked(8);
			}
		}
	}
}
