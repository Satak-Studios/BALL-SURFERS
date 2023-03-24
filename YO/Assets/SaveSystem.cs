using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameData gd;
    int currentScore;
    string currentName; 
    float currentLevel;

    void Start()
    {
        SaveFile();
        LoadFile();
        currentScore = gd.score;
        currentName = gd.name;
        currentLevel = gd.Level;
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(currentScore, currentName, currentLevel);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        currentName = data.name;
        currentLevel = data.Level;

        Debug.Log(data.name);
        Debug.Log(data.score);
        Debug.Log(data.Level);
    }

}