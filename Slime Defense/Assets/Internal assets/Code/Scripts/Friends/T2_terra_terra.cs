using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_terra_terra : Abstract_Slime
{
   [SerializeField] private GameObject rock_wall;

   protected override void Deth_Skill(){
        rock_wall.SetActive(true);
        PoolControl.Instance.DestroyObject(gameObject);
   }
}
