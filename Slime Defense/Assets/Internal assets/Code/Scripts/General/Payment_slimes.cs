using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SlimeType = PoolControl.SlimeType;

public class Payment_slimes : MonoBehaviour
{

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


    public void OnShopClick(){
        shop.SetActive(!shop.activeSelf);
    }

    public void ConfirmPayment(){
        if (component_first is SlimeType.None && component_second is SlimeType.None) return;

        
        GameObject slime = PoolControl.Instance.GetObject(component_first, PoolControl.Instance.slime_pools);
        if (slime is not null) slime.transform.position = new Vector3(castle_position.x, posY, castle_position.z);
        
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
        AddComponent(SlimeType.T1_fire);
    }

    public void OnWaterClick(){
        AddComponent(SlimeType.T1_water);
    }


    public void OnTerraClick(){
        AddComponent(SlimeType.T1_terra);
    }

    public void OnAirClick(){
        AddComponent(SlimeType.T1_air);
    }

    private void AddComponent(SlimeType type = SlimeType.None){
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
