using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject speed_container;
    [SerializeField] private GameObject speed_slider;


    public void Awake(){
        speed_slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Speed of scrolling", 1f);
        speed_container.GetComponent<Text>().text = PlayerPrefs.GetFloat("Speed of scrolling", 1f).ToString();
    }

    public void OnSaveSettings(){
        PlayerPrefs.SetFloat("Speed of scrolling", speed_slider.GetComponent<Slider>().value);
    }

    public void OnSpeedChange(){
        speed_container.GetComponent<Text>().text = speed_slider.GetComponent<Slider>().value.ToString();
    }
}
