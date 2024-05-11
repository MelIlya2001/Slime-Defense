using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_air_water : Abstract_Slime
{
    [SerializeField] private GameObject ice;

   protected override void Deth_Skill(){
        ice.SetActive(true);
        PoolControl.Instance.DestroyObject(gameObject);
   }
}
