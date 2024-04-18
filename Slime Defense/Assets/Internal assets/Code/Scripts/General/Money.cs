using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Money : MonoBehaviour
{   
    [SerializeField] GameObject money_prefab;
    [SerializeField] private float time_respawn_money;
    [SerializeField] private int additional_money;

    private TextMeshProUGUI money;
    public static Action onchanged;

    void OnEnable() {
        Abstract_minion.onDied += AddMoney;
        Payment_slimes.onPayed += SpendMOney;
        Payment_slimes.onHavedMoney += Compare_costs;
    }

    void OnDisable(){
        Abstract_minion.onDied -= AddMoney;
        Payment_slimes.onPayed -= SpendMOney;
        Payment_slimes.onHavedMoney -= Compare_costs;
    }


    void Start()
    {
        money = money_prefab.GetComponent<TextMeshProUGUI>();
        StartCoroutine(Respawsn_money());
    }

    

    IEnumerator Respawsn_money ()
    {
        while (true)
        {
            yield return new WaitForSeconds(time_respawn_money);  
            AddMoney(additional_money);
        }   
    }

    private void AddMoney (int new_money){
        money.text = (GetCurrentMoney() + new_money).ToString();
        onchanged?.Invoke();
    }

    private void SpendMOney (int mon){
        money.text = (GetCurrentMoney() - mon).ToString(); 
    }

    private bool Compare_costs (int cost){
        return cost > GetCurrentMoney();
    }

    private int GetCurrentMoney(){
        return int.Parse(money.text);
    }
}
