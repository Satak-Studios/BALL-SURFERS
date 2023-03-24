using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AutoSave : MonoBehaviour
{

    string destination = "player.fun";

    public void Loading()
    {
        Load();
    }

    void Start()
    {
        InvokeRepeating("Save", 5.0f, 0.25f);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + destination);
        AutoData data = new AutoData();
        if (File.Exists(Application.persistentDataPath + destination))
        {
            File.OpenWrite(Application.persistentDataPath + destination);
            bf.Serialize(file, data);
            file.Close();
        }
        //hundreds of values
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + destination))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + destination, FileMode.Open);
            AutoData data = (AutoData)bf.Deserialize(file);
            file.Close();
            //hundreds of values
        }

    }
}

 