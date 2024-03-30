using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void OnLevelClick(string scene_name){
        PlayerPrefs.SetString("loading_scene", scene_name);
        SceneManager.LoadScene("Loader_Scene");
    }
}
