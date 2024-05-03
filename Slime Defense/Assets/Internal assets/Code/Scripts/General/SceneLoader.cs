using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Music
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private Text loading_text;
    [SerializeField] private Image progress_bar_image;
    [SerializeField] private GameObject progress_bar;
    [SerializeField] private GameObject press_key_hit;


    private AsyncOperation async_operation;

    private string next_scene_name;

    
    void Awake(){
        Instance = this;
        Time.timeScale = 1;
    }

    void Start(){
        StartCoroutine("Async_load", PlayerPrefs.GetString("loading_scene", next_scene_name));
    }


    IEnumerator Async_load(string scene_name)
    {
        float loading_progress;
        async_operation = SceneManager.LoadSceneAsync(scene_name);

        async_operation.allowSceneActivation = false;   //Чтобы сцена не загружалась сразу после подггрузки всех файлов
        while (async_operation.progress < 0.9f) //при 1 уже загружается новая сцена
        {
            loading_progress = Mathf.Clamp01(async_operation.progress / 0.9f);  //async_operation.progress идёт от 0 до 9 - загрузка ассетов, от 0.9 до 1 - активация сцены
            loading_text.text = $"Загрузка ... {(loading_progress * 100).ToString("0")}%";
            progress_bar_image.fillAmount = loading_progress;
            yield return true;
        }

        progress_bar.SetActive(false);
        press_key_hit.SetActive(true);

    }


    void Update(){
        if (press_key_hit.activeSelf)
        {
            if (Input.anyKeyDown) {
                PlaySound(sounds[0]);
                async_operation.allowSceneActivation = true;
            }
        }
    }


}
