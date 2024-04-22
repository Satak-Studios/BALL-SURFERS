using UnityEngine;

public class PlaytimeCalculator : MonoBehaviour
{
    public float totalPlaytime;

    private void Start()
    {
        totalPlaytime = PlayerPrefs.GetFloat("TotalPlaytime", 0f);
    }

    private void Update()
    {
        totalPlaytime += Time.deltaTime;

        if (Time.frameCount % 300 == 0)
        {
            PlayerPrefs.SetFloat("TotalPlaytime", totalPlaytime);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TotalPlaytime", totalPlaytime);
        PlayerPrefs.Save();
    }
}
