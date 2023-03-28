using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class ResumeSystem// : MonoBehaviour
{
    public static void SaveFile(levelmanager lm)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Player.satak";
        FileStream stream = new FileStream(path, FileMode.Create);

        ResumeData data = new ResumeData(lm);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game Saved");

    }

    public static ResumeData LoadFile()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Debug.Log("Game Loaded");

            ResumeData data = formatter.Deserialize(stream) as ResumeData;
            stream.Close();

            return data;
         
        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
 
    }
}