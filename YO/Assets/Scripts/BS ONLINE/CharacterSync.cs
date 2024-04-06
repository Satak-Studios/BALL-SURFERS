using UnityEngine;
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

    private void Start()
    {
        //if (photonView.IsMine)
        {
            // Initialize customization for local player
            LoadPlayerCustomization();
            InvokeRepeating("SyncCharacter", 2.0f, 5f);
            SyncCharacter();
            SetVisualCustomization(selectedEyes, selectedMouth, selectedBodyColor, selectedEyeColor);
        }
    }

    private void LoadPlayerCustomization()
    {
        // Load customization data from PlayerPrefs
        selectedEyes = PlayerPrefs.GetInt("eyes");
        selectedMouth = PlayerPrefs.GetInt("mouth");
        selectedBodyColor = PlayerPrefs.GetInt("bodyColor");
        selectedEyeColor = PlayerPrefs.GetInt("eyeColor");
    }

    public void SetPlayerInfo(Player _player)
    {
        eyes[SatakExtensions.GetPlayerEyes(_player)].SetActive(true);
        mouth[SatakExtensions.GetPlayerMouth(_player)].SetActive(true);
        //BodyColor.color = SatakExtensions.GetPlayerBodyColor(_player) switch
        tempBody.GetComponent<MeshRenderer>().material.color = SatakExtensions.GetPlayerBodyColor(_player) switch
        {
            0 => Color.black,
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => Color.red
        };
        //EyeColor.color = SatakExtensions.GetPlayerEyeColor(_player) switch
        tempEye[selectedEyes].GetComponent<Material>().color = SatakExtensions.GetPlayerEyeColor(_player) switch
        {
            0 => Color.black,
            1 => Color.white,
            _ => Color.black,
        };
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

        if (!(selectedEyes + selectedMouth == 0))
        {
            if (PlayerPrefs.GetInt("Achievement_2") == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(2);
            }
        }

        compManager = FindObjectOfType<CompManager>();
    }

    private void SetVisualCustomization(int eyesIndex, int mouthIndex, int bodyColorIndex, int eyeColorIndex)
    {
        //BodyColor.color = bodyColorIndex switch
        tempBody.GetComponent<MeshRenderer>().material.color = bodyColorIndex switch
        {
            0 => Color.black,
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.yellow,
            5 => Color.magenta,
            _ => Color.red
        };
        //EyeColor.color = eyeColorIndex switch
        tempEye[eyesIndex].GetComponent<Material>().color = eyeColorIndex switch
        {
            0 => Color.black,
            1 => Color.white,
            _ => Color.black
        };
        SetActiveGameObject(eyes, eyesIndex);
        SetActiveGameObject(mouth, mouthIndex);
    }

    void SyncCharacter()
    {
        if (photonView.IsMine)
        {
            // Synchronize customization across the network
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
        if (!photonView.IsMine && targetPlayer == photonView.Owner && compManager.gameStarted)
        {
            int eyes = (int)changedProps["eyes"];
            int mouth = (int)changedProps["mouth"];
            int boddColor = (int)changedProps["bodyColor"];
            int eyeColor = (int)changedProps["eyeColor"];

            SetVisualCustomization(eyes, mouth, boddColor, eyeColor);
        }
    }
}
