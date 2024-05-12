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
    public Material EyeColor;
    public GameObject tempBody;
    public GameObject[] tempEye;
    public GameObject[] eyes;
    public GameObject[] mouth;

    public int selectedEyes = 0;
    public int selectedMouth = 0;
    public int selectedBodyColor = 0;
    public int selectedEyeColor = 0;

    public bool lockRotation = true;
    CompManager compManager;
    CustomManager custManager;
    PlayerSpawner playerSpawner;
    public bool compMode = false;
    public bool defaultMode = false;

    private void Start()
    {
        LoadPlayerCustomization();
        SyncCharacter();
        SetVisualCustomization(selectedEyes, selectedMouth, selectedBodyColor, selectedEyeColor);
    }

    private void LoadPlayerCustomization()
    {
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedMouth = PlayerPrefs.GetInt("mouth");
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedEyeColor = PlayerPrefs.GetInt("eyeColor");
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

    private void SetVisualCustomization(int eyesIndex, int mouthIndex, int bodyColorIndex, int eyeColorIndex)
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
        if (tempEye[eyeColorIndex].GetComponent<Material>() != null)
        {
            tempEye[eyesIndex].GetComponent<Material>().color = eyeColorIndex switch
            {
                0 => Color.black,
                1 => Color.white,
                _ => Color.black
            };
        }
        SetActiveGameObject(eyes, eyesIndex);
        SetActiveGameObject(mouth, mouthIndex);
    }

    void SyncCharacter()
    {
        if (photonView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("eyes", selectedEyes);
            hash.Add("mouth", selectedMouth);
            hash.Add("bodyColor", selectedBodyColor);
            hash.Add("eyeColor", selectedEyeColor);
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
                    int eyeColor = (int)changedProps["eyeColor"];

                    SetVisualCustomization(eyes, mouth, bodyColor, eyeColor);
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
                int eyeColor = (int)changedProps["eyeColor"];

                SetVisualCustomization(eyes, mouth, bodyColor, eyeColor);
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
                    int eyeColor = (int)changedProps["eyeColor"];

                    SetVisualCustomization(eyes, mouth, bodyColor, eyeColor);
                }
            }
        }
    }
}
