using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // public AudioClip MainTheme;
    public Sound[] sounds;

    //AudioSource s;
    // Start is called before the first frame update
    void Start()
    {
        //Hey I SAW YOU TRYING TO COMPLETE THIS LEVEL, THE HINT IS DONT JUDGE A BOOK BY ITS COVER
        Play("MainTheme");
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
