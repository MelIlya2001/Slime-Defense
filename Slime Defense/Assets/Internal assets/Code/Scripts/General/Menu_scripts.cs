using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_scripts : Music
{
    public static Menu_scripts Instance; 
    [SerializeField] private GameObject panel_pause;
    [SerializeField] private GameObject panel_game_over;
    [SerializeField] private GameObject panel_game_over_lose;
    [SerializeField] private GameObject Bt_result;

    private string current_level;
    private int current_level_index ;

    public void Awake(){
        Time.timeScale = 1;
        if (Instance == null)
            Instance = this;
        
        current_level = SceneManager.GetActiveScene().name;
        //current_level_index = int.Parse(current_level.Split('_')[1]);             вернуть потом на место
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

            if (current_level_index >= PlayerPrefs.GetInt("Available level", 1)){
            PlayerPrefs.SetInt("Available level", current_level_index + 1);
        }
        }

    }
}
