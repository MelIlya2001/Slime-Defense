using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_fire_terra : Abstract_Slime
{
   [SerializeField] private GameObject magma_surface;

   protected override void Deth_Skill(){
        magma_surface.SetActive(true);
        PoolControl.Instance.DestroyObject(gameObject);
   }
}
