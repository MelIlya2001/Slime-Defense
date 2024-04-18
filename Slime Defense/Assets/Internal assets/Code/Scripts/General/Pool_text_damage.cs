using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pool_text_damage : MonoBehaviour
{
    public static Pool_text_damage Instance {get; private set;}
    [SerializeField] GameObject text_prefab;
    [SerializeField] uint start_count;

    private Pool pool;


    private void Awake()
    {

        //инициализируем пуллы объектов. Для удобства работы разбиты на 3 пулла по типам объекта: Враги, Слаймы, Снаряды. 
        if (Instance == null)
            Instance = this;
        
        pool = new Pool(transform);
        InitPool();
    }



    private void InitPool(){
        for (int i = 0; i < start_count; i++)
        {
            GameObject in_obj = Instantiate(text_prefab, transform);
            pool.Objects.Enqueue(in_obj);
        }
    }


    public void ShowDamage(float damage, Transform parent){
        GameObject obj = pool.Objects.Count > 0 ?
            pool.Objects.Dequeue() : Instantiate(text_prefab, parent);

        obj.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
        obj.transform.position = parent.position + new Vector3(0, 5, 0);
        obj.SetActive(true);
    }

    public void Remove(GameObject textObject){
         pool.Objects.Enqueue(textObject);
         textObject.SetActive(false);
        
    }
}
