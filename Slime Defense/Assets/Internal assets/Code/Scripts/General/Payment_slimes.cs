using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using SlimeType = PoolControl.SlimeType;
using TMPro;

public class Payment_slimes : MonoBehaviour
{
    private Dictionary<SlimeType, int> costs_slimes = new Dictionary<SlimeType, int>{
        [SlimeType.None] = 0,
        [SlimeType.air] = 15,
        [SlimeType.terra] = 10,
        [SlimeType.water] = 15,
        [SlimeType.fire] = 20,
        [SlimeType.fire_air] = 30,
        [SlimeType.fire_fire] = 30,
        [SlimeType.fire_terra] = 30,
        [SlimeType.fire_water] = 25,
        [SlimeType.air_air] = 25,
        [SlimeType.air_water] = 30,
        [SlimeType.air_terra] = 25,
        [SlimeType.terra_terra] = 25,
        [SlimeType.terra_water] = 30,
        [SlimeType.water_water] = 20
    };


    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject result_cell;
    [Space]

    [SerializeField] private float posY;
    [SerializeField] private Vector3 castle_position;

    [Space]

    [SerializeField] private GameObject Bt_component_first;
    [SerializeField] private GameObject Bt_component_second;
    private SlimeType component_first = SlimeType.None;
    private SlimeType component_second = SlimeType.None;


    public static Action<int> onPayed;
    private int slime_cost;


    public void OnShopClick(){
        shop.SetActive(!shop.activeSelf);
    }

    public void ConfirmPayment(){
        if (component_first is SlimeType.None && component_second is SlimeType.None) return;

        
        GameObject slime = PoolControl.Instance.GetObject(component_first, PoolControl.Instance.slime_pools);
        if (slime is null) return;
        slime.transform.position = new Vector3(castle_position.x, posY, castle_position.z);
        onPayed?.Invoke(slime_cost);
       
    }

    public void OnFirstComponentClick(){
        component_first = SlimeType.None;
        Bt_component_first.GetComponentInChildren<Text>().text = component_first.ToString();
    }

    public void OnSecondComponentClick(){
        component_second = SlimeType.None;
        Bt_component_second.GetComponentInChildren<Text>().text = component_second.ToString();
    }

    public void OnFireClick(){
        AddComponent(SlimeType.fire, costs_slimes[SlimeType.fire]);
    }

    public void OnWaterClick(){
        AddComponent(SlimeType.water, costs_slimes[SlimeType.water]);
    }


    public void OnTerraClick(){
        AddComponent(SlimeType.terra, costs_slimes[SlimeType.terra]);
    }

    public void OnAirClick(){
        AddComponent(SlimeType.air, costs_slimes[SlimeType.air]);
    }

    private void AddComponent(SlimeType type = SlimeType.None, int cost = 0){
        if (component_first is SlimeType.None){
            component_first = type;
            Bt_component_first.GetComponentInChildren<Text>().text = type.ToString();
            return;
        }
        if (component_second is SlimeType.None && Bt_component_second.GetComponent<Button>().interactable){
            component_second = type;
            Bt_component_second.GetComponentInChildren<Text>().text = type.ToString();
            return;
        }
    }

    public void ExitShop(){
        OnShopClick();
        OnFirstComponentClick();
        OnSecondComponentClick();
    }
}
