using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string word;
    public GameObject Alert;
    public string[] bw;

    private void Update()
    {
        if (word == "bw")
        {
            Alert.SetActive(true);
        }
        else
        {
            Alert.SetActive(false);
        }
    }
}