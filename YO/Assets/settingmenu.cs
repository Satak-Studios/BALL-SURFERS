using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;


public class settingmenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown reslutionDropdown;

    Resolution[] resolutions;

    public string path = "options";
    float bolume;
    bool _volume;

    //public PauseMenu pm = null;

    public Dropdown lang;
    //public Language lg;
    int val;
    public GameObject Score;
    public Hindi h;
    public Dropdown language;

    public Toggle FPST;
    public Toggle fsn;
    public Toggle sRun;

    public Dropdown DMode;
    public Dropdown Qindex;

    int res;
    int qIndex;
    int FPS;
    int fScreen;
    int Dval;
    int Srun;

    private void Start()
    {
        //Setting Values
        //FPS
        if (FPS == 1)
        {
            FPST.isOn = true;
        }
        else
        {
            FPST.isOn = false;
        }

        //SpeedRun
        if (PlayerPrefs.GetInt("sdrn") == 1)
        {
            sRun.isOn = true;
        }
        if (PlayerPrefs.GetInt("sdrn") == 0)
        {
            sRun.isOn = false;
        }

        //Language Selection
        val = lang.value;

        // Full Screen
        if (Screen.fullScreen == true)
        {
            fsn.isOn = true;
        }
        else
        {
            fsn.isOn = false;
        }

        //Getting Value
        //FPS = PlayerPrefs.GetInt("fps");
        //qIndex = PlayerPrefs.GetInt("fps");

        val = PlayerPrefs.GetInt("lang");
        bolume = PlayerPrefs.GetFloat("volume");
        res = PlayerPrefs.GetInt("index");
        /*qIndex*/ Qindex.value = PlayerPrefs.GetInt("qIndex");
        FPS = PlayerPrefs.GetInt("fps");
        fScreen = PlayerPrefs.GetInt("fScreen");
        DMode.value = PlayerPrefs.GetInt("dmode");
        Srun = PlayerPrefs.GetInt("sdrn");

        //FPS
        if (FPS == 1)
        {
            FPST.isOn = true;
        }
        else
        {
            FPST.isOn = false;
        }

        //Resoultion
        resolutions = Screen.resolutions;

        reslutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        reslutionDropdown.AddOptions(options);
        reslutionDropdown.value = currentResolutionIndex;
        reslutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
        resolutionIndex = PlayerPrefs.GetInt("index",resolutionIndex);
        res = resolutionIndex;
        PlayerPrefs.SetInt("index",resolutionIndex);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        volume = PlayerPrefs.GetFloat("volume", bolume);
        PlayerPrefs.SetFloat("volume", bolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qIndex = qualityIndex;
        qualityIndex = PlayerPrefs.GetInt("qIndex");
        PlayerPrefs.SetInt("qIndex", Qindex.value);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        /*if (fScreen == 0)
        {
            isFullscreen = false;
        }
        if (fScreen == 1)
        {
            isFullscreen = true;
        }*/
    }
    
    public void SetFPS(bool isFPS_ON)
    {
       // Debug.Log("FPS is " + isFPS_ON);
        if (isFPS_ON == true)
        {
            FPS = 1;
            Debug.Log("FPS is " + isFPS_ON + " " + FPS);
            PlayerPrefs.SetInt("fps", FPS);
        }
        else
        {
            FPS = 0;
            Debug.Log("FPS is " + isFPS_ON + " " + FPS);
            PlayerPrefs.SetInt("fps", FPS);
        }

        /*if (FPS == 1)
        {
            isFPS_ON = true;
        }
        if (FPS == 0)
        {
            isFPS_ON = false;
        }*/
    }

    //Set Speedrun
    public void SetSpeedRun()
    {
        // Debug.Log("FPS is " + isFPS_ON);
        if (sRun.isOn == true)
        {
            Srun = 1;
            Debug.Log("Srun " + sRun.isOn + " " + Srun);
            PlayerPrefs.SetInt("sdrn", Srun);
        }
        if (sRun.isOn == false)
        {
            Srun = 0;
            Debug.Log("SRUN is " + sRun.isOn + " " + Srun);
            PlayerPrefs.SetInt("sdrn", Srun);
            PlayerPrefs.DeleteKey("cTimer");
        }

        /*if (FPS == 1)
        {
            isFPS_ON = true;
        }
        if (FPS == 0)
        {
            isFPS_ON = false;
        }*/
    }

    private void Update()
    {
        //pm = FindObjectOfType<PauseMenu>();

        //FPS
        if (FPS == 1)
        {
            FPST.isOn = true;
        }
        else
        {
            FPST.isOn = false;
        }

        //SpeedRun
        PlayerPrefs.SetInt("sdrn", Srun);

        //Language Selection
        val = lang.value;

        // Full Screen
        /*if (Screen.fullScreen == true)
        {
            fsn.isOn = true;
        }
        else
        {
            fsn.isOn = false;
        }

        /*PlayerPrefs.SetInt("lang", val);
        PlayerPrefs.SetFloat("volume", bolume);
        PlayerPrefs.Save();*/
    }

    public void SetLang()
    {
        //Language.SetLang(languageIndex);
        //languageIndex = PlayerPrefs.GetInt("lang", languageIndex);
        // PlayerPrefs.SetInt("lang", languageIndex);
        //PlayerPrefs.Save();
        
        val = PlayerPrefs.GetInt("lang", val);
        val = language.value;


        if (val == 0)
        {
            h.LangEnglish();
            Debug.Log("ENGLISH");
        }

        if (val == 1)
        {
            h.LangHindi();
            Debug.Log("HINDI");
        }

        if (val == 2)
        {
            h.LangTelugu();
            Debug.Log("TELUGU");
        }
    }

    public void SetDMode()
    {
        Debug.Log("DMode = " + DMode.value);
        if (DMode.value == 0)
        {
            Normal();
        }

        if (DMode.value == 1)
        {
            Bot();
        }

        if (DMode.value == 2)
        {
            Noob();
        }

        if (DMode.value == 3)
        {
            ProGamer();
        }

        if (DMode.value == 4)
        {
            EpicGamer();
        }
    }

    /// <summary>
    /// All GameModes
    public void Normal()
    {
        float ff = 300;
        float sf = 35;
        PlayerPrefs.SetFloat("ff", ff);
        PlayerPrefs.SetFloat("sf", sf);

        PlayerPrefs.SetInt("dmode", DMode.value);
    }

    public void Bot()
    {
        float ff = 100;
        float sf = 15;
        PlayerPrefs.SetFloat("ff", ff);
        PlayerPrefs.SetFloat("sf", sf);

        PlayerPrefs.SetInt("dmode", DMode.value);
    }
    public void Noob()
    {
        float ff = 200;
        float sf = 25;
        PlayerPrefs.SetFloat("ff", ff);
        PlayerPrefs.SetFloat("sf", sf);

        PlayerPrefs.SetInt("dmode", DMode.value);
    }
    public void ProGamer()
    {
        float ff = 400;
        float sf = 45;
        PlayerPrefs.SetFloat("ff", ff);
        PlayerPrefs.SetFloat("sf", sf);

        PlayerPrefs.SetInt("dmode", DMode.value);
    }
    public void EpicGamer()
    {
        float ff = 600;
        float sf = 65;
        PlayerPrefs.SetFloat("ff", ff);
        PlayerPrefs.SetFloat("sf", sf);

        PlayerPrefs.SetInt("dmode", DMode.value);
    }
    /// </summary>
    // END

    //Saving
    public void SaveSettings()
    {
        //PlayerPrefs.SetInt("index", resolutionIndex);
        PlayerPrefs.SetInt("lang", val);
        PlayerPrefs.SetFloat("volume", bolume);
        PlayerPrefs.SetInt("index", res);
        PlayerPrefs.SetInt("qIndex", qIndex);
        PlayerPrefs.SetInt("fps", FPS);
        PlayerPrefs.SetInt("fScreen", fScreen);
        PlayerPrefs.SetInt("fScreen", fScreen);
        PlayerPrefs.SetInt("dmode", DMode.value);
        PlayerPrefs.SetInt("sdrn", Srun);
        PlayerPrefs.Save();
    }

    public void BenchMark()
    {
        SceneManager.LoadScene("Benchmark");
    }

    public void Back()
    {
        PauseMenu.isOptions = false;

    }
}
