using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Stars : MonoBehaviour
{
    private const int LEVEL_COST = 2;
    
    [SerializeField] private GameObject error_panel;


    private TextMeshProUGUI stars;

    void OnEnable() {
        Bt_level.onPayed += SpendStar;
    }

    void OnDisable(){
        Bt_level.onPayed -= SpendStar;
    }


    void Awake()
    {
        stars = gameObject.GetComponent<TextMeshProUGUI>();
        stars.text = PlayerPrefs.GetInt("avaible stars", 0).ToString();
    }


    private bool SpendStar(){
        if (Compare_costs()){
            error_panel.SetActive(true);
            return false;
        }

        int ramainig_stars = GetCurrentStars() - LEVEL_COST;
        PlayerPrefs.SetInt("avaible stars", ramainig_stars);
        stars.text = ramainig_stars.ToString(); 
        return true;
    }

    private bool Compare_costs (){
        return LEVEL_COST > GetCurrentStars();
    }

    private int GetCurrentStars(){
        return int.Parse(stars.text);
    }
}
