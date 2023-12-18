using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    public GameObject cheatMenu;
    public GameObject prePanel;
    public GameObject mainPanel;

    public InputField cheatField;

    [SerializeField]
    private string passwordKey;

    //MainCheats
    public InputField levelsCompleted;
    public InputField hScore;
    public Dropdown achDropdown;

    // Start is called before the first frame update
    void Start()
    {
        cheatMenu.SetActive(false);
        CheckOwner();

        levelsCompleted.text = PlayerPrefs.GetInt("levelsCompleted", 1).ToString();
        hScore.text = PlayerPrefs.GetFloat("hiScore", 1).ToString();
    }

    public void Check()
    {
        string cleanCheatString = cheatField.text.ToLower().Trim();

        if (cleanCheatString == passwordKey)     
        {
            OK();
        }
        else
        {
            NO();
        }
    }

    public void CheckOwner()
    {
        if(PlayerPrefs.HasKey("sk"))
        {
            prePanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            prePanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    void OK()
    {
        cheatMenu.SetActive(true);
        prePanel.SetActive(false);
        mainPanel.SetActive(true);
        PlayerPrefs.SetString("sk", "Sathvik");
    }

    public void NO()
    {
        cheatMenu.SetActive(false);
        prePanel.SetActive(true);
        mainPanel.SetActive(false);
        PlayerPrefs.DeleteKey("sk");
    }

    public void LevelsCompleted()
    {
        int newLevelsUnlocked = int.Parse(levelsCompleted.text);
        PlayerPrefs.SetInt("levelsUnlocked", newLevelsUnlocked);
        PlayerPrefs.Save();
    }

    public void HighScore()
    {
        int newHScore = int.Parse(hScore.text);
        PlayerPrefs.SetFloat("hiScore", newHScore);
        PlayerPrefs.Save();
    }

    public void AchCompleted()
    {
        int achIndex = achDropdown.value;
        if (achIndex <= 25)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(achIndex);
        }
        else if (achIndex == 26)
        {
            UnlockAllAch();
        }
        else if (achIndex == 27)
        {
            LockAllAch();
        }
    }
    void UnlockAllAch()
    {
        for (int i = 0; i <= 25; i++)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(i);
        }
        FindObjectOfType<Achiever>().totalAch = 25;
        PlayerPrefs.SetInt("totalAch", 25);
    }

    void LockAllAch()
    {
        FindObjectOfType<Achiever>().DeleteAchievements();
    }    
}
