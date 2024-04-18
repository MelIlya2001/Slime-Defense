using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Money : MonoBehaviour
{   
    [SerializeField] GameObject money_prefab;
    [SerializeField] private float time_respawn_money;
    [SerializeField] private int additional_money;

    private TextMeshProUGUI money;

    void OnEnable() {
        Abstract_minion.onDied += AddMoney;
        Payment_slimes.onPayed += SpendMOney;
    }

    void OnDisable(){
        Abstract_minion.onDied -= AddMoney;
        Payment_slimes.onPayed -= SpendMOney;
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
            money.text = (int.Parse(money.text) + additional_money).ToString(); 
        }   
    }

    private void AddMoney (int new_money){
        money.text = (int.Parse(money.text) + new_money).ToString();

    }

    private void SpendMOney (int mon){
        money.text = (int.Parse(money.text) - mon).ToString(); 
    }


}
