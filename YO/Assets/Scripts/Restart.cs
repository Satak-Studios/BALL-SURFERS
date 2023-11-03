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

    private void Start()
    {
        movement = FindObjectOfType<playermovement>();
		rb = movement.rb;
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
					_disapper = true;

					Time.timeScale = 0.5f;
					new WaitForSeconds(5f);
					Restartmenu.SetActive(true);
					PlayerPrefs.SetInt("sdrn", 0);
					movement.enabled = false;
					int band = PlayerPrefs.GetInt("band", 0) + 1;
					PlayerPrefs.SetInt("band", band);



					new WaitForSeconds(1);

					if (gameHasEnded == true)
					{
						rscreen = true;
						Restartmenu.SetActive(true);
					}
				}
            }
        }
        else
        {

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

