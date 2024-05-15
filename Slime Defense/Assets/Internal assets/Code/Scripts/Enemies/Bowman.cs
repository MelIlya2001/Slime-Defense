using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : Abstract_minion
{
    [SerializeField] private GameObject Bow;
    protected override void AutoAttack(Collider target)
    {
        GameObject arow = PoolControl.Instance.GetObject(PoolControl.ProjectileType.arrow, PoolControl.Instance.projectile_pools);    
        arow.transform.position = Bow.transform.position;
        var bound = target.GetComponent<Collider>().bounds;
        arow.transform.LookAt(new Vector3(target.transform.position.x, bound.max.y, target.transform.position.z));
    }
}
