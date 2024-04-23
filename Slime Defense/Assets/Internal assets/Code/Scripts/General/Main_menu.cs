using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour
{
    [SerializeField] private uint count_levels;
    [SerializeField] private GameObject prefab_Bt_level;
    [SerializeField] private GameObject container_levels;

    public void Awake(){

        int available_level = PlayerPrefs.GetInt("Available level", 1);


        if(count_levels > 0 && prefab_Bt_level != null && container_levels != null){
            for (int i = 1; i < count_levels + 1; i++){
                GameObject level = Instantiate(prefab_Bt_level, container_levels.transform);
                level.GetComponentInChildren<Text>().text =  i.ToString();
                level.GetComponent<Button>().onClick.AddListener( () => OnLevelClick("Level_" + level.GetComponentInChildren<Text>().text));
                
                if (i > available_level){
                    level.GetComponent<Button>().interactable = false;
                }
            }
        }

        
    }

    public void OnLevelClick(string scene_name){
        Debug.Log(""+scene_name);
        PlayerPrefs.SetString("loading_scene", scene_name);
        SceneManager.LoadScene("Loader_Scene");
    }

    public void ExitFromGame(){
        //сохранить прогресс и выйти из игры
        Application.Quit();
    }

}
