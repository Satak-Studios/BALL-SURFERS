using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




[System.Serializable]
public class CheatCodeInstance
{
    public string code;
    public UnityEvent cheatEvent;
}

public class CheatCodeManager : MonoBehaviour
{
    [SerializeField]
    private bool playerTyping = false;
    [SerializeField]
    private string currentString = "";

    [SerializeField]
    private List<CheatCodeInstance> cheatCodeList = new List<CheatCodeInstance>();

    public GameManager gm;
    public Restart rs;

    void Start()
    {
        rs = FindObjectOfType<Restart>();
        gm = FindObjectOfType<GameManager>();
    }

    private bool CheckCheat(string _input)
    {
        foreach (CheatCodeInstance code in cheatCodeList)
        {
            if (_input == code.code)
            {
                code.cheatEvent?.Invoke();
                return true;
            }
        }
        return false;
    }


    //Update 
    void Update()
    {
        if (gm == null == false)
        {
            gm = FindObjectOfType<GameManager>();
        }

        if (rs == null == false)
        {
            rs = FindObjectOfType<Restart>();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerTyping)
                CheckCheat(currentString);

            playerTyping = !playerTyping;
        }

        if (playerTyping)
        {
            foreach(char c in Input.inputString)
            {
                if (c == '\b')//backspace or delete
                {
                    if (currentString.Length > 0)
                        currentString = currentString.Substring(0, currentString.Length - 1);
                }
                else if (c == '\n' || c == '\r') //enter or return 
                {
                    currentString = "";
                }
                else
                {
                    currentString += c;
                }
            }
        }
    }


    ///////////CHEATS CODESSSSSS 
    public void cheating()
    {
        FindObjectOfType<Achiever>().Notify("Cheat Activated", "Cheating?");
        if(FindObjectOfType<Achiever>().achIndex[3] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(3);
        }
    }
    public void levelcomplete()
    {
        gm.CompleteLevel();
        FindObjectOfType<Achiever>().Notify("Cheat Activated", "Level Complete");
        if(FindObjectOfType<Achiever>().achIndex[3] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(3);
        }
    }
    public void slowmo()
    {
        Time.timeScale = 0.5f;
        FindObjectOfType<Achiever>().Notify("Cheat Activated", "Slow Mo");
        if(FindObjectOfType<Achiever>().achIndex[3] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(3);
        }
    }
    public void GodMode()
    {
        rs.isGodMode = true;
        FindObjectOfType<Achiever>().Notify("Cheat Activated", "OP Mode");
        if(FindObjectOfType<Achiever>().achIndex[3] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(3);
        }
    }
    public void normal()
    {
        Time.timeScale = 1f;
        if(FindObjectOfType<Achiever>().achIndex[3] == 0)
        {
            FindObjectOfType<Achiever>().AchievementUnlocked(3);
        }
    }
    public void Pause()
    {
        PauseMenu.GameIsPaused = true;
    }

}