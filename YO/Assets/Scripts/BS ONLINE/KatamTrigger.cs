using UnityEngine;
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

    void Start()
    {
        if (PhotonNetwork.InRoom == true)
        {
            Get = FindObjectOfType<PlayerSpawner>();
            Heart.SetActive(true);
        }
        else
        {
            Heart.SetActive(false);
        }

        if (!(SceneManager.GetActiveScene().name == "Game"))
        {
            Heart.SetActive(false);
        }
        else
        {
            Heart.SetActive(true);
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

    public void Connect()
    {
        PlayerPrefs.SetString("intro", "katam");
        string achievementKey = "Achievement_4";
        if (PlayerPrefs.GetInt(achievementKey) == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(4);
        }
        SceneManager.LoadScene("connecttoserver");
    }
}
