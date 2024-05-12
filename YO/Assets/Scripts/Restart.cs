using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	[SerializeField] public GameObject Restartmenu;
	public float restartDelay = 1f;
	public bool gameHasEnded = false;
	public bool rscreen;
	public GameObject pauseButton;
	public GameObject cameraBtn;

	public bool _disapper = false;
	public bool isOnline = false;

	public ScoreOnline sco;

	public bool cl = false;
    public bool isGodMode = false;
	Device _device;

	public Vector3 katamPoint;

    private void Start()
    {
		_device = FindObjectOfType<Device>();
	}


    public void RestartGame ()
	{
		//PlayerPrefs.SetInt("sdrn", 0);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void EndGames(float z)
	{
		if (cl == false)
		{
			if (isGodMode == false)
			{
				if (gameHasEnded == false)
				{
					SavePos(z);
					gameHasEnded = true;
					_disapper = true;
					_device.isInvisible = true;

					Time.timeScale = 0.5f;
					new WaitForSeconds(5f);
					Restartmenu.SetActive(true);
					PlayerPrefs.SetInt("sdrn", 0);
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
	}

	public void Update()
	{
		if (!isOnline)
		{
			if (_disapper)
			{
				_device.isInvisible = true;
				pauseButton.SetActive(false);
				cameraBtn.SetActive(false);
			}
			else
			{
				if (SystemInfo.deviceType == DeviceType.Handheld)
				{
					_device.isInvisible = false;
				}
				else
				{
					_device.isInvisible = true;
				}
				pauseButton.SetActive(true);
				cameraBtn.SetActive(true);
			}
		}
	}

	void SavePos(float z)
    {
		katamPoint.x = 0f;
		katamPoint.y = 1.5f;
		katamPoint.z = z;
	}
}

