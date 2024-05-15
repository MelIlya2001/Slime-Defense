using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu_scripts : Music
{
    public static Menu_scripts Instance; 
    [SerializeField] private GameObject panel_pause;
    [SerializeField] private GameObject panel_game_over;
    [SerializeField] private GameObject panel_game_over_lose;
    [SerializeField] private GameObject Bt_result;

    [Space]
    [SerializeField] private GameObject star_def_castle_hp;
    [SerializeField] private GameObject star_def_slimes_count;
    [SerializeField] private Sprite filled_star;


    public static Func<bool> onEndGameSlimesResult;
    public static Func<bool> onEndGameHpCastleResult;

    private string current_level;
    private int current_level_index ;

    public void Awake(){
        star_def_castle_hp.GetComponentInChildren<TextMeshProUGUI>().text = "Оставшиеся hp башни > " + Utilities.Instance.GetRemainigHpForStar();
        star_def_slimes_count.GetComponentInChildren<TextMeshProUGUI>().text = "Вызвано менее " + Utilities.Instance.GetCountSlimesForStar() + " слаймов"; 


        Time.timeScale = 1;
        if (Instance == null)
            Instance = this;
        
        current_level = SceneManager.GetActiveScene().name;
        current_level_index = int.Parse(current_level.Split('_')[1]);             
    }

    public void OnPauseClick(){

        PlaySound(sounds[2]);

        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (Time.timeScale == 1){
            mixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f));
        } else
        {
            mixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f) - 10f);
        }

        panel_pause.SetActive(!panel_pause.activeSelf);
    }

    public void ExitToMainMenu(){
        PlaySound(sounds[2], destroyed: true);
        mixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f));
        SceneManager.LoadScene("Main_menu");
    }


    public void NextLevel(){
        PlaySound(sounds[2]);
        mixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f));


        string name_next_level = "Level_" + (current_level_index + 1).ToString();

        PlayerPrefs.SetString("loading_scene", name_next_level); 
        SceneManager.LoadScene("Loader_Scene");
    }

    public void RestartLevel(){
        PlaySound(sounds[2]);
        mixer.SetFloat("General_volume", 0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    

    public void GameOver(bool isWinner = false){
        mixer.SetFloat("General_volume", PlayerPrefs.GetFloat("Volume of music", 0f) -10f);

        Time.timeScale = 0;
        if (!isWinner)
        {
            /*panel_game_over.GetComponent<Image>().color = Color.red;
            panel_game_over.GetComponentsInChildren<Text>()[0].text = "You Lose";
            Bt_result.GetComponent<Button>().onClick.RemoveAllListeners();
            Bt_result.GetComponent<Button>().onClick.AddListener(RestartLevel);
            Bt_result.GetComponentInChildren<Text>().text = "Restart";*/
            panel_game_over_lose.SetActive(true);

            PlaySound(sounds[0]);
        } else {
            panel_game_over.SetActive(true);
            PlaySound(sounds[1]);

            /*if (current_level_index >= PlayerPrefs.GetInt("Available level", 1)){
                PlayerPrefs.SetInt("Available level", current_level_index + 1);
            }*/

            //+2 объекта-текста, в которые прописываются условия. Можно в Awake пихнуть
            //

            int stars = 1;

            if (onEndGameHpCastleResult.Invoke()){
                stars++;
                star_def_castle_hp.GetComponentInChildren<Image>().sprite = filled_star;
            }
                //добавляем звезду
            
            if (onEndGameSlimesResult.Invoke()) {
                stars++; 
                star_def_slimes_count.GetComponentInChildren<Image>().sprite = filled_star;
            }
                //добавляем звезду

            
            int diff_stars = stars - PlayerPrefs.GetInt("Stars of level " + current_level_index.ToString(), 0);

            if (diff_stars > 0){
                int avaible_stars = PlayerPrefs.GetInt("avaible stars", 0);
                PlayerPrefs.SetInt("avaible stars", avaible_stars + diff_stars);
                PlayerPrefs.SetInt("Stars of level " + current_level_index.ToString(), stars);
            }
            
        }

    }
}
