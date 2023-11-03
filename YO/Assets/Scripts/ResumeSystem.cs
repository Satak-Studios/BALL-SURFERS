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

    }

    public static ResumeData LoadFile()
    {
        string path = Application.persistentDataPath + "/Player.satak";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ResumeData data = formatter.Deserialize(stream) as ResumeData;
            stream.Close();

            return data;
         
        }
        else
        {
            return null;
        }
 
    }
}