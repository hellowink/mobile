using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider ambientSlider;

    private void Start()
    {
        // Cargar vol�menes guardados o setear por defecto
        musicSlider.value = PlayerPrefs.GetFloat("Music", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
        ambientSlider.value = PlayerPrefs.GetFloat("Ambient", 0.75f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetAmbientVolume(ambientSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat("Ambient", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Ambient", volume);
    }
}
