using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Сastle : Music, I_Abstract_character
{
    [Header ("Specifications")]
    [SerializeField] private float hp;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider health_bar;
    [SerializeField] private bool is_enemy_castle;

    void Awake(){
        health_bar.maxValue = hp;
        health_bar.value = hp;
    }


    public void TakeDamage(float damage,  Utilities.Elements[] elements = null){
        hp -= damage;
        PlaySound(sounds[0], destroyed: true);
        if (hp <= 0){
            Deth_Skill();
        } else {
            health_bar.value = hp;
        }
    }


    private void Deth_Skill()
    {
        Menu_scripts.Instance.GameOver(is_enemy_castle);
    }
}
