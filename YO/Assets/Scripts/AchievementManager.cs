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
    public Sprite[] DefaultSprites;
    public Slider[] sliders;
    public Text[] statusText;

    public GameObject[] AchPanels;
    public GameObject upBtn;
    public GameObject downBtn;
    public int currentIndex = 0;

    public Text welcomeStatus;

    // Start is called before the first frame update
    void Start()
    {
        upBtn.SetActive(false);
        _achiever = FindObjectOfType<Achiever>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void LockAchievement(int index)
    {
        CompleteObjects[index].SetActive(false);
        Icons[index].sprite = DefaultSprites[index];
    }

    public void Down()
    {
        if (currentIndex < AchPanels.Length - 1)
        {
            AchPanels[currentIndex].SetActive(false);
            currentIndex++;
            AchPanels[currentIndex].SetActive(true);
        }

        upBtn.SetActive(true);
        downBtn.SetActive(currentIndex < AchPanels.Length - 1);
    }

    public void Up()
    {
        if (currentIndex > 0)
        {
            AchPanels[currentIndex].SetActive(false);
            currentIndex--;
            AchPanels[currentIndex].SetActive(true);
        }

        upBtn.SetActive(currentIndex > 0);
        downBtn.SetActive(true);
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
}
