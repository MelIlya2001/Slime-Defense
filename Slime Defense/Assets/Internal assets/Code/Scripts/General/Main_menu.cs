using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : Music
{


    [SerializeField] private uint count_levels;
    [SerializeField] private GameObject prefab_Bt_level;
    [SerializeField] private GameObject container_levels;


    [Space]
    [SerializeField] private GameObject count_gem_container;

    //private  int available_level;
    
    

    public void Awake(){

       // available_level = PlayerPrefs.GetInt("Available level", 1);

       /* if(count_levels > 0 && prefab_Bt_level != null && container_levels != null){
            for (int i = 1; i < count_levels + 1; i++){
                GameObject level = Instantiate(prefab_Bt_level, container_levels.transform);
                
                

                if (i <= available_level) {
                    level.GetComponentInChildren<Text>().text =  i.ToString();
                    level.GetComponent<Button>().onClick.AddListener( () => OnLevelClick("Level_" + level.GetComponentInChildren<Text>().text));
                    level.GetComponent<Image>().sprite = default_level_sprite;
                    level.GetComponent<Image>().color = Color.white;
                    
                } else {
                    level.GetComponentInChildren<Text>().text =  LEVEL_COST.ToString();
                    level.GetComponentInChildren<Text>().color = Color.red;
                    level.GetComponent<Button>().onClick.AddListener(Payment_level);
                    level.GetComponent<Image>().sprite = closed_level_sprite;
                    level.GetComponent<Image>().color = Color.gray;

                    if (i != available_level + 1) 
                        level.GetComponent<Button>().interactable = false;
                }
            }
        } */

        
    }



    /*public void OnLevelClick(string scene_name){
        PlaySound(sounds[0], destroyed:true);
        
        PlayerPrefs.SetString("loading_scene", scene_name);
        SceneManager.LoadScene("Loader_Scene");
    }*/

    public void ExitFromGame(){
        PlaySound(sounds[0], destroyed:true);
        //сохранить прогресс и выйти из игры
        Application.Quit();
    }

}
