using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achiever : MonoBehaviour
{
    public int band = 0;
    public Animator _anim;
    public int[] achIndex;
    public int totalAch = 0;

    public GameObject AchievementObj;
    public Text Subj;
    public Text Title;

    private static Achiever instance;
    public bool delete = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadAchievements();
    }

    // Update is called once per frame
    void Update()
    {
        //Casual Surfer
        if (PlayerPrefs.GetInt("levelsUnlocked", 1) >= 2  && achIndex[3] == 0)
        {
            AchievementUnlocked(3);
        }

        //End?
        else if(PlayerPrefs.GetInt("levelsUnlocked", 1) >= 9 && achIndex[6] == 0)
        {
            AchievementUnlocked(6);
        }

        //Century
        else if (PlayerPrefs.GetInt("levelsUnlocked", 1) == 100 && achIndex[17] == 0)
        {
            AchievementUnlocked(17);
        }

        //Collector
        if(totalAch >= 15 && achIndex[24] == 0)
        {
            AchievementUnlocked(24);
        }else if (totalAch == 25 && achIndex[4] == 0)//MasterCollector
        {
            AchievementUnlocked(25);
        }

        band = PlayerPrefs.GetInt("band");
        if (band == 5 && SceneManager.GetActiveScene().buildIndex == 0 && achIndex[18] == 0)//gritBandage
        {
            achIndex[18] = 1;
            AchievementUnlocked(18);
        }
        else if(band == 50 && SceneManager.GetActiveScene().buildIndex == 0 && achIndex[18] == 0)//Resilience 50
        {
            achIndex[19] = 1;
            AchievementUnlocked(19);
        }

        if (PlayerPrefs.GetInt("totalAch") > 25)
        {
            totalAch = 25;
            PlayerPrefs.SetInt("totalAch", 25);
        }
    }

    public void AchievementUnlocked(int index)
    {
        AchievementObj.SetActive(true);
        Title.text = "Achievement Unlocked";
        Subj.text = index switch
        {
            1 => "Welcome",//
            2 => "Costumes", // 
            3 => "Novice Surfer",//
            4 => "Multiplayer?",//
            5 => "Cheater!",//
            6 => "End?",//
            7 => "Infinity?",//
            8 => "Crazy Mode?",//      
            9 => "Casual Surfer",//
            10 => "Intermediate Surfer",//
            11 => "Advanced Surfer",//
            12 => "Expert Surfer",//
            13 => "Master Surfer",//
            14 => "GrandMaster Surfer",//
            15 => "Legend Surfer",//
            16 => "End!",//
            17 => "Century!",//
            18 => "Grit Bandage!",//
            19 => "Resilience 50",//
            20 => "Winner!",//
            21 => "Close!",//
            22 => "Third!",//
            23 => "Social Person",//
            24 => "Collector",//
            25 => "Master Collector",//
            _ => "Error :("//
        };
        achIndex[index] = 1;
        SaveAchievements(index);
        _anim.SetBool("achUn", true);
        PlayerPrefs.SetInt("ImpMark", index);

        if(delete)
        {
            DeleteAchievements();
        }
    }

    public void Turnoff()
    {
        AchievementObj.SetActive(false);
        _anim.SetBool("achUn", false);
    }

    public void SaveAchievements(int index)
    {
        string achievementKey = "Achievement_" + index.ToString();
        PlayerPrefs.SetInt(achievementKey, achIndex[index]);
        totalAch = 1 + PlayerPrefs.GetInt("totalAch");  
        PlayerPrefs.SetInt("totalAch", totalAch);
        PlayerPrefs.Save();
    }

    public void LoadAchievements()
    {
        for (int i = 1; i <= 25; i++)
        {
            string achievementKey = "Achievement_" + i.ToString();
            achIndex[i] = PlayerPrefs.GetInt(achievementKey);
        }
        band = PlayerPrefs.GetInt("band"); 
        totalAch = PlayerPrefs.GetInt("totalAch");     
    }

    public void DeleteAchievements()
    {
        for (int i = 1; i <= 25; i++)
        {
            string achievementKey = "Achievement_" + i.ToString();
            PlayerPrefs.DeleteKey(achievementKey);
            LockAchievements(i);
        }
        AchievementObj.SetActive(true);
        Title.text = "Achievement Deleted";
        Subj.text = "All Deleted!";
    }

    public void LockAchievements(int index)
    {
        string achievementKey = "Achievement_" + index.ToString();
        PlayerPrefs.SetInt(achievementKey, 0);
        FindObjectOfType<AchievementManager>().LockAchievement(index);
        totalAch = 0;
        PlayerPrefs.SetInt("totalAch", totalAch);
        PlayerPrefs.Save();
    }

    public void Notify(string _Title, string _Subject)
    {
        AchievementObj.SetActive(true);
        _anim.SetBool("achUn", true);
        Title.text = _Title;
        Subj.text = _Subject;
    }
}
