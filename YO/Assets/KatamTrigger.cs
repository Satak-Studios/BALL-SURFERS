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
        //PV = GetComponent<PhotonView>();
    }
	void OnTriggerEnter()
	{

        /*  if (PhotonNetwork.LocalPlayer.GetHearts() == 2)
          {
              PhotonNetwork.Destroy(PV);
              //layerSpawn.timesKatam -= 1;
              PhotonNetwork.LocalPlayer.AddHearts(+1);
              Debug.Log("1 Heart Added");
          }
          if (PhotonNetwork.LocalPlayer.GetHearts() == 1)
          {
              PhotonNetwork.Destroy(PV);
              //layerSpawn.timesKatam -= 1;
              PhotonNetwork.LocalPlayer.AddHearts(+1);
              Debug.Log("1 Heart Added");
          }

          if (PhotonNetwork.LocalPlayer.GetHearts() == 0)
          {
              PhotonNetwork.LocalPlayer.SetHearts(0);
              //playerSpawn.timesKatam = 0;
              Debug.Log("0 Heart Added");
          }
          if (PhotonNetwork.LocalPlayer.GetHearts() == 3)
          {
              PhotonNetwork.LocalPlayer.SetHearts(3);
              //playerSpawn.timesKatam = 0;
              Debug.Log("0 Heart Added");
          }*/

        if (Get.Hearts == 2)
        {
            PhotonNetwork.Destroy(PV);
            //layerSpawn.timesKatam -= 1;
            Get.Hearts += 1;
            Debug.Log("1 Heart Added");
        }
        if (Get.Hearts == 1)
        {
            PhotonNetwork.Destroy(PV);
            //layerSpawn.timesKatam -= 1;
            Get.Hearts += 1;
            Debug.Log("1 Heart Added");
        }

        if (Get.Hearts == 0)
        {
            Get.Hearts = 0;
            //playerSpawn.timesKatam = 0;
            Debug.Log("0 Heart Added");
        }
        if (Get.Hearts == 3)
        {
            Get.Hearts = 3;
            //playerSpawn.timesKatam = 0;
            Debug.Log("0 Heart Added");
        }
    }

    /*public void OnCollisionEnter(Collision collisionInfo)
    {
            if (collisionInfo.collider.tag == "Player")
            {
            playerSpawn.timesKatam -= 1;
            Debug.Log("1 Heart Added");
            }
    }*/

    public void Connect()
    {
        //SceneManager.LoadScene("connecttoserver");
        PlayerPrefs.SetString("intro", "katam");
        SceneManager.LoadScene("Game 01");
    }
    public void Proceed()
    {
        SceneManager.LoadScene("connecttoserver");
    }
}
