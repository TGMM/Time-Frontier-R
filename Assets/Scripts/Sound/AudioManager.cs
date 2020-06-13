using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
        }
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        PlaySceneSong();
    }

    private void Start()
    {
        PlaySceneSong();
    }

    private void PlaySceneSong()
    {
        StopAll();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                Play("menu");
                break;
            case 1:
                Play("easter");
                break;
            case 2:
                Play("Theme");
                break;
        }
    }

    public void Play(string name)
    {
        Sound s= Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file : " + name + "not found");
            return;
        }
        s.source.Play();
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file : " + name + "not found");
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        Stop("menu");
        Stop("easter");
        Stop("Theme");
    }


}
