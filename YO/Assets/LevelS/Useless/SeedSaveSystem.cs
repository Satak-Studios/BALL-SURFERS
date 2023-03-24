using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SeedSaveSystem// : MonoBehaviour
{
    public static int randno = Random.Range(10, 100);
    public static string pathh = "/PlayerSeed.level";
    static int randn;

    public static void SaveFile(RandObjSaver cols)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //int randno = Random.Range(0, 100);
        string path = Application.persistentDataPath + pathh + randno;
        FileStream stream = new FileStream(path, FileMode.Create);

        SeedData data = new SeedData(cols);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Seed Saved as " + randno);
        randn = randno;
        PlayerPrefs.SetInt("randSeed", randno);
    }

    public static SeedData LoadFile(RandObjSaver cols)
    {
        string path = Application.persistentDataPath + pathh + randn;
        randn = PlayerPrefs.GetInt("randSeed", randno); 
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Debug.Log("Seed = " + randn + "Has Loaded");

            SeedData data = formatter.Deserialize(stream) as SeedData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Seed = "+randn + " cannot be found");
            return null;
            randn = PlayerPrefs.GetInt("randSeed", randno);
        }

    }
}