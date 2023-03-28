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
    }
	void OnTriggerEnter()
	{
        if (Get.Hearts == 2)
        {
            PhotonNetwork.Destroy(PV);
            Get.Hearts += 1;
            Debug.Log("1 Heart Added");
        }
        if (Get.Hearts == 1)
        {
            PhotonNetwork.Destroy(PV);
            Get.Hearts += 1;
            Debug.Log("1 Heart Added");
        }

        if (Get.Hearts == 0)
        {
            Get.Hearts = 0;
            Debug.Log("0 Heart Added");
        }
        if (Get.Hearts == 3)
        {
            Get.Hearts = 3;
            Debug.Log("0 Heart Added");
        }
    }

    public void Connect()
    {
        PlayerPrefs.SetString("intro", "katam");
        SceneManager.LoadScene("Lobby 1");
    }
}
