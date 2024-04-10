using UnityEngine.UI;
using UnityEngine;
using Utilities;
using Photon.Chat.UtilityScripts;

public class AchievementManager : MonoBehaviour
{
    public Sprite completedSprite;
    Achiever _achiever;

    public GameObject[] CompleteObjects;
    public Image[] Icons;
    public Image[] AchBackgrounds;
    public Sprite[] DefaultSprites;
    public Slider[] sliders;
    public GameObject[] AchDetails;
    public Text[] statusText;

    public GameObject[] AchPanels;
    public int currentIndex = 0;

    public Text welcomeStatus;

    //PlayTime
    public Text playTimeText;
    public float playtimeInSeconds;

    void Start()
    {       
        _achiever = FindObjectOfType<Achiever>();
        GoToAch();
    }

    void Update()
    {
        playtimeInSeconds = PlayerPrefs.GetFloat("TotalPlaytime", 0f);
        DisplayFormattedPlaytime();
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].interactable = false;
        }

        //Ach4(End?)
        sliders[0].value = PlayerPrefs.GetInt("levelsUnlocked", 1);
        statusText[0].text = PlayerPrefs.GetInt("levelsUnlocked", 1) + "/9";

        //Ach17(End?)
        sliders[1].value = PlayerPrefs.GetInt("levelsUnlocked", 1);
        statusText[1].text = PlayerPrefs.GetInt("levelsUnlocked", 1) + "/100";

        //Ach18(Brit Bandagess)
        sliders[2].value = _achiever.band;
        statusText[2].text = _achiever.band + "/" + "5";

        //Ach19(Resilience 50)
        sliders[3].value = _achiever.band;
        statusText[3].text = _achiever.band + "/50";

        //Ach24(Collector)
        sliders[4].value = PlayerPrefs.GetInt("totalAch");
        statusText[4].text = PlayerPrefs.GetInt("totalAch")+ "/15";

        //Ach25(Master Collector)
        sliders[5].value = PlayerPrefs.GetInt("totalAch");
        statusText[5].text = PlayerPrefs.GetInt("totalAch")+ "/25";

        for (int i = 1; i <= 25; i++)
        {
            if (_achiever.achIndex[i] == 1)
            {
                UnlockAchievement(i);
            }else
            {
                LockAchievement(i);
            }
            Icons[i].color = Color.white;
        }

        if (PlayerPrefs.GetInt("Welcomed") == 1)
        {
            Claimed();
        }
    }

    public void UnlockAchievement(int index)
    {
        CompleteObjects[index].SetActive(true);
        Icons[index].sprite = completedSprite;
        AchBackgrounds[index].color = new Color32(0, 235, 255, 255);
    }

    public void LockAchievement(int index)
    {
        CompleteObjects[index].SetActive(false);
        Icons[index].sprite = DefaultSprites[index];
    }

    public void Claim()
    {
        PlayerPrefs.SetInt("Welcomed", 1);
        PlayerPrefs.SetInt("eyes", 1);
        PlayerPrefs.SetInt("mouth", 2);       
        PlayerPrefs.SetInt("bodyColor", 0);
        PlayerPrefs.SetInt("eyeColor", 0);
    }

    public void Claimed()
    {
        welcomeStatus.text = "Claimed";
    }

    void GoToAch()
    {
        int currentPanel = (PlayerPrefs.GetInt("ImpMark") - 1) / 5;
        int currentAch = PlayerPrefs.GetInt("ImpMark");
        currentPanel = Mathf.Clamp(currentPanel, 0, AchPanels.Length - 1);

        for (int i = 0; i < AchPanels.Length; i++)
        {
            AchPanels[i].SetActive(i == currentPanel);
        }

        for (int i = 0; i < AchDetails.Length; i++)
        {
            AchDetails[i].SetActive(i == currentAch);
        }

        if(PlayerPrefs.GetInt("ImpMark") == 0)
        {
            for (int i = 0; i < AchDetails.Length; i++)
            {
                AchDetails[i].SetActive(i == 1);
            }
        }
    }

    private void DisplayFormattedPlaytime()
    {
        float totalMinutes = playtimeInSeconds / 60f;
        float totalHours = playtimeInSeconds / 3600f;
        float totalDays = playtimeInSeconds / (3600f * 24f);

        string formattedPlaytime;

        if (totalDays >= 1)
        {
            formattedPlaytime = string.Format("{0} Days", Mathf.Floor(totalDays));
        }
        else if (totalHours >= 1)
        {
            formattedPlaytime = string.Format("{0} hr", Mathf.Floor(totalHours));
        }
        else
        {
            formattedPlaytime = string.Format("{0} min", Mathf.Floor(totalMinutes));
        }

        playTimeText.text = "Total Playtime: " + formattedPlaytime;
    }
}
