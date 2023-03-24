using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public Hindi h;
    public string english;
    static int currentLang;
    Hindi _hindi;
    public string _tel = "TELUGU";
    public string _eng = "ENGLISH";
    public  Dropdown langDrop;
  //  public GameObject[] flags;

    // Start is called before the first frame update
    void Start()
    {
        //  lang.ClearOptions();

        // List<Dropdown.OptionData> flagItem = new List<Dropdown.OptionData>();

        // foreach(var flag in flags)
        // {
        //      string flagName = flag.name;
        //      int dot = flag.name.IndexOf('.');
        //     if (dot >= 0)
        //     {
        //          flagName = flagName.Substring(dot + 1);
        //     }

        //    var flagOption = new Dropdown.OptionData(flagName);
        //     flagItem.Add(flagOption);
        // }
        // lang.AddOptions(flagItem);

       // langDrop.onValueChanged.AddListener(delegate
      //  {
          //  bla(lang);
       // });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLang()//int languageInde)
    {
        // currentLang = PlayerPrefs.GetInt("lang", languageIndex);
        if (langDrop.value == 0)
        {
            Debug.Log("LANGUAGE SELECTED TO ENGLISH");
            h.LangEnglish();
        }

        if (langDrop.value == 1)
        {
            Debug.Log("LANGUAGE SELECTED TO HIDI");
            h.LangHindi();
        }

        if (langDrop.value == 2)
        {
            Debug.Log("LANGUAGE SELECTED TO TELUGU");
            h.LangTelugu();
        }
    }
}
