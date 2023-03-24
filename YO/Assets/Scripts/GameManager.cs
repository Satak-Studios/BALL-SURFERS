using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Utilities;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
	public GameObject completeLevelUI;
	public Restart rs;
	public int level;
	public LevelScript ls;
	public string _name;

	public bool isBSO;
	private PlayerSpawner pso;
	//public Text katamText;
	public Transform KatamArea;
	public GameObject KatamChance;
	public GameObject KatamE;
	//public Player _player;
	public string Cause;

	public bool isKatamObj;

	public void Start()
    {
		//level = PlayerPrefs.GetInt("levelsUnlocked");
	}

	public void CompleteLevel()
	{
		completeLevelUI.SetActive(true);
		PlayerPrefs.SetInt("sdrn", 0);
		rs.cl = true;
		//ls.Pass();
		//SceneManager.LoadScene("levelmanager");
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

    private void Update()
    {


        if (isKatamObj == true)
        {
			Text pName;
			pName = GetComponent<Text>();
			pName.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

	public void Check()
    {
		if (isBSO == true)
		{
			if (PhotonNetwork.LocalPlayer.GetHearts() == 3)
			{
				//PhotonNetwork.Instantiate(KatamObj[1].name, KatamArea.position, Quaternion.identity);
				//katamText.text = _player.NickName + " Got Eliminated";
				//Destroy(KatamObj[1], 10f);
				PhotonNetwork.Instantiate(KatamChance.name, KatamArea.position, Quaternion.identity);
				Destroy(KatamChance, 10f);
				Debug.Log("Eliminated");
			}

			if (PhotonNetwork.LocalPlayer.GetHearts() < 0)
			{
				//PhotonNetwork.Instantiate(KatamObj[0].name, KatamArea.position, Quaternion.identity);
				//Destroy(KatamObj[0], 10f);
				PhotonNetwork.Instantiate(KatamE.name, KatamArea.position, Quaternion.identity);
				Destroy(KatamE, 10f);
				Debug.Log("Katam Chance");
			}
		}
	}
}
