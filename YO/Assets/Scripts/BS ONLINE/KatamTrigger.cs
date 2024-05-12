using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Utilities;

public class KatamTrigger : MonoBehaviour
{
    public PlayerSpawner playerSpawn;
    public PhotonView PV;
    public PlayerSpawner Get;
    public GameObject Heart;
    //public Player _player;

    //Others
    public GameObject nameScreen = null;
    public InputField nameField = null;

    void Start()
    {
        if (PhotonNetwork.InRoom == true)
        {
            Get = FindObjectOfType<PlayerSpawner>();
            Heart.SetActive(true);
        }
        else
        {
            if (Heart != null)
            {
                Heart.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Heart.SetActive(true);
        }
        else
        {
            Heart.SetActive(false);
        }
    }

	void OnTriggerEnter()
	{
        if (Get.Hearts == 2)
        {
            PhotonNetwork.Destroy(PV);
            Get.Hearts += 1;
        }
        if (Get.Hearts == 1)
        {
            PhotonNetwork.Destroy(PV);
            Get.Hearts += 1;
        }

        if (Get.Hearts == 0)
        {
            Get.Hearts = 0;
        }
        if (Get.Hearts == 3)
        {
            Get.Hearts = 3;
        }
    }

    #region Trailer_Scene
    public void ConnectOld()
    {
        PlayerPrefs.SetString("intro", "katam");
        string achievementKey = "Achievement_4";
        if (PlayerPrefs.GetInt(achievementKey) == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(4);
        }
        SceneManager.LoadScene("connecttoserver");
    }

    public void Connect()
    {
        nameScreen.SetActive(true);
    }

    //Random PlayerName
    public void SetPlayerName()
    {
        string[] nouns = FindObjectOfType<Achiever>().playerNameSuffix;
        string[] names = FindObjectOfType<Achiever>().playerNames;

        int randNoun = Random.Range(0, nouns.Length);
        int rand = Random.Range(0, 10000);
        int randName = Random.Range(0, names.Length);
        string player_name = names[randName] + nouns[randNoun] + rand.ToString("0000");
        if (player_name.Length > 15)
        {
            string playerName = player_name.Substring(0, 15);
            PlayerPrefs.SetString("PlayerName", playerName);
            PhotonNetwork.NickName = playerName;
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", player_name);
            PhotonNetwork.NickName = player_name;
            nameField.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void SavePlayerName()
    {
        if (nameField.text.Length >= 1)
        {
            PlayerPrefs.SetString("PlayerName", nameField.text);
            PhotonNetwork.NickName = nameField.text;
            nameField.text = PlayerPrefs.GetString("PlayerName");
            PlayerPrefs.SetString("intro", "katam");
            SceneManager.LoadScene("Lobby 1");
        }
        else
        {
            SetPlayerName();
            PlayerPrefs.SetString("intro", "katam");
            SceneManager.LoadScene("Lobby 1");
        }
    }
    #endregion
}
