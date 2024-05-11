using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using SlimeType = PoolControl.SlimeType;
using TMPro;

public class Payment_slimes : Music
{
    

    #region combinations of slimes
    [Serializable]
    struct SlimeInfo{
        [SerializeField]private SlimeType type;

        [SerializeField]private Sprite sprite;
        [SerializeField] private List<SlimeType> components;
        [SerializeField]private int cost;

       /* public SlimeInfo(SlimeType type, int cost, List<SlimeType> components){
            this.type = type;
            this.cost = cost;
            this.components = components;
        }*/

        public List<SlimeType> GetComponents(){
            return components;
        }

        public SlimeType GetSlimeType(){
            return type;
        }

        public int GetCost(){
            return cost;
        }

        public Sprite GetSprite(){
            return sprite;
        }
    }

    [SerializeField] private List<SlimeInfo> slimeInfo;

    /*private List<SlimeInfo> slimeInfo = new List<SlimeInfo>{
        //T1
        new SlimeInfo(SlimeType.T1_air, 15,
                     new List<SlimeType>(){SlimeType.T1_air, SlimeType.None}),
        new SlimeInfo(SlimeType.T1_water, 15,
                     new List<SlimeType>(){SlimeType.T1_water, SlimeType.None}),
        new SlimeInfo(SlimeType.T1_terra, 10,
                     new List<SlimeType>(){SlimeType.T1_terra, SlimeType.None}),
        new SlimeInfo(SlimeType.T1_fire, 20,
                     new List<SlimeType>(){SlimeType.T1_fire, SlimeType.None}),

        //T2
        new SlimeInfo(SlimeType.T2_fire_fire, 40,
                     new List<SlimeType>(){SlimeType.T1_fire, SlimeType.T1_fire}),
        new SlimeInfo(SlimeType.T2_fire_air, 40,
                     new List<SlimeType>(){SlimeType.T1_air, SlimeType.T1_fire}),
        new SlimeInfo(SlimeType.T2_fire_water, 35,
                     new List<SlimeType>(){SlimeType.T1_water, SlimeType.T1_fire}),
        new SlimeInfo(SlimeType.T2_fire_terra, 35,
                     new List<SlimeType>(){SlimeType.T1_terra, SlimeType.T1_fire}),



        new SlimeInfo(SlimeType.T2_air_air, 30,
                     new List<SlimeType>(){SlimeType.T1_air, SlimeType.T1_air}),
        new SlimeInfo(SlimeType.T2_air_water, 35,
                     new List<SlimeType>(){SlimeType.T1_air, SlimeType.T1_water}),
        new SlimeInfo(SlimeType.T2_air_terra, 25,
                     new List<SlimeType>(){SlimeType.T1_air, SlimeType.T1_terra}),
        

        new SlimeInfo(SlimeType.T2_terra_terra, 25,
                     new List<SlimeType>(){SlimeType.T1_terra, SlimeType.T1_terra}),
        new SlimeInfo(SlimeType.T2_terra_water, 30,
                     new List<SlimeType>(){SlimeType.T1_terra, SlimeType.T1_water}),


        new SlimeInfo(SlimeType.T2_water_water, 25,
                     new List<SlimeType>(){SlimeType.T1_water, SlimeType.T1_water}),
    };*/
    #endregion


    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject Bt_result;
    [Space]

    [SerializeField] private float posY;

    [Space]

    [SerializeField] private GameObject Bt_component_first;
    [SerializeField] private GameObject Bt_component_second;
    private SlimeType component_first = SlimeType.None;
    private SlimeType component_second = SlimeType.None;


    /// не нравится. Переписать
   // [SerializeField] private Sprite closed_book;
   // [SerializeField] private Sprite opend_book;
    [SerializeField] private GameObject Bt_shop;
    ///






    public static Action<int> onPayed;
    public static Func<int, bool> onHavedMoney;
    private SlimeInfo current_slime;


    void OnEnable() {
        Money.onchanged += CheckMoney;
    }

    void OnDisable(){
        Money.onchanged -= CheckMoney;
    }

    public void OnShopClick(){
        shop.SetActive(!shop.activeSelf);
        Bt_shop.SetActive(!Bt_shop.activeSelf);
        //Bt_shop.GetComponent<Image>().sprite = Bt_shop.GetComponent<Image>().sprite == closed_book ? opend_book : closed_book;
        PlaySound(sounds[0], volume: 0.3f);
    }

    public void ConfirmPayment(){
        if (component_first is SlimeType.None && component_second is SlimeType.None) return;


        GameObject slime = PoolControl.Instance.GetObject(current_slime.GetSlimeType(), PoolControl.Instance.slime_pools);
        if (slime is null) return;
        
        float posZ = Utilities.Instance.GetRandomPosZ();
        
        slime.transform.position = new Vector3(Utilities.Instance.GetSlimeSpawnX(), posY, posZ);

        onPayed?.Invoke(current_slime.GetCost());
        PlaySound(sounds[0], volume: 0.3f);
        CheckMoney();
    }

    public void OnFirstComponentClick(){
        component_first = SlimeType.None;
        //Bt_component_first.GetComponentInChildren<Text>().text = component_first.ToString();
        Bt_component_first.GetComponent<Image>().sprite = null;
        UpdateResult();
    }

    public void OnSecondComponentClick(){
        component_second = SlimeType.None;
        //Bt_component_second.GetComponentInChildren<Text>().text = component_second.ToString();
        Bt_component_second.GetComponent<Image>().sprite = null;
        UpdateResult();
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
            //Bt_component_first.GetComponentInChildren<Text>().text = type.ToString();
            Bt_component_first.GetComponent<Image>().sprite = slimeInfo.Find(item => item.GetSlimeType() == component_first).GetSprite();
            UpdateResult();
            return;
        }
        if (component_second is SlimeType.None && Bt_component_second.GetComponent<Button>().interactable){
            component_second = type;
            //Bt_component_second.GetComponentInChildren<Text>().text = type.ToString();
            Bt_component_second.GetComponent<Image>().sprite = slimeInfo.Find(item => item.GetSlimeType() == component_second).GetSprite();
            UpdateResult();
            return;
        }
    }


    public void UpdateResult(){
        PlaySound(sounds[0], volume: 0.3f);
        if (component_first is SlimeType.None && component_second is SlimeType.None){
            //Bt_result.GetComponentInChildren<Text>().text = "";
            Bt_result.GetComponent<Image>().sprite = null;
            Bt_result.GetComponentInChildren<TextMeshProUGUI>().text = "";
            Bt_result.GetComponent<Button>().interactable = true;
            return;
        }
        

        current_slime = slimeInfo.Find(item => new HashSet<SlimeType>(item.GetComponents()).SetEquals(new List<SlimeType>(){component_first, component_second}));
        //Bt_result.GetComponentInChildren<Text>().text = current_slime.GetSlimeType().ToString();
        Bt_result.GetComponent<Image>().sprite = current_slime.GetSprite();
        Bt_result.GetComponentInChildren<TextMeshProUGUI>().text = current_slime.GetCost().ToString();


        CheckMoney();
    }


    public void CheckMoney(){
        if (!shop.activeSelf) return; 
        if (onHavedMoney.Invoke(current_slime.GetCost())){
            Bt_result.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            Bt_result.GetComponent<Button>().interactable = false;
        } else{
            Bt_result.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
            Bt_result.GetComponent<Button>().interactable = true;
        }
    }

    public void ExitShop(){
        OnShopClick();
        OnFirstComponentClick();
        OnSecondComponentClick();
    }
}
