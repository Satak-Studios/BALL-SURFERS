using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public Vector3 katamPoint;
    public bool foundSaveFile = false;
    public Device _device;
    public GameObject LoadOldGameScreen;
    public GameObject TutorialItems;
    public GameObject PlayerObj;
    public GameObject noobScreen;
    public GameObject dialogPC;
    public GameObject dialogMobile;

    void Start()
    {
        TutorialItems.SetActive(false);
        LookForOldSaveFiles();
        dialogPC.SetActive(false);
        dialogMobile.SetActive(false);
    }

    public void OldPlayer()
    {
        if (foundSaveFile)
        {
            LoadOldGameScreen.SetActive(true);
        }
        else
        {
            LoadMainMenu();
            LoadOldGameScreen.SetActive(false);
        }
    }

    public void NewPlayer()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            dialogPC.SetActive(true);
            dialogMobile.SetActive(false);
        }
        else
        {
            dialogPC.SetActive(false);
            dialogMobile.SetActive(true);
        }
        PlayerObj.SetActive(false);
        _device.isInvisible = true;
        TutorialItems.SetActive(true);
        noobScreen.SetActive(false);
    }

    public void LookForOldSaveFiles()
    {
        string _path = Application.persistentDataPath + "/Player.satak"; ;
        if (File.Exists(_path))
        {
            foundSaveFile = true;
        }
        else
        {
            foundSaveFile = false;
        }
    }

    public SaveData LoadOldData()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        Debug.Log("Game Loaded");
        SaveData saveData = new SaveData();
        ResumeData data = formatter.Deserialize(stream) as ResumeData;
        stream.Close();

        saveData._hScore = Mathf.RoundToInt(data.HighScore);
        saveData._userName = data.PlayerName;
        saveData._level = data.Level;
        File.Move(path, Application.persistentDataPath + "/PlayerDataOld.satak");
        FindObjectOfType<Achiever>().Notify("Yay!", "Welcome back " + saveData._userName);
        SaveSystem.SaveCustomData(saveData);
        LoadMainMenu();
        return saveData;
    }

    public void CopyAndOldLoadSaveFile()
    {
        LoadOldData();
    }

    public void OnClickOkTutorial()
    {
        PlayerPrefs.SetString("pro", "pro");
        dialogPC.SetActive(false);
        dialogMobile.SetActive(false);
        PlayerObj.SetActive(true);
        _device.isInvisible = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetString("v2.0", "Welcome but please dont hack/cheat like this ever again");
    }

    public void EndGame()
    {
        _device.isInvisible = true;
        RetryLevel();
    }


    public void RetryLevel()
    {
        PlayerObj.transform.position = new Vector3(0, 0, 0);
    }
}
