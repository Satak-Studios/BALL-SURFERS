﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    //public GameObject thetenthPanel;

    public void Quit ()
    {
        Application.Quit();
    }

    public void LoadLevelManager()
    {
        gameObject.SetActive(false);
    }
}
