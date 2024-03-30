using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_scripts : MonoBehaviour
{
    [SerializeField] private GameObject panel_pause;

    public void Awake(){
        Time.timeScale = 1;
    }

    public void OnPauseClick(){
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        panel_pause.SetActive(!panel_pause.activeSelf);
    }

    public void ExitToMainMenu(){
        SceneManager.LoadScene("Main_menu");
    }
}
