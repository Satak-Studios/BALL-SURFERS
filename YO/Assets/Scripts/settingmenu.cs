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

    public GameObject Score;

    public Toggle FPST;
    public Toggle LFPS;
    public Toggle fsn;
    //public Toggle sRun;

    public Dropdown DMode;
    public Dropdown Qindex;

    int res;
    int qIndex;
    int FPS;
    int lFPS;
    int fScreen;
    bool isFScreen;
    int Dval;
    //int Srun;

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

        //RIP In-built SpeedRun Timer
        /*if (PlayerPrefs.GetInt("sdrn") == 1)
        {
            sRun.isOn = true;
        }
        if (PlayerPrefs.GetInt("sdrn") == 0)
        {
            sRun.isOn = false;
        }*/
        
        //Getting Value
        bolume = PlayerPrefs.GetFloat("volume");
        res = PlayerPrefs.GetInt("index");
        //SetResolution(res);
        Qindex.value = PlayerPrefs.GetInt("qIndex");
        SetQuality(Qindex.value);
        FPS = PlayerPrefs.GetInt("fps");
        fScreen = PlayerPrefs.GetInt("fScreen");
        lFPS = PlayerPrefs.GetInt("lfps");
        DMode.value = PlayerPrefs.GetInt("dmode");
        //Srun = PlayerPrefs.GetInt("sdrn");
        //FPS
        if (FPS == 1)
        {
            FPST.isOn = true;
        }
        else
        {
            FPST.isOn = false;
        } 
        
        if (lFPS == 1)
        {
            LFPS.isOn = true;
        }
        else
        {
            LFPS.isOn = false;
        }   
        
        if (Screen.fullScreen)
        {
            fsn.isOn = true;
            fScreen = 1;
        }
        else
        {
            fsn.isOn = false;
            fScreen = 0;
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
        if (reslutionDropdown == null)
        {
            Debug.LogError("reslutionDropdown is not assigned in the Inspector.");
            return;
        }

        if (resolutions == null || resolutions.Length == 0 || resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            //Debug.LogError("Invalid resolution index or resolutions array is null/empty.");
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        isFScreen = (fScreen == 1);

        Screen.SetResolution(resolution.width, resolution.height, isFScreen);
        res = resolutionIndex;
        PlayerPrefs.SetInt("index", resolutionIndex);
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
        PlayerPrefs.SetInt("qIndex", Qindex.value);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        //Screen.fullScreen = isFullscreen;
        if (isFullscreen)
        {
            fScreen = 1;
            Screen.fullScreen = true;
        }
        else
        {
            fScreen = 0;
            Screen.fullScreen = false;
        }
        PlayerPrefs.SetInt("fScreen", fScreen);
    }
    
    public void SetFPS(bool isFPS_ON)
    {
        if (isFPS_ON == true)
        {
            FPS = 1;
            PlayerPrefs.SetInt("fps", FPS);
        }
        else
        {
            FPS = 0;
            PlayerPrefs.SetInt("fps", FPS);
        }
    }

    public void LockFPS(bool isLocked)
    {
        if (isLocked)
        {
            lFPS = 1;
            PlayerPrefs.SetInt("lfps", lFPS);
        }
        else
        {
            lFPS = 0;
            PlayerPrefs.SetInt("lfps", lFPS);
        }
    }

    //Set RIP Speedrun
    /*public void SetSpeedRun()
    {
        if (sRun.isOn == true)
        {
            Srun = 1;
            PlayerPrefs.SetInt("sdrn", Srun);
        }
        if (sRun.isOn == false)
        {
            Srun = 0;
            PlayerPrefs.SetInt("sdrn", Srun);
            PlayerPrefs.DeleteKey("cTimer");
        }
    }*/

    private void Update()
    {
        //FPS
        if (FPS == 1)
        {
            FPST.isOn = true;
        }
        else
        {
            FPST.isOn = false;
        }

        //RIP SpeedRun
        //PlayerPrefs.SetInt("sdrn", Srun);
    }

    public void SetDMode()
    {
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

    //Saving
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("volume", bolume);
        PlayerPrefs.SetInt("index", res);
        PlayerPrefs.SetInt("qIndex", qIndex);
        PlayerPrefs.SetInt("fps", FPS);
        PlayerPrefs.SetInt("fScreen", fScreen);
        PlayerPrefs.SetInt("dmode", DMode.value);
        //PlayerPrefs.SetInt("sdrn", Srun);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        PauseMenu.isOptions = false;
    }
}
