using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bt_level : Music
{   
    [SerializeField] private GameObject NextLevel;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Sprite default_level, closed_level, filled_star;

    private Text text_level;
    public static Func<bool> onPayed;
    void Awake(){
        int available_level = PlayerPrefs.GetInt("Available level", 1);
        text_level = GetComponentInChildren<Text>();

        if (available_level >= int.Parse(text_level.text)){

            int count_star = PlayerPrefs.GetInt("Stars of level " + text_level.text, 0);
            filled_stars(count_star, filled_star);
            text_level.enabled = true;
            gameObject.GetComponent<Image>().sprite = default_level;
            gameObject.GetComponent<Image>().color = Color.white;
            gameObject.GetComponent<Button>().onClick.AddListener(() => OnLevelClick("Level_" + text_level.text));

        } else {

            text_level.enabled = false;
            gameObject.GetComponent<Image>().sprite = closed_level;
            gameObject.GetComponent<Image>().color = Color.gray;
            
            if (available_level + 1 == int.Parse(text_level.text)){
                gameObject.GetComponent<Image>().color = Color.white;
                gameObject.GetComponent<Button>().onClick.AddListener(Unlock_level);
            }

        }
        
    }
    

    public void filled_stars(int count_star, Sprite filled_star){
        for (int i = 0; i < count_star; i++){
            stars[i].GetComponent<Image>().sprite = filled_star;
            
        }
    }

    public void OnLevelClick(string scene_name){
        PlaySound(sounds[0], destroyed:true);
        
        PlayerPrefs.SetString("loading_scene", scene_name);
        SceneManager.LoadScene("Loader_Scene");
    }

    public void Unlock_level(){
        if (onPayed?.Invoke() == true){
            gameObject.GetComponent<Button>().onClick.RemoveListener(Unlock_level);

            text_level.enabled = true;
            gameObject.GetComponent<Image>().sprite = default_level;
            gameObject.GetComponent<Button>().onClick.AddListener(() => OnLevelClick("Level_" + text_level.text));
            PlayerPrefs.SetInt("Available level", int.Parse(text_level.text));

            if (NextLevel != null){
                NextLevel.GetComponent<Button>().onClick.AddListener(Unlock_level);
                NextLevel.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
