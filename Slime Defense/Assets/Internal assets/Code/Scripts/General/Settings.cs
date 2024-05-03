using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Settings : Music
{
    [SerializeField] private GameObject speed_container;
    [SerializeField] private GameObject speed_slider;




    public void Awake(){
        speed_slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Speed of scrolling", 1f);
        speed_container.GetComponent<Text>().text = PlayerPrefs.GetFloat("Speed of scrolling", 1f).ToString();
    }

    public void OnSaveSettings(){
        PlaySound(sounds[0]);
        PlayerPrefs.SetFloat("Speed of scrolling", speed_slider.GetComponent<Slider>().value);
    }

    public void OnSpeedChange(){
        speed_container.GetComponent<Text>().text = speed_slider.GetComponent<Slider>().value.ToString();
    }
}
