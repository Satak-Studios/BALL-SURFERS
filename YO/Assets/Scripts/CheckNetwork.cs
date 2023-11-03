using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckNetwork : MonoBehaviour
{
    public GameObject noInternet;

    // Start is called before the first frame update
    void Update()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            noInternet.SetActive(false);
        }
        else
        {
            FindObjectOfType<Achiever>().Notify("Error", "No Internet");
            noInternet.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("connecttoserver");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu 1");
    }
}
