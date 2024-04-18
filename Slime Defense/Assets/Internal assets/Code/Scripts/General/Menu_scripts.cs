using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_scripts : MonoBehaviour
{
    public static Menu_scripts Instance; 
    [SerializeField] private GameObject panel_pause;
    [SerializeField] private GameObject panel_game_over;
    [SerializeField] private GameObject Bt_result;

    public void Awake(){
        Time.timeScale = 1;
        if (Instance == null)
            Instance = this;
    }

    public void OnPauseClick(){
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        panel_pause.SetActive(!panel_pause.activeSelf);
    }

    public void ExitToMainMenu(){
        SceneManager.LoadScene("Main_menu");
    }


    public void NextLevel(){
        string name_current_level = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetString("loading_scene", name_current_level); //пока что так
        SceneManager.LoadScene("Loader_Scene");
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver(bool isWinner = false){
        Time.timeScale = 0;
        if (!isWinner)  //если мы не победили, нам понадобится изменить панель game_over, которая по умолчанию настроенна на победу
        {
            panel_game_over.GetComponent<Image>().color = Color.red;
            panel_game_over.GetComponentsInChildren<Text>()[0].text = "You Lose";
            Bt_result.GetComponent<Button>().onClick.RemoveAllListeners();
            Bt_result.GetComponent<Button>().onClick.AddListener(RestartLevel);
            Bt_result.GetComponentInChildren<Text>().text = "Restart";
        }

        panel_game_over.SetActive(true);
    }
}
