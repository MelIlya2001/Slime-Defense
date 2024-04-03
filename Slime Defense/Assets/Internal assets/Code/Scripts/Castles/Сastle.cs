using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Сastle : MonoBehaviour, I_Abstract_character
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


    public void TakeDamage(float damage){
        hp -= damage;
        if (hp <= 0){
            Deth_Skill();
        } else {
            health_bar.value = hp;
        }
    }


    public void TakeDamage(float damage, string[] elements){
        TakeDamage(damage);
    }

    private void Deth_Skill()
    {
        Menu_scripts.Instance.GameOver(is_enemy_castle);
    }
}
