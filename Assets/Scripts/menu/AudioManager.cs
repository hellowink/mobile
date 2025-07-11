using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioMixer audioMixer;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private AudioClip currentClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayMenuMusic()
    {
        if (currentClip == menuMusic) return;

        musicSource.clip = menuMusic;
        musicSource.loop = true;
        currentClip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        if (currentClip == gameMusic) return;

        musicSource.clip = gameMusic;
        musicSource.loop = true;
        currentClip = gameMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentClip = null;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuInicial")
        {
            PlayMenuMusic();
        }
        else if (scene.name.StartsWith("levelOne"))
        {
            PlayGameMusic();
        }
    }
}
