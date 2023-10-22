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
	public GameObject pauseButton;

	public bool _disapper = false;

	public ScoreOnline sco;

	public bool cl = false;
        public bool isGodMode = false;
        public playermovement movement;

	public void restartGame()
	{
		const string Message = "RESTART";
		Debug.Log(Message);
		Invoke("Restart", restartDelay);
	}


    private void Start()
    {
        movement = FindObjectOfType<playermovement>();
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
            if (isGodMode == false)
            {
				if (gameHasEnded == false)
				{
					gameHasEnded = true;
					//Debug.Log("GAME OVER");
					_disapper = true;

					Time.timeScale = 0.5f;
					new WaitForSeconds(5f);
					Restartmenu.SetActive(true);
					PlayerPrefs.SetInt("sdrn", 0);
					movement.enabled = false;




					new WaitForSeconds(1);

					if (gameHasEnded == true)
					{
						rscreen = true;
						//Debug.Log("Displayed");
						Restartmenu.SetActive(true);
					}
				}
            }
            else
            {
            	//Debug.Log("You GodMode Cheater!!!!!");
            }
        }
        else
        {
	    	//Debug.Log("You Are in godmode because you cl");
        }
	}
	public void Update()
	{
		if (rb.position.y < -1f)
		{
            if (!isGodMode)
            {
				EndGames();
            }
            else
            {
                Transform pm = movement.gameObject.transform;
                float x = 0f;
                float y = 1f;
                float z = pm.position.z;
                pm.position = new Vector3(x, y, z);
                //Debug.Log("You GodMode Cheater!!!!!");
            }
		}

		if (_disapper)
		{
			controls.SetActive(false);
			pauseButton.SetActive(false);
		}
		else
		{
			controls.SetActive(true);
			pauseButton.SetActive(true);
		}
	}
}

