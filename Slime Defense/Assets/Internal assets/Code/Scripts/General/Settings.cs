using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


//отрефакторить?
public class Settings : Music
{
    [SerializeField] private GameObject speed_container;
    [SerializeField] private GameObject speed_slider;

    [SerializeField] private GameObject music_container;
    [SerializeField] private GameObject music_slider;
    [SerializeField] private AudioMixer musicMixer;


    [SerializeField] private GameObject sound_container;
    [SerializeField] private GameObject sound_slider;
    [SerializeField] private AudioMixer soundMixer;



    public void Awake(){
        soundMixer.SetFloat("General_sound_volume", PlayerPrefs.GetFloat("Volume of sound", 0f));
        Debug.Log(PlayerPrefs.GetFloat("Volume of sound", 0f));
        musicMixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f));
    }

    public void OnSettingsClick(){
        PlaySound(sounds[0]);
        float value = PlayerPrefs.GetFloat("Speed of scrolling", 1f);
        speed_slider.GetComponent<Slider>().value = value;
        speed_container.GetComponent<Text>().text = value.ToString();

    
        value = PlayerPrefs.GetFloat("Volume of music", 0f);
        music_slider.GetComponent<Slider>().value = value;
        music_container.GetComponent<Text>().text = value.ToString();

        value = PlayerPrefs.GetFloat("Volume of sound", 0f);
        sound_slider.GetComponent<Slider>().value = value;
        sound_container.GetComponent<Text>().text = value.ToString();
    }

    public void OnSaveSettings(){
        PlaySound(sounds[0]);
        PlayerPrefs.SetFloat("Speed of scrolling", speed_slider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Volume of music", music_slider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Volume of sound", sound_slider.GetComponent<Slider>().value);
    }

    public void OnSpeedChange(){
        speed_container.GetComponent<Text>().text = speed_slider.GetComponent<Slider>().value.ToString();
    }

    public void OnVolumeMusicChange(){
        float volume = music_slider.GetComponent<Slider>().value;
        music_container.GetComponent<Text>().text = volume.ToString();
        musicMixer.SetFloat("General_volume", volume);
    }



    public void OnVolumeSoundChange(){
        float volume = sound_slider.GetComponent<Slider>().value;
        sound_container.GetComponent<Text>().text = volume.ToString();
        soundMixer.SetFloat("General_sound_volume", volume);
    }

    public void OnBackSettings(){
        PlaySound(sounds[0]);

        soundMixer.SetFloat("General_sound_volume", PlayerPrefs.GetFloat("Volume of sound", 0f));
        musicMixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f));
    }
}
