using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class Restart : MonoBehaviour
{
	[SerializeField] public GameObject Restartmenu;
	public float restartDelay = 1f;
	public bool gameHasEnded = false;
	public Rigidbody rb;
	public GameObject controls;
	public bool rscreen;

	public ScoreOnline sco;

	public bool cl = false;

	public void restartGame()
	{
		const string Message = "RESTART";
		Debug.Log(Message);
		Invoke("Restart", restartDelay);
	}


    private void Start()
    {
        
	}


    public void RestartGame ()
	{
		PlayerPrefs.SetInt("sdrn", 0);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void EndGames()
	{
		if(cl == false)
        {
			if (gameHasEnded == false)
			{
				gameHasEnded = true;
				Debug.Log("GAME OVER");
				controls.SetActive(false);

				Time.timeScale = 0.5f;
				new WaitForSeconds(5f);
				Restartmenu.SetActive(true);
				//sco.SaveHScore();
				PlayerPrefs.SetInt("sdrn", 0);




				new WaitForSeconds(1);

				if (gameHasEnded == true)
				{
					rscreen = true;
					Debug.Log("Displayed");
					Restartmenu.SetActive(true);
				}
			}
        }
        else
        {
			Debug.Log("You Are in godmode because you cl");
        }
	}
	public void Update()
	{
		if (rb.position.y < -1f)
		{
			EndGames();
		}
	}
}

