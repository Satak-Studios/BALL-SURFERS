using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Satak.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CharacterSync : MonoBehaviourPunCallbacks
{
    public new PhotonView photonView;
    public Material BodyColor;
    public GameObject tempBody;
    public GameObject[] eyes;
    public GameObject[] hats;
    public GameObject[] mouth;

    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedHat = 0;

    public bool lockRotation = true;
    public bool _okay = false;
    CompManager compManager;
    CustomManager custManager;
    PlayerSpawner playerSpawner;
    public bool compMode = false;
    public bool defaultMode = false;

    private void Start()
    {
        LoadPlayerCustomization();
        SyncCharacter();
        SetVisualCustomization(selectedEyes, selectedMouth, selectedBodyColor, selectedHat);
    }

    private void LoadPlayerCustomization()
    {
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedMouth = PlayerPrefs.GetInt("mouth");
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedHat = PlayerPrefs.GetInt("eyeColor");
    }

    private void Update()
    {
        if (lockRotation)
        {
            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.x = 0f;
            newRotation.y = 180f;
            newRotation.z = 0f;
            transform.rotation = Quaternion.Euler(newRotation);
        }

        if (!(selectedEyes + selectedMouth == 0) && SceneManager.GetActiveScene().buildIndex > 0)
        {
            if (PlayerPrefs.GetInt("Achievement_2") == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(2);
            }
        }

        if (selectedMouth > 2 && _okay)
        {
            lockRotation = false;
        }
        else if (_okay && selectedMouth < 3)
        {
            lockRotation = true;
        }
        else
        {
            lockRotation = false;
        }

        if (compMode)
        {
            compManager = FindObjectOfType<CompManager>();
        }
        else if (!compMode && !defaultMode)
        {
            custManager = FindObjectOfType<CustomManager>();
        }
        if (defaultMode)
        {
            playerSpawner = FindObjectOfType<PlayerSpawner>();
        }
        SyncCharacter();
    }

    private void SetVisualCustomization(int eyesIndex, int mouthIndex, int bodyColorIndex, int hatIndex)
    {
        tempBody.GetComponent<MeshRenderer>().material.color = bodyColorIndex switch
        {
            0 => Color.red,
            1 => Color.black,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => Color.red
        };       
        SetActiveGameObject(eyes, eyesIndex);
        SetActiveGameObject(mouth, mouthIndex);
        SetActiveGameObject(hats, hatIndex);
    }

    void SyncCharacter()
    {
        if (photonView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("eyes", selectedEyes);
            hash.Add("mouth", selectedMouth);
            hash.Add("bodyColor", selectedBodyColor);
            hash.Add("eyeColor", selectedHat);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    private void SetActiveGameObject(GameObject[] objects, int index)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == index);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (compMode && !defaultMode)
        {
            if (!photonView.IsMine && targetPlayer == photonView.Owner)// && compManager.gameStarted)
            {
                if (changedProps != null)
                {
                    int eyes = (int)changedProps["eyes"];
                    int mouth = (int)changedProps["mouth"];
                    int bodyColor = (int)changedProps["bodyColor"];
                    int hat = (int)changedProps["eyeColor"];

                    SetVisualCustomization(eyes, mouth, bodyColor, hat);
                }
            }
        }
        else if (!compMode && !defaultMode)
        {
            if (!photonView.IsMine && targetPlayer == photonView.Owner)// && custManager.gameStarted)
            {
                int eyes = (int)changedProps["eyes"];
                int mouth = (int)changedProps["mouth"];
                int bodyColor = (int)changedProps["bodyColor"];
                int hat = (int)changedProps["eyeColor"];

                SetVisualCustomization(eyes, mouth, bodyColor, hat);
            }
        }
        else
        {
            if (!photonView.IsMine && targetPlayer == photonView.Owner && playerSpawner.Hearts > 0)
            {
                if (changedProps != null)
                {
                    int eyes = (int)changedProps["eyes"];
                    int mouth = (int)changedProps["mouth"];
                    int bodyColor = (int)changedProps["bodyColor"];
                    int hat = (int)changedProps["eyeColor"];

                    SetVisualCustomization(eyes, mouth, bodyColor, hat);
                }
            }
        }
    }
}
