using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class SaveFileManager : MonoBehaviour
{
    public Button[] SaveFiles;
    public Button[] LoadFiles;
    public GameObject[] deleteFiles;
    public string saveFileLocation = "/PlayerData";
    string fileType = ".satak";

    void Update()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (File.Exists(Application.persistentDataPath + saveFileLocation + i + fileType))
            {
                LoadFiles[i].interactable = true;
                LoadTitleFromFile(i);
                LoadSaveTitleFromFile(i);
                deleteFiles[i].SetActive(true);
            }
            else
            {
                LoadFiles[i].GetComponentInChildren<Text>().text = "Empty";
                SaveFiles[i].GetComponentInChildren<Text>().text = "Empty";
                deleteFiles[i].SetActive(false);
                LoadFiles[i].interactable = false;
            }
        }
    }

    void LoadTitleFromFile(int i)
    {
        string _path = Application.persistentDataPath + saveFileLocation + i + ".txt";
        if (File.Exists(_path))
        {
            StreamReader reader = new StreamReader(_path);
            LoadFiles[i].GetComponentInChildren<Text>().text = reader.ReadToEnd();
            reader.Close();
        }
    }

    void LoadSaveTitleFromFile(int i)
    {
        string _path = Application.persistentDataPath + saveFileLocation + i + ".txt";
        if (File.Exists(_path))
        {
            StreamReader reader = new StreamReader(_path);
            SaveFiles[i].GetComponentInChildren<Text>().text = reader.ReadToEnd();
            reader.Close();
        }
    }
}
