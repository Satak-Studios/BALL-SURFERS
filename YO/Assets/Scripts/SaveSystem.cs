using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Photon.Chat.UtilityScripts;

public class SaveSystem : MonoBehaviour
{
    private FirebaseFirestore firestore;
    public InputField secretKeyHolder;
    public Toggle _remember;
    public GameObject saveWarn;
    public GameObject loadWarn;
    public GameObject mainWarn;
    private string secretKey = "0";

    public ErrorThrower errorThrower;

    void Awake()
    {
        firestore = FirebaseFirestore.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_remember.isOn)
        {
            PlayerPrefs.SetString("secretKey", secretKey);
        }else
        {
            PlayerPrefs.DeleteKey("secretKey");
        }
    }

    public void SecretKey()
    {
        if (secretKeyHolder.text == "")
        {
            errorThrower.ThrowError("Save:(", "Make sure you have entered the secret key.", "Oops!");
        }
        else
        {
            secretKey = secretKeyHolder.text;
        }
    }

    public void SaveFake()
    {
        if (PlayerPrefs.HasKey("secretKey"))
        {
            secretKey = PlayerPrefs.GetString("secretKey");
            CheckNetwork(false);
            saveWarn.SetActive(false);
        }else
        {
            saveWarn.SetActive(true);
            mainWarn.SetActive(true);
        }
    }

    public void CheckNetwork(bool toDoLoad)
    {
        NetworkReachability reachability = Application.internetReachability;

        switch (reachability)
        {
            case NetworkReachability.NotReachable:
                errorThrower.ThrowError("404_:(", "Make sure you are connected to the internet.", "Oops!");
            break;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                errorThrower.ThrowError("🤨DataNetwork?🤨", "Are you sure to play BALL SURFERS using your Mobile Data?", "Sure?");
                if (toDoLoad)
                {
                    LoadFromCloud();
                }else
                {
                    SaveToCloud();
                }
            break;

            case NetworkReachability.ReachableViaLocalAreaNetwork:
                if (toDoLoad)
                {
                    LoadFromCloud();
                }else
                {
                    SaveToCloud();
                }
            break;
        }
    }

    public async void SaveToCloud()
    {
        if (errorThrower.networkAvailable)
        {
            SaveData saveData = new SaveData();
            await firestore.Document($"save_data/" + secretKey).SetAsync(saveData);
            FindObjectOfType<Achiever>().Notify("Done!", "Game Saved Sucessfully!");
            errorThrower.ThrowError("111111", "Game Saved Sucessfully!", "Done!");
        }else
        {
            errorThrower.CheckInternet();
        }
    }

    public async void LoadFromCloud()
    {
        var snapshot = await firestore.Document($"save_data/" + secretKey).GetSnapshotAsync();
        if (snapshot.Exists)
        {
            var data = snapshot.ConvertTo<SaveData>();
            SaveData saveData = new SaveData();
            saveData.UserName = data.UserName;
            saveData.Level = data.Level;
            saveData.HighScore = data.HighScore;
            saveData.Achievements = data.Achievements;
            saveData.Eye = data.Eye;
            saveData.EyeColor = data.EyeColor;
            saveData.BodyColor = data.BodyColor;
            saveData.Mouth = data.Mouth;
            saveData.Bandage = data.Bandage;
            FindObjectOfType<Achiever>().Notify("Done!", "Game Loaded Sucessfully!");
            FindObjectOfType<Achiever>().Notify("Yay!", "Welcome back " + data.UserName);
            errorThrower.ThrowError("111111", "Game Loaded Sucessfully!", "Done!");
        }else
        {
            FindObjectOfType<Achiever>().Notify("Error!", "No such game progress");
            errorThrower.ThrowError("Game404", "No such game progress", "Error");
        }
    }
}
