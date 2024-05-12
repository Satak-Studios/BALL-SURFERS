using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    static string fileName = "0";
    public GameObject newGameWarn;
    public GameObject saveWarn;
    public GameObject loadWarn;
    public GameObject delWarn;
    static string _title;
    public ErrorThrower errorThrower;
    public progress _progress;
    string _path;

    void Update()
    {
        _title = (PlayerPrefs.GetInt("xp") / 300).ToString("F2");
    }

    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + fileName + ".satak";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
        if (File.Exists(Application.persistentDataPath + fileName + ".txt"))
        {
            File.Delete(Application.persistentDataPath + fileName + ".txt");
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + fileName + ".txt", true);
            writer.WriteLine(_title + "% Complete");
            writer.Close();
            Debug.Log("Deleting");
        }
        else
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + fileName + ".txt", true);
            writer.WriteLine(_title + "% Complete");
            writer.Close();
        }
        fileName = "/PlayerData";
    }

    public SaveData LoadGame()
    {
        string path = Application.persistentDataPath + fileName + ".satak";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        SaveData data = formatter.Deserialize(stream) as SaveData;
        stream.Close();
        FindObjectOfType<Achiever>().Notify("Yay!", "Welcome back " + data._userName);
        fileName = "/PlayerData";
        return data;   
    }

    public void SaveGameFile()
    {
        SaveGame();
        FindObjectOfType<Achiever>().Notify("Done!", "Game Saved Sucessfully!");
        //errorThrower.ThrowError("111111", "Game Saved Sucessfully!", "Done!");
    }   

    public void SaveGameFake(int saveSlot)
    {
        fileName = "/PlayerData" + saveSlot.ToString();
        Debug.Log("FileName = " + fileName);
        saveWarn.SetActive(true);
    }

    public void LoadGameFake(int saveSlot)
    {
        fileName = "/PlayerData" + saveSlot.ToString();
        if (File.Exists(Application.persistentDataPath + fileName + ".satak"))
            loadWarn.SetActive(true);
        else
            newGameWarn.SetActive(true);
    }

    public void LoadGameFile()
    {
        LoadGame();
    }

    public void DeleteSaveFake(int saveSlot)
    {
        _path = Application.persistentDataPath + "/PlayerData" + saveSlot.ToString();
        delWarn.SetActive(true);
    }

    public void DeleteSaveFile()
    {
        File.Delete(_path + ".satak");
        File.Delete(_path + ".txt");
        FindObjectOfType<Achiever>().Notify("Done!", "Deleted Sucessfully!");
    }

    public static void SaveCustomData(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "PlayerData1.satak";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
        if (File.Exists(Application.persistentDataPath + "PlayerData1.txt"))
        {
            File.Delete(Application.persistentDataPath + fileName + ".txt");
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "PlayerData1.txt", true);
            writer.WriteLine(_title + "% Complete");
            writer.Close();
            Debug.Log("Deleting");
        }
        else
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "PlayerData1.txt", true);
            writer.WriteLine(_title + "% Complete");
            writer.Close();
        }
    }
}